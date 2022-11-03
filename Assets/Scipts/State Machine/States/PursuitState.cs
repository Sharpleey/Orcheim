using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuitState : State
{
    /// <summary>
    /// Дистанция до атаки 
    /// </summary>
    private float _attackDistance = 2.5f;
    /// <summary>
    /// Радиус генерации случайной точки на меше возле игрока
    /// </summary>
    private float _randomPointRadius = 6f;
    
    private Transform _transformPlayer;

    private Vector3 _positionRandomPointNearPlayer;
    private Vector3 _positionTarget;

    /// <summary>
    /// Дистанция от противника до игрока
    /// </summary>
    private float _distanceFromEnemyToPlayer;
    /// <summary>
    /// Дистанция от случайной точки возле игрока до игрока
    /// </summary>
    private float _distanceFromRandomPointToPlayer;

    private NavMeshPath _navMeshPath = new NavMeshPath();

    private float _timerUpdate;

    public PursuitState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // Обнуляем таймер
        _timerUpdate = 0;

        // Устанавливаем дистанцию атаки
        _attackDistance = _enemy.NavMeshAgent.stoppingDistance + 0.1f;

        // Получаем Transform игрока для отслеживания его позиции
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        // Включаем анимацию для этого состояния, задаем параметр анимации
        _enemy.Animator.SetBool(HashAnimation.IsMovement, true);

        // Получаем случайную точку в определенном радиусе (_randomPointRadius) рядом с игрок
        _positionRandomPointNearPlayer = GenerateRandomPointNearPlayer();
        // Задаем цель противнику, к которой он движется
        _enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
        // Устанавливаем дистанцию остановки 0, чтобы исключить ситуации, когда враг останавливался за радиусом (_randomPointRadius) и не мог сменить цель на игрока
        _enemy.NavMeshAgent.stoppingDistance = 0f;
    }

    public override void Update()
    {
        base.Update();

        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5f)
        {
            _distanceFromEnemyToPlayer = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);
            _distanceFromRandomPointToPlayer = Vector3.Distance(_positionRandomPointNearPlayer, _transformPlayer.position);

            // Если противник подошел на дистанцию атаки (_attackDistance), то изменяем состояние
            if (_distanceFromEnemyToPlayer < _attackDistance)
            {
                // Изменяем состояние на состояние атаки
                _enemy.ChangeState(_enemy.AttackIdleState);
            }

            // Если противник подошел в радиус генерации случайной точки  (_randomPointRadius) и если не проигрывается анимация атаки, то изменяем цель противнику
            if (_distanceFromEnemyToPlayer < _randomPointRadius)
            {
                // Изменяем дистанцию остановки протиника
                _enemy.NavMeshAgent.stoppingDistance = _attackDistance - 0.1f;
                // Изменияем цель противнику на игрока
                _enemy.NavMeshAgent.SetDestination(_transformPlayer.position);
            }

            // Генерим новую случайную точку, если текущая случайная точка находится за пределом радиуса (_randomPointRadius) и если не проигрывается анимация атаки
            if (_distanceFromRandomPointToPlayer > _randomPointRadius)
            {
                // Генерим случайную точку
                _positionRandomPointNearPlayer = GenerateRandomPointNearPlayer();
                // Изменияем цель противнику на новую случайную точку рядом с игроком
                _enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
            }

            // Обнуляем таймер
            _timerUpdate = 0;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // Рисуем линию от протиника до его цели
        //Debug.DrawLine(_enemy.transform.position, _enemy.NavMeshAgent.destination, Color.yellow);

        // Задаем параметр анимации
        _enemy.Animator.SetFloat(HashAnimation.Speed, _enemy.Speed / _enemy.MaxSpeed);
    }

    public override void Exit()
    {
        base.Exit();

        //_enemy.SummonTrigger.enabled = false;

        // Задаем параметр анимации, выключаем анимацию для этого состояния
        _enemy.Animator.SetBool(HashAnimation.IsMovement, false);
    }

    /// <summary>
    /// Метод генерирут случайную точку на навмеше радом с игроком
    /// </summary>
    /// <returns>Vector3 случайной точки</returns>
    private Vector3 GenerateRandomPointNearPlayer()
    {
        NavMeshHit navMeshHit;
        Vector3 randomPoint = Vector3.zero;

        bool isPathComplite = false;

        // TODO Оптимизировать, избавится от цикла
        while(!isPathComplite)
        {
            Vector3 sourcePosition = Random.insideUnitSphere * _randomPointRadius + _transformPlayer.position;
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

        return randomPoint;
    }
}
