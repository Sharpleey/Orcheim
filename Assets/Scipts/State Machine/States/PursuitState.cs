using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ����� ��������� ������������� ����������� ������
/// </summary>
public class PursuitState : State
{
    /// <summary>
    /// ��������� �� ����� 
    /// </summary>
    private float _attackDistance = 2.5f;
    
    /// <summary>
    /// ������ ��������� ��������� ����� �� ���� ����� ������
    /// </summary>
    private float _randomPointRadius = 8f;
    
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

    private float _timerUpdateDistance;

    public PursuitState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // �������� ������
        _timerUpdateDistance = 0;

        // ������������� ��������� �����
        _attackDistance = enemy.NavMeshAgent.stoppingDistance + 0.2f;

        // �������� Transform ������ ��� ������������ ��� �������
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemy.Animator.SetBool(HashAnimation.IsMovement, true);

        // �������� ��������� ����� � ������������ ������� (_randomPointRadius) ����� � �����
        _positionRandomPointNearPlayer = GenerateRandomPointNearPlayer();

        // ������ ���� ����������, � ������� �� ��������
        enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
        // ������������� ��������� ��������� 0, ����� ��������� ��������, ����� ���� �������������� �� �������� (_randomPointRadius) � �� ��� ������� ���� �� ������
        enemy.NavMeshAgent.stoppingDistance = 0f;
    }

    public override void Update()
    {
        base.Update();

        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            _distanceFromEnemyToPlayer = Vector3.Distance(enemy.transform.position, _transformPlayer.position);
            _distanceFromRandomPointToPlayer = Vector3.Distance(_positionRandomPointNearPlayer, _transformPlayer.position);

            // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
            if (_distanceFromEnemyToPlayer < _attackDistance)
            {
                // �������� ��������� �� ��������� �����
                enemy.SetState<AttackIdleState>();
            }

            // ���� ��������� ������� � ������ ��������� ��������� �����  (_randomPointRadius) � ���� �� ������������� �������� �����, �� �������� ���� ����������
            if (_distanceFromEnemyToPlayer < _randomPointRadius)
            {
                // �������� ��������� ��������� ���������
                enemy.NavMeshAgent.stoppingDistance = _attackDistance - 0.2f;
                // ��������� ���� ���������� �� ������
                enemy.NavMeshAgent.SetDestination(_transformPlayer.position);
            }

            // ������� ����� ��������� �����, ���� ������� ��������� ����� ��������� �� �������� ������� (_randomPointRadius) � ���� �� ������������� �������� �����
            if (_distanceFromRandomPointToPlayer > _randomPointRadius)
            {
                // ������� ��������� �����
                _positionRandomPointNearPlayer = GenerateRandomPointNearPlayer();
                // ��������� ���� ���������� �� ����� ��������� ����� ����� � �������
                enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
            }

            // �������� ������
            _timerUpdateDistance = 0;
        }

        // ������ ����� �� ��������� �� ��� ����
        //Debug.DrawLine(_enemy.transform.position, _enemy.NavMeshAgent.destination, Color.yellow);

        // ������ �������� ��������
        enemy.Animator.SetFloat(HashAnimation.Speed, enemy.Speed/enemy.MaxSpeed);
    }

    public override void Exit()
    {
        base.Exit();

        // ������ �������� ��������, ��������� �������� ��� ����� ���������
        enemy.Animator.SetBool(HashAnimation.IsMovement, false);
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
