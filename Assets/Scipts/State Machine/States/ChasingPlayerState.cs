using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ����� ��������� ������������� ����������� ������
/// </summary>
public class ChasingPlayerState : State
{
    /// <summary>
    /// ��������� �� ����� 
    /// </summary>
    private float _attackDistance = 2.5f;
    
    /// <summary>
    /// ������ ��������� ��������� ����� �� ���� ����� ������
    /// </summary>
    private float _randomPointRadius = 8f;

    /// <summary>
    /// ������� ��������� ����� ����� ������
    /// </summary>
    private Vector3 _positionRandomPointNearPlayer;

    /// <summary>
    /// ��������� �� ��������� ����� ����� ������ �� ������
    /// </summary>
    private float _distanceRandomPointToPlayer;

    private NavMeshPath _navMeshPath = new NavMeshPath();

    /// <summary>
    /// ������ ���������� �������
    /// </summary>
    private float _timerUpdateDistance;

    public ChasingPlayerState(Enemy enemy) : base(enemy)
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
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

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
            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
            _distanceRandomPointToPlayer = GetDistanceRandomPointToPlayer();

            // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
            if (distanceEnemyToPlayer < _attackDistance)
            {
                // �������� ��������� �� ��������� �����
                enemy.SetState<AttackState>();
            }

            // ���� ��������� ������� � ������ ��������� ��������� �����  (_randomPointRadius) � ���� �� ������������� �������� �����, �� �������� ���� ����������
            if (distanceEnemyToPlayer < _randomPointRadius)
            {
                // �������� ��������� ��������� ���������
                enemy.NavMeshAgent.stoppingDistance = _attackDistance - 0.2f;
                // ��������� ���� ���������� �� ������
                enemy.NavMeshAgent.SetDestination(transformPlayer.position);
            }

            // ������� ����� ��������� �����, ���� ������� ��������� ����� ��������� �� �������� ������� (_randomPointRadius) � ���� �� ������������� �������� �����
            if (_distanceRandomPointToPlayer > _randomPointRadius)
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
    /// <returns>������� ��������� �����</returns>
    private Vector3 GenerateRandomPointNearPlayer()
    {
        NavMeshHit navMeshHit;
        Vector3 randomPoint = Vector3.zero;

        bool isPathComplite = false;

        // TODO ��������������, ��������� �� �����
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

        return randomPoint;
    }

    /// <summary>
    /// ����� ���������� ��������� �� ��������� ����� �� ������
    /// </summary>
    /// <returns>��������� ��������� ����� �� ������</returns>
    private float GetDistanceRandomPointToPlayer()
    {
        return Vector3.Distance(_positionRandomPointNearPlayer, transformPlayer.position);
    }
}   

