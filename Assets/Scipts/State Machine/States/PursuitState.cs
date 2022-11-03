using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuitState : State
{
    /// <summary>
    /// ��������� �� ����� 
    /// </summary>
    private float _attackDistance = 2.5f;
    /// <summary>
    /// ������ ��������� ��������� ����� �� ���� ����� ������
    /// </summary>
    private float _randomPointRadius = 6f;
    
    private Transform _transformPlayer;

    private Vector3 _positionRandomPointNearPlayer;
    private Vector3 _positionTarget;

    /// <summary>
    /// ��������� �� ���������� �� ������
    /// </summary>
    private float _distanceFromEnemyToPlayer;
    /// <summary>
    /// ��������� �� ��������� ����� ����� ������ �� ������
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

        // �������� ������
        _timerUpdate = 0;

        // ������������� ��������� �����
        _attackDistance = _enemy.NavMeshAgent.stoppingDistance + 0.1f;

        // �������� Transform ������ ��� ������������ ��� �������
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        _enemy.Animator.SetBool(HashAnimation.IsMovement, true);

        // �������� ��������� ����� � ������������ ������� (_randomPointRadius) ����� � �����
        _positionRandomPointNearPlayer = GenerateRandomPointNearPlayer();
        // ������ ���� ����������, � ������� �� ��������
        _enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
        // ������������� ��������� ��������� 0, ����� ��������� ��������, ����� ���� �������������� �� �������� (_randomPointRadius) � �� ��� ������� ���� �� ������
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

            // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
            if (_distanceFromEnemyToPlayer < _attackDistance)
            {
                // �������� ��������� �� ��������� �����
                _enemy.ChangeState(_enemy.AttackIdleState);
            }

            // ���� ��������� ������� � ������ ��������� ��������� �����  (_randomPointRadius) � ���� �� ������������� �������� �����, �� �������� ���� ����������
            if (_distanceFromEnemyToPlayer < _randomPointRadius)
            {
                // �������� ��������� ��������� ���������
                _enemy.NavMeshAgent.stoppingDistance = _attackDistance - 0.1f;
                // ��������� ���� ���������� �� ������
                _enemy.NavMeshAgent.SetDestination(_transformPlayer.position);
            }

            // ������� ����� ��������� �����, ���� ������� ��������� ����� ��������� �� �������� ������� (_randomPointRadius) � ���� �� ������������� �������� �����
            if (_distanceFromRandomPointToPlayer > _randomPointRadius)
            {
                // ������� ��������� �����
                _positionRandomPointNearPlayer = GenerateRandomPointNearPlayer();
                // ��������� ���� ���������� �� ����� ��������� ����� ����� � �������
                _enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
            }

            // �������� ������
            _timerUpdate = 0;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // ������ ����� �� ��������� �� ��� ����
        //Debug.DrawLine(_enemy.transform.position, _enemy.NavMeshAgent.destination, Color.yellow);

        // ������ �������� ��������
        _enemy.Animator.SetFloat(HashAnimation.Speed, _enemy.Speed / _enemy.MaxSpeed);
    }

    public override void Exit()
    {
        base.Exit();

        //_enemy.SummonTrigger.enabled = false;

        // ������ �������� ��������, ��������� �������� ��� ����� ���������
        _enemy.Animator.SetBool(HashAnimation.IsMovement, false);
    }

    /// <summary>
    /// ����� ��������� ��������� ����� �� ������� ����� � �������
    /// </summary>
    /// <returns>Vector3 ��������� �����</returns>
    private Vector3 GenerateRandomPointNearPlayer()
    {
        NavMeshHit navMeshHit;
        Vector3 randomPoint = Vector3.zero;

        bool isPathComplite = false;

        // TODO ��������������, ��������� �� �����
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
