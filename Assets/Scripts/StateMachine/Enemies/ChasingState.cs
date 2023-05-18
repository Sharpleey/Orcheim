using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Базовый абстрактный класс состояния преследования противником игрока
/// </summary>
public abstract class ChasingState : EnemyState
{
    /// <summary>
    /// Радиус генерации случайной точки на меше возле игрока
    /// </summary>
    protected float _randomPointRadius = 8f;

    /// <summary>
    /// Позиция случайной точки возле игрока
    /// </summary>
    protected Vector3 _positionRandomPointNearTarget;

    /// <summary>
    /// Дистанция от случайной точки возле игрока до игрока
    /// </summary>
    protected float _distanceRandomPointToPlayer;

    /// <summary>
    /// Таймер обновления позиции
    /// </summary>
    protected float _timerUpdateDistance;

    /// <summary>
    /// Таймер воспроизведения звука рева противника
    /// </summary>
    protected float _timerAudioPlayback;

    /// <summary>
    /// Частота воспроизведения звука рева
    /// </summary>
    protected float _soundFrequency = 5f;

    protected NavMeshPath _navMeshPath = new NavMeshPath();

    public ChasingState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }
    public override void Enter()
    {
        // Обнуляем таймеры
        _timerUpdateDistance = 0.5f;
        _timerAudioPlayback = 0;

        // Получаем случайную точку в определенном радиусе (_randomPointRadius) рядом с игроком
        GenerateRandomPointNearTarget();

        // Двигаем противника к случайной точке
        MoveToRandomPointNearTarget();
    }

    public override void Update()
    {
        _timerUpdateDistance += Time.deltaTime;
        _timerAudioPlayback += Time.deltaTime;

        if (_timerUpdateDistance > 0.5f)
        {
            distanceEnemyToTarget = Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position);
            _distanceRandomPointToPlayer = Vector3.Distance(_positionRandomPointNearTarget, enemyUnit.TargetUnit.transform.position);

            // Генерим новую случайную точку, если текущая случайная точка находится за пределом радиуса (_randomPointRadius) и если не проигрывается анимация атаки
            if (_distanceRandomPointToPlayer > _randomPointRadius)
            {
                // Генерим случайную точку
                GenerateRandomPointNearTarget();

                // Двигаем противника к случайной точке
                MoveToRandomPointNearTarget();
            }

            // Если противник подошел в радиус генерации случайной точки, то изменяем цель противнику
            if (distanceEnemyToTarget < _randomPointRadius)
            {
                MoveToTarget();
            }

            // Обнуляем таймер
            _timerUpdateDistance = 0;
        }

        if(_timerAudioPlayback >= _soundFrequency)
        {
            // Воспроизводим звук
            enemyUnit?.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.Chasing);

            // Обнуляем таймер
            _timerAudioPlayback = 0;
        }
#if UNITY_EDITOR
        // Рисуем линию от протиника до его цели
        Debug.DrawLine(enemyUnit.transform.position, enemyUnit.NavMeshAgent.destination, Color.yellow);
#endif
        // Задаем параметр анимации
        enemyUnit.Animator.SetFloat(HashAnimStringEnemy.Speed, enemyUnit.NavMeshAgent.velocity.magnitude / (enemyUnit.MovementSpeed.Max/100f));
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// Враг двигается в сторону случайной точки возле игрока
    /// </summary>
    protected void MoveToRandomPointNearTarget()
    {
        // Изменяем дистанцию остановки протиника
        enemyUnit.NavMeshAgent.stoppingDistance = 0f;
        // Изменияем цель противнику на новую случайную точку рядом с игроком
        enemyUnit?.NavMeshAgent?.SetDestination(_positionRandomPointNearTarget);
    }

    /// <summary>
    /// Враг двигается в сторону игрока
    /// </summary>
    protected void MoveToTarget()
    {
        // Изменяем дистанцию остановки протиника
        enemyUnit.NavMeshAgent.stoppingDistance = enemyUnit.AttackDistance - 0.5f;
        // Изменияем цель противнику на игрока
        enemyUnit.NavMeshAgent.SetDestination(enemyUnit.TargetUnit.transform.position);
    }

    /// <summary>
    /// Метод генерирут случайную точку на навмеше радом с игроком
    /// </summary>
    /// <returns>Позицию случайной точки</returns>
    protected void GenerateRandomPointNearTarget()
    {
        NavMeshHit navMeshHit;
        Vector3 randomPoint = Vector3.zero;

        bool isPathComplite = false;

        // TODO Оптимизировать, избавится от цикла
        while(!isPathComplite)
        {
            Vector3 sourcePosition = Random.insideUnitSphere * _randomPointRadius + enemyUnit.TargetUnit.transform.position;
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

        _positionRandomPointNearTarget = randomPoint;
    }
}   

