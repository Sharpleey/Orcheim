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
    protected Vector3 _positionRandomPointNearPlayer;

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

    public ChasingState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        // Обнуляем таймеры
        _timerUpdateDistance = 0.5f;
        _timerAudioPlayback = 0;

        // Получаем Transform игрока для отслеживания его позиции
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // Получаем случайную точку в определенном радиусе (_randomPointRadius) рядом с игроком
        GenerateRandomPointNearPlayer();

        // Двигаем противника к случайной точке
        MoveToRandomPointNearPlayer();
    }

    public override void Update()
    {
        _timerUpdateDistance += Time.deltaTime;
        _timerAudioPlayback += Time.deltaTime;

        if (_timerUpdateDistance > 0.5f)
        {
            distanceEnemyToPlayer = Vector3.Distance(enemy.transform.position, transformPlayer.position);
            _distanceRandomPointToPlayer = Vector3.Distance(_positionRandomPointNearPlayer, transformPlayer.position);

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

        if(_timerAudioPlayback >= _soundFrequency)
        {
            // Воспроизводим звук
            if (enemy.AudioController)
                enemy.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Agro);

            // Обнуляем таймер
            _timerAudioPlayback = 0;
        }    

        // Рисуем линию от протиника до его цели
        //Debug.DrawLine(enemy.transform.position, enemy.NavMeshAgent.destination, Color.yellow);

        // Задаем параметр анимации
        enemy.Animator.SetFloat(HashAnimStringEnemy.Speed, enemy.NavMeshAgent.velocity.magnitude/ enemy.MovementSpeed.MaxSpeed);
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// Враг двигается в сторону случайной точки возле игрока
    /// </summary>
    protected void MoveToRandomPointNearPlayer()
    {
        // Изменяем дистанцию остановки протиника
        enemy.NavMeshAgent.stoppingDistance = 0f;
        // Изменияем цель противнику на новую случайную точку рядом с игроком
        enemy?.NavMeshAgent?.SetDestination(_positionRandomPointNearPlayer);
    }

    /// <summary>
    /// Враг двигается в сторону игрока
    /// </summary>
    protected void MoveToPlayer()
    {
        // Изменяем дистанцию остановки протиника
        enemy.NavMeshAgent.stoppingDistance = enemy.AttackDistance - 0.5f;
        // Изменияем цель противнику на игрока
        enemy.NavMeshAgent.SetDestination(transformPlayer.position);
    }

    /// <summary>
    /// Метод генерирут случайную точку на навмеше радом с игроком
    /// </summary>
    /// <returns>Позицию случайной точки</returns>
    protected void GenerateRandomPointNearPlayer()
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
}   

