using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ��������� �������������� ���������� ����������. �������������� ������������ �� ������
/// </summary>
public class PatrollingState : IdleState
{
    private Transform[] _wayPoints;

    private float _distanceEnemyToWayPoint; 

    private Vector3 _positionCurrentWayPoint;
    private int _indexCurrentWayPoint;

    /// <summary>
    /// �������� ���������������
    /// </summary>
    private float _patrolSpeed = 1.8f;

    public PatrollingState(EnemyUnit enemyUnit) : base(enemyUnit)
    {
    }

    public override void Enter()
    {
        // �������� ���������
        _indexCurrentWayPoint = 0;
        _timerUpdate = 0;

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);

        // �������� ��������� ��������� ���������
        enemyUnit.NavMeshAgent.stoppingDistance = 0f;
        enemyUnit.NavMeshAgent.angularSpeed = 120f;

        // �������� ������� ���������� �� ������ ��������� ������
        enemyUnit.SummonTrigger?.SetEnable(true);

        // �������� ��������� ������� �������
        PatrolRoute nearbyPatrolRoute = FindNearbyRoute();

        if (nearbyPatrolRoute == null)
        {
            // ���� �� ����� ������� ��� �������������� �� ������ ���������
            enemyUnit.SetState<IdleState>();

            return;
        }

        // �������� ����� ��������������
        _wayPoints = nearbyPatrolRoute?.WayPoints;

        MoveToWayPoint();
    }

    public override void Update()
    {
        base.Update();

        _distanceEnemyToWayPoint = Vector3.Distance(enemyUnit.transform.position, _positionCurrentWayPoint);

        // ���� ��������� �� ����� �������� ������ 1 �����
        if (_distanceEnemyToWayPoint <= 1f)
        {
            // ����������� ������ �����
            _indexCurrentWayPoint += 1;

            // ���� ������ ����� �� ������� ������� � �������
            if (_indexCurrentWayPoint == _wayPoints.Length)
                _indexCurrentWayPoint = 0;

            MoveToWayPoint();
        }

        // ������ ����� �� ��������� �� ��� ����
        Debug.DrawLine(enemyUnit.transform.position, enemyUnit.NavMeshAgent.destination, Color.yellow);

        // ������ �������� ��������
        enemyUnit.Animator.SetFloat(HashAnimStringEnemy.Speed, enemyUnit.NavMeshAgent.velocity.magnitude / (enemyUnit.MovementSpeed.Max/100f));
    }

    public override void Exit()
    {
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);

        // ���������� �������� ��������
        enemyUnit.MovementSpeed.Actual = enemyUnit.MovementSpeed.Max;
        enemyUnit.NavMeshAgent.speed = enemyUnit.MovementSpeed.Actual/100f;

        enemyUnit.NavMeshAgent.angularSpeed = 360f;

        // �������� ��������� ��������� ����������
        enemyUnit.NavMeshAgent.stoppingDistance = enemyUnit.AttackDistance - 0.2f;

        // ��������� ������� ���������� �� ������ ��������� ����� ������� ������� ������
        enemyUnit.SummonTrigger?.SetEnable(false);
    }

    /// <summary>
    /// �����
    /// </summary>
    /// <returns></returns>
    private PatrolRoute FindNearbyRoute()
    {
        float distanceNearbyPatrolRoute = 999;
        PatrolRoute nearbyPatrolRoute = null;

        // �������� ������� �� ����� � ������� �������������� 
        GameObject[] _patrolRoutes = GameObject.FindGameObjectsWithTag("PatrolRoute"); //TODO

        if (_patrolRoutes == null)
            return null;

        foreach (GameObject patrol in _patrolRoutes)
        {
            PatrolRoute patrolRoute = patrol.GetComponent<PatrolRoute>();

            if (!patrolRoute)
                continue;

            Transform wayPoint = patrolRoute.WayPoints[0];

            // ���������� ���������� �� ��������� ���������� �� ������ ����� ��������
            float distance = Vector3.Distance(enemyUnit.transform.position, wayPoint.position);

            if(distance <= distanceNearbyPatrolRoute)
            {
                nearbyPatrolRoute = patrolRoute;
                distanceNearbyPatrolRoute = distance;
            }
        }

        return nearbyPatrolRoute;
    }


    /// <summary>
    /// ����� ������������� ���������� ����� �������������� � ��������� ��� ����� ��� ���������� ��������� ���������� 
    /// </summary>
    private void MoveToWayPoint()
    {
        // �������� ��������� ����� �������� �� �������
        _positionCurrentWayPoint = _wayPoints[_indexCurrentWayPoint].position;
        // ��������� ������� ����� �� ������� �� �������
        _positionCurrentWayPoint = GetPointOnNavmesh(_positionCurrentWayPoint);

        // ������ ��������, ����� ������� ������������ ������� ���������
        float speed = _patrolSpeed + Random.Range(-0.05f, 0.05f);
        enemyUnit.MovementSpeed.Actual = speed;
        enemyUnit.NavMeshAgent.speed = speed;

        // ������������� ����� ���������� ��������� ���������� �� ����� ��������
        enemyUnit?.NavMeshAgent?.SetDestination(_positionCurrentWayPoint);
    }


    /// <summary>
    /// ����� �������� �� ����� � �� ����� ����� �� �������
    /// </summary>
    /// <param name="wayPoint">������� ����� ��������</param>
    /// <returns>������� ����� �� �������</returns>
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
}