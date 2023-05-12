using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ������� ����������� ����� ��������� ������������� ����������� ������
/// </summary>
public abstract class ChasingState : EnemyState
{
    /// <summary>
    /// ������ ��������� ��������� ����� �� ���� ����� ������
    /// </summary>
    protected float _randomPointRadius = 8f;

    /// <summary>
    /// ������� ��������� ����� ����� ������
    /// </summary>
    protected Vector3 _positionRandomPointNearTarget;

    /// <summary>
    /// ��������� �� ��������� ����� ����� ������ �� ������
    /// </summary>
    protected float _distanceRandomPointToPlayer;

    /// <summary>
    /// ������ ���������� �������
    /// </summary>
    protected float _timerUpdateDistance;

    /// <summary>
    /// ������ ��������������� ����� ���� ����������
    /// </summary>
    protected float _timerAudioPlayback;

    /// <summary>
    /// ������� ��������������� ����� ����
    /// </summary>
    protected float _soundFrequency = 5f;

    protected NavMeshPath _navMeshPath = new NavMeshPath();

    public ChasingState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }
    public override void Enter()
    {
        // �������� �������
        _timerUpdateDistance = 0.5f;
        _timerAudioPlayback = 0;

        // �������� ��������� ����� � ������������ ������� (_randomPointRadius) ����� � �������
        GenerateRandomPointNearTarget();

        // ������� ���������� � ��������� �����
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

            // ������� ����� ��������� �����, ���� ������� ��������� ����� ��������� �� �������� ������� (_randomPointRadius) � ���� �� ������������� �������� �����
            if (_distanceRandomPointToPlayer > _randomPointRadius)
            {
                // ������� ��������� �����
                GenerateRandomPointNearTarget();

                // ������� ���������� � ��������� �����
                MoveToRandomPointNearTarget();
            }

            // ���� ��������� ������� � ������ ��������� ��������� �����, �� �������� ���� ����������
            if (distanceEnemyToTarget < _randomPointRadius)
            {
                MoveToTarget();
            }

            // �������� ������
            _timerUpdateDistance = 0;
        }

        if(_timerAudioPlayback >= _soundFrequency)
        {
            // ������������� ����
            enemyUnit?.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.Chasing);

            // �������� ������
            _timerAudioPlayback = 0;
        }
#if UNITY_EDITOR
        // ������ ����� �� ��������� �� ��� ����
        Debug.DrawLine(enemyUnit.transform.position, enemyUnit.NavMeshAgent.destination, Color.yellow);
#endif
        // ������ �������� ��������
        enemyUnit.Animator.SetFloat(HashAnimStringEnemy.Speed, enemyUnit.NavMeshAgent.velocity.magnitude / (enemyUnit.MovementSpeed.Max/100f));
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// ���� ��������� � ������� ��������� ����� ����� ������
    /// </summary>
    protected void MoveToRandomPointNearTarget()
    {
        // �������� ��������� ��������� ���������
        enemyUnit.NavMeshAgent.stoppingDistance = 0f;
        // ��������� ���� ���������� �� ����� ��������� ����� ����� � �������
        enemyUnit?.NavMeshAgent?.SetDestination(_positionRandomPointNearTarget);
    }

    /// <summary>
    /// ���� ��������� � ������� ������
    /// </summary>
    protected void MoveToTarget()
    {
        // �������� ��������� ��������� ���������
        enemyUnit.NavMeshAgent.stoppingDistance = enemyUnit.AttackDistance - 0.5f;
        // ��������� ���� ���������� �� ������
        enemyUnit.NavMeshAgent.SetDestination(enemyUnit.TargetUnit.transform.position);
    }

    /// <summary>
    /// ����� ��������� ��������� ����� �� ������� ����� � �������
    /// </summary>
    /// <returns>������� ��������� �����</returns>
    protected void GenerateRandomPointNearTarget()
    {
        NavMeshHit navMeshHit;
        Vector3 randomPoint = Vector3.zero;

        bool isPathComplite = false;

        // TODO ��������������, ��������� �� �����
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

