using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Состояние патрулирования персонажем противника. Патрулирование производится по точкам
/// </summary>
public class PatrollingState : EnemyState
{
    private Transform[] _wayPoints;

    private float _distanceEnemyToWayPoint; 

    private Vector3 _positionCurrentWayPoint;
    private int _indexCurrentWayPoint;

    private float _timerUpdateDistance;

    /// <summary>
    /// Угол обзора враг
    /// </summary>
    private float _viewAngleDetection = 120f;

    /// <summary>
    /// Дистанция обзора врага
    /// </summary>
    private float _viewDetectionDistance = 14f;

    /// <summary>
    /// Радиус обнаружения, при котором в любом случае враг заметит игрока
    /// </summary>
    private float _absoluteDetectionDistance = 4f;

    public PatrollingState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        // Обнуляем параметры
        _indexCurrentWayPoint = 0;
        _timerUpdateDistance = 0;

        // Включаем анимацию для этого состояния, задаем параметр анимации
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);

        // Изменяем дистанцию остановки протиника
        enemy.NavMeshAgent.stoppingDistance = 0f;
        enemy.NavMeshAgent.angularSpeed = 120f;

        // Получаем Transform игрока для отслеживания его позиции
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        WaveEventManager.OnWaveIsComing.AddListener(SetChasingState);

        // Получаем ближайший маршрут патруля
        PatrolRoute nearbyPatrolRoute = FindNearbyRoute();

        if (nearbyPatrolRoute == null)
        {
            // Если не нашли маршрут для патрулирования то меняем состояние
            enemy.SetState<IdleState>();

            return;
        }

        // Получаем точки патрулирования
        _wayPoints = nearbyPatrolRoute?.WayPoints;

        MoveToWayPoint();
    }

    public override void Update()
    {
        _timerUpdateDistance += Time.deltaTime;

        // Проверяем дистанцию каждуые полсекунды
        if(_timerUpdateDistance >= 0.5f)
        {
            // На случай, когда игрок еще не заспавнился
            if (!transformPlayer)
            {
                transformPlayer = GetTransformPlayer();
                return;
            }

            // Получаем дистанцию от персонажа противника до игрока
            distanceEnemyToPlayer = Vector3.Distance(enemy.transform.position, transformPlayer.position);
            // Получаем дистанцию от персонажа противника до точки маршрута
            _distanceEnemyToWayPoint = Vector3.Distance(enemy.transform.position, _positionCurrentWayPoint);

            // Меняем сосстояние на преследеование, если (Игрок в зоне абсолютной дистанции видимости) или (Персонаж проивника увидил игрока перед собой)
            if (distanceEnemyToPlayer < _absoluteDetectionDistance || IsPlayerInSight())
            {
                // Воспроизводим звук
                if (enemy.AudioController)
                    enemy.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Confused);

                enemy.SetState<ChasingState>();
            }


            // Если дистанция до точки маршрута меньше 1 метра
            if (_distanceEnemyToWayPoint <= 1f)
            {
                // Увеличиваем индекс точки
                _indexCurrentWayPoint += 1;

                // Если индекс вышел за пределы массива с точками
                if (_indexCurrentWayPoint == _wayPoints.Length)
                    _indexCurrentWayPoint = 0;

                MoveToWayPoint();
            }

            // Обнуляем таймер
            _timerUpdateDistance = 0;
        }

        // Рисуем линию от протиника до его цели
        Debug.DrawLine(enemy.transform.position, enemy.NavMeshAgent.destination, Color.yellow);

        // Задаем параметр анимации
        enemy.Animator.SetFloat(HashAnimStringEnemy.Speed, enemy.Speed / enemy.MaxSpeed);
    }

    public override void Exit()
    {
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);

        // Возвращаем исходную скорость
        enemy.Speed = enemy.MaxSpeed;
        enemy.NavMeshAgent.angularSpeed = 360f;

        // Изменяем дистанцию остановки противника
        enemy.NavMeshAgent.stoppingDistance = enemy.AttackDistance - 0.2f;

        WaveEventManager.OnWaveIsComing.RemoveListener(SetChasingState);
    }

    /// <summary>
    /// Метож
    /// </summary>
    /// <returns></returns>
    private PatrolRoute FindNearbyRoute()
    {
        float distanceNearbyPatrolRoute = 999;
        PatrolRoute nearbyPatrolRoute = null;

        // Получаем объекты на сцене с точками патрулирования 
        GameObject[] _patrolRoutes = GameObject.FindGameObjectsWithTag("PatrolRoute");

        if (_patrolRoutes == null)
            return null;

        foreach (GameObject patrol in _patrolRoutes)
        {
            PatrolRoute patrolRoute = patrol.GetComponent<PatrolRoute>();

            if (!patrolRoute)
                continue;

            Transform wayPoint = patrolRoute.WayPoints[0];

            // Определяем дистаниция от персонажа противника до первой точки маршрута
            float distance = Vector3.Distance(enemy.transform.position, wayPoint.position);

            if(distance <= distanceNearbyPatrolRoute)
            {
                nearbyPatrolRoute = patrolRoute;
                distanceNearbyPatrolRoute = distance;
            }
        }

        return nearbyPatrolRoute;
    }


    /// <summary>
    /// Метод устанавливает актуальную точку патрулирование и назначает эту точку для достижения персонажу противника 
    /// </summary>
    private void MoveToWayPoint()
    {
        // Получаем следующую точку маршрута по индексу
        _positionCurrentWayPoint = _wayPoints[_indexCurrentWayPoint].position;
        // Переводим позицию точки на позицию на навмеше
        _positionCurrentWayPoint = GetPointOnNavmesh(_positionCurrentWayPoint);

        // Меняем скорость, что сделать передвижение немного хаотично
        enemy.Speed = enemy.MaxSpeed / 2 + Random.Range(-0.05f, 0.05f);

        // Устанавливаем точку назначения персонажу противника на точку маршрута
        enemy?.NavMeshAgent?.SetDestination(_positionCurrentWayPoint);
    }


    /// <summary>
    /// Метод получает из точки в на сцене точку на навмеше
    /// </summary>
    /// <param name="wayPoint">Позицию точки маршрута</param>
    /// <returns>Позццию точки на навмеше</returns>
    private Vector3 GetPointOnNavmesh(Vector3 wayPoint)
    {
        NavMeshHit navMeshHit;
        Vector3 positionWayPointInNavMesh;

        NavMesh.SamplePosition(wayPoint, out navMeshHit, 1f, NavMesh.AllAreas);

        float x = Random.Range(navMeshHit.position.x - 1.5f, navMeshHit.position.x + 1.5f);
        float z = Random.Range(navMeshHit.position.z - 1.5f, navMeshHit.position.z + 1.5f);

        positionWayPointInNavMesh = new Vector3(x, navMeshHit.position.y, z);

        return positionWayPointInNavMesh;
    }

    /// <summary>
    /// Метод проверяет находится ли игрок в поле зрения врага
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerInSight()
    {
        float realAngle = Vector3.Angle(enemy.transform.forward, transformPlayer.position - enemy.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, transformPlayer.position - enemy.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(enemy.transform.position, transformPlayer.position) <= _viewDetectionDistance && hit.transform == transformPlayer.transform)
            {
                return true;
            }
        }
        return false;
    }
}
