using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Класс состояния преследования противником игрока
/// </summary>
public class ChasingPlayerState : State
{
    /// <summary>
    /// Дистанция до атаки 
    /// </summary>
    private float _attackDistance = 2.5f;
    
    /// <summary>
    /// Радиус генерации случайной точки на меше возле игрока
    /// </summary>
    private float _randomPointRadius = 8f;

    /// <summary>
    /// Позиция случайной точки возле игрока
    /// </summary>
    private Vector3 _positionRandomPointNearPlayer;

    /// <summary>
    /// Дистанция от случайной точки возле игрока до игрока
    /// </summary>
    private float _distanceRandomPointToPlayer;

    /// <summary>
    /// Таймер обновления позиции
    /// </summary>
    private float _timerUpdateDistance;

    private NavMeshPath _navMeshPath = new NavMeshPath();


    public ChasingPlayerState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        // Обнуляем таймер
        _timerUpdateDistance = 0.5f;

        // Получаем Transform игрока для отслеживания его позиции
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // Включаем анимацию для этого состояния, задаем параметр анимации
        enemy.Animator.SetBool(HashAnimation.IsMovement, true);

        // Получаем случайную точку в определенном радиусе (_randomPointRadius) рядом с игроком
        GenerateRandomPointNearPlayer();

        // Двигаем противника к случайной точке
        MoveToRandomPointNearPlayer();
    }

    public override void Update()
    {
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
            _distanceRandomPointToPlayer = GetDistanceRandomPointToPlayer();

            // Генерим новую случайную точку, если текущая случайная точка находится за пределом радиуса (_randomPointRadius) и если не проигрывается анимация атаки
            if (_distanceRandomPointToPlayer > _randomPointRadius)
            {
                // Генерим случайную точку
                GenerateRandomPointNearPlayer();

                // Двигаем противника к случайной точке
                MoveToRandomPointNearPlayer();
            }

            // Если противник подошел в радиус генерации случайной точки, то изменяем цель противнику
            if (distanceEnemyToPlayer < _randomPointRadius)
            {
                MoveToPlayer();
            }

            // Обнуляем таймер
            _timerUpdateDistance = 0;
        }

        // Рисуем линию от протиника до его цели
        //Debug.DrawLine(enemy.transform.position, enemy.NavMeshAgent.destination, Color.yellow);

        // Задаем параметр анимации
        enemy.Animator.SetFloat(HashAnimation.Speed, enemy.Speed/enemy.MaxSpeed);


        // Если противник подошел на дистанцию атаки (_attackDistance), то изменяем состояние
        if (distanceEnemyToPlayer < _attackDistance)
        {
            // Изменяем состояние на состояние атаки
            enemy.SetState<AttackState>();
        }
    }

    public override void Exit()
    {
        // Задаем параметр анимации, выключаем анимацию для этого состояния
        enemy.Animator.SetBool(HashAnimation.IsMovement, false);
    }

    /// <summary>
    /// Враг двигается в сторону случайной точки возле игрока
    /// </summary>
    private void MoveToRandomPointNearPlayer()
    {
        // Изменяем дистанцию остановки протиника
        enemy.NavMeshAgent.stoppingDistance = 0f;
        // Изменияем цель противнику на новую случайную точку рядом с игроком
        enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
    }

    /// <summary>
    /// Враг двигается в сторону игрока
    /// </summary>
    private void MoveToPlayer()
    {
        // Изменяем дистанцию остановки протиника
        enemy.NavMeshAgent.stoppingDistance = _attackDistance - 0.2f;
        // Изменияем цель противнику на игрока
        enemy.NavMeshAgent.SetDestination(transformPlayer.position);
    }

    /// <summary>
    /// Метод генерирут случайную точку на навмеше радом с игроком
    /// </summary>
    /// <returns>Позицию случайной точки</returns>
    private void GenerateRandomPointNearPlayer()
    {
        NavMeshHit navMeshHit;
        Vector3 randomPoint = Vector3.zero;

        bool isPathComplite = false;

        // TODO Оптимизировать, избавится от цикла
        while(!isPathComplite)
        {
            Vector3 sourcePosition = Random.insideUnitSphere * _randomPointRadius + transformPlayer.position;
            NavMesh.SamplePosition(sourcePosition, out navMeshHit, _randomPointRadius, NavMesh.AllAreas);
            randomPoint = navMeshHit.position;
            isPathComplite = true;

            //if (randomPoint.y > -10000 && randomPoint.y < 10000)
            //{
            //    //_enemy.NavMeshAgent.CalculatePath(randomPoint, _navMeshPath);

            //    //if(_navMeshPath.status == NavMeshPathStatus.PathComplete && !NavMesh.Raycast(_transformPlayer.position, randomPoint, out navMeshHit, NavMesh.AllAreas))
            //    //{
            //    //    isPathComplite = true;
            //    //}

            //    if (!NavMesh.Raycast(_transformPlayer.position, randomPoint, out navMeshHit, NavMesh.AllAreas))
            //    {
            //        isPathComplite = true;
            //    }
            //}
        }

        _positionRandomPointNearPlayer = randomPoint;
    }

    /// <summary>
    /// Метод определяет дистанцию от случайной точки до игрока
    /// </summary>
    /// <returns>Дистанцию случайной точки до игрока</returns>
    private float GetDistanceRandomPointToPlayer()
    {
        return Vector3.Distance(_positionRandomPointNearPlayer, transformPlayer.position);
    }
}   

