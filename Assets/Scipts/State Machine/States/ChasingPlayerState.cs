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

    /// <summary>
    /// ������ ���������� �������
    /// </summary>
    private float _timerUpdateDistance;

    private NavMeshPath _navMeshPath = new NavMeshPath();


    public ChasingPlayerState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        // �������� ������
        _timerUpdateDistance = 0.5f;

        // �������� Transform ������ ��� ������������ ��� �������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemy.Animator.SetBool(HashAnimation.IsMovement, true);

        // �������� ��������� ����� � ������������ ������� (_randomPointRadius) ����� � �������
        GenerateRandomPointNearPlayer();

        // ������� ���������� � ��������� �����
        MoveToRandomPointNearPlayer();
    }

    public override void Update()
    {
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
            _distanceRandomPointToPlayer = GetDistanceRandomPointToPlayer();

            // ������� ����� ��������� �����, ���� ������� ��������� ����� ��������� �� �������� ������� (_randomPointRadius) � ���� �� ������������� �������� �����
            if (_distanceRandomPointToPlayer > _randomPointRadius)
            {
                // ������� ��������� �����
                GenerateRandomPointNearPlayer();

                // ������� ���������� � ��������� �����
                MoveToRandomPointNearPlayer();
            }

            // ���� ��������� ������� � ������ ��������� ��������� �����, �� �������� ���� ����������
            if (distanceEnemyToPlayer < _randomPointRadius)
            {
                MoveToPlayer();
            }

            // �������� ������
            _timerUpdateDistance = 0;
        }

        // ������ ����� �� ��������� �� ��� ����
        //Debug.DrawLine(enemy.transform.position, enemy.NavMeshAgent.destination, Color.yellow);

        // ������ �������� ��������
        enemy.Animator.SetFloat(HashAnimation.Speed, enemy.Speed/enemy.MaxSpeed);


        // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
        if (distanceEnemyToPlayer < _attackDistance)
        {
            // �������� ��������� �� ��������� �����
            enemy.SetState<AttackState>();
        }
    }

    public override void Exit()
    {
        // ������ �������� ��������, ��������� �������� ��� ����� ���������
        enemy.Animator.SetBool(HashAnimation.IsMovement, false);
    }

    /// <summary>
    /// ���� ��������� � ������� ��������� ����� ����� ������
    /// </summary>
    private void MoveToRandomPointNearPlayer()
    {
        // �������� ��������� ��������� ���������
        enemy.NavMeshAgent.stoppingDistance = 0f;
        // ��������� ���� ���������� �� ����� ��������� ����� ����� � �������
        enemy.NavMeshAgent.SetDestination(_positionRandomPointNearPlayer);
    }

    /// <summary>
    /// ���� ��������� � ������� ������
    /// </summary>
    private void MoveToPlayer()
    {
        // �������� ��������� ��������� ���������
        enemy.NavMeshAgent.stoppingDistance = _attackDistance - 0.2f;
        // ��������� ���� ���������� �� ������
        enemy.NavMeshAgent.SetDestination(transformPlayer.position);
    }

    /// <summary>
    /// ����� ��������� ��������� ����� �� ������� ����� � �������
    /// </summary>
    /// <returns>������� ��������� �����</returns>
    private void GenerateRandomPointNearPlayer()
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

        _positionRandomPointNearPlayer = randomPoint;
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

