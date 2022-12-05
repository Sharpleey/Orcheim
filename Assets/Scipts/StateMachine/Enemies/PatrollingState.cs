using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ��������� �������������� ���������� ����������. �������������� ������������ �� ������
/// </summary>
public class PatrollingState : EnemyState
{
    private Transform[] _wayPoints;

    private float _distanceEnemyToWayPoint; 

    private Vector3 _positionCurrentWayPoint;
    private int _indexCurrentWayPoint;

    private float _timerUpdateDistance;

    /// <summary>
    /// ���� ������ ����
    /// </summary>
    private float _viewAngleDetection = 120f;

    /// <summary>
    /// ��������� ������ �����
    /// </summary>
    private float _viewDetectionDistance = 14f;

    /// <summary>
    /// ������ �����������, ��� ������� � ����� ������ ���� ������� ������
    /// </summary>
    private float _absoluteDetectionDistance = 4f;

    public PatrollingState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        // �������� ���������
        _indexCurrentWayPoint = 0;
        _timerUpdateDistance = 0;

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);

        // �������� ��������� ��������� ���������
        enemy.NavMeshAgent.stoppingDistance = 0f;
        enemy.NavMeshAgent.angularSpeed = 120f;

        // �������� Transform ������ ��� ������������ ��� �������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        WaveEventManager.OnWaveIsComing.AddListener(SetChasingState);

        // �������� ��������� ������� �������
        PatrolRoute nearbyPatrolRoute = FindNearbyRoute();

        if (nearbyPatrolRoute == null)
        {
            // ���� �� ����� ������� ��� �������������� �� ������ ���������
            enemy.SetState<IdleState>();

            return;
        }

        // �������� ����� ��������������
        _wayPoints = nearbyPatrolRoute?.WayPoints;

        MoveToWayPoint();
    }

    public override void Update()
    {
        _timerUpdateDistance += Time.deltaTime;

        // ��������� ��������� ������� ����������
        if(_timerUpdateDistance >= 0.5f)
        {
            // �� ������, ����� ����� ��� �� �����������
            if (!transformPlayer)
            {
                transformPlayer = GetTransformPlayer();
                return;
            }

            // �������� ��������� �� ��������� ���������� �� ������
            distanceEnemyToPlayer = Vector3.Distance(enemy.transform.position, transformPlayer.position);
            // �������� ��������� �� ��������� ���������� �� ����� ��������
            _distanceEnemyToWayPoint = Vector3.Distance(enemy.transform.position, _positionCurrentWayPoint);

            // ������ ���������� �� ��������������, ���� (����� � ���� ���������� ��������� ���������) ��� (�������� ��������� ������ ������ ����� �����)
            if (distanceEnemyToPlayer < _absoluteDetectionDistance || IsPlayerInSight())
            {
                // ������������� ����
                if (enemy.AudioController)
                    enemy.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Confused);

                enemy.SetState<ChasingState>();
            }


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

            // �������� ������
            _timerUpdateDistance = 0;
        }

        // ������ ����� �� ��������� �� ��� ����
        Debug.DrawLine(enemy.transform.position, enemy.NavMeshAgent.destination, Color.yellow);

        // ������ �������� ��������
        enemy.Animator.SetFloat(HashAnimStringEnemy.Speed, enemy.Speed / enemy.MaxSpeed);
    }

    public override void Exit()
    {
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);

        // ���������� �������� ��������
        enemy.Speed = enemy.MaxSpeed;
        enemy.NavMeshAgent.angularSpeed = 360f;

        // �������� ��������� ��������� ����������
        enemy.NavMeshAgent.stoppingDistance = enemy.AttackDistance - 0.2f;

        WaveEventManager.OnWaveIsComing.RemoveListener(SetChasingState);
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
        GameObject[] _patrolRoutes = GameObject.FindGameObjectsWithTag("PatrolRoute");

        if (_patrolRoutes == null)
            return null;

        foreach (GameObject patrol in _patrolRoutes)
        {
            PatrolRoute patrolRoute = patrol.GetComponent<PatrolRoute>();

            if (!patrolRoute)
                continue;

            Transform wayPoint = patrolRoute.WayPoints[0];

            // ���������� ���������� �� ��������� ���������� �� ������ ����� ��������
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
    /// ����� ������������� ���������� ����� �������������� � ��������� ��� ����� ��� ���������� ��������� ���������� 
    /// </summary>
    private void MoveToWayPoint()
    {
        // �������� ��������� ����� �������� �� �������
        _positionCurrentWayPoint = _wayPoints[_indexCurrentWayPoint].position;
        // ��������� ������� ����� �� ������� �� �������
        _positionCurrentWayPoint = GetPointOnNavmesh(_positionCurrentWayPoint);

        // ������ ��������, ��� ������� ������������ ������� ��������
        enemy.Speed = enemy.MaxSpeed / 2 + Random.Range(-0.05f, 0.05f);

        // ������������� ����� ���������� ��������� ���������� �� ����� ��������
        enemy?.NavMeshAgent?.SetDestination(_positionCurrentWayPoint);
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

    /// <summary>
    /// ����� ��������� ��������� �� ����� � ���� ������ �����
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
