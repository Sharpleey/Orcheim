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
    protected Vector3 _positionRandomPointNearPlayer;

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

        // �������� Transform ������ ��� ������������ ��� �������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // �������� ��������� ����� � ������������ ������� (_randomPointRadius) ����� � �������
        GenerateRandomPointNearPlayer();

        // ������� ���������� � ��������� �����
        MoveToRandomPointNearPlayer();
    }

    public override void Update()
    {
        _timerUpdateDistance += Time.deltaTime;
        _timerAudioPlayback += Time.deltaTime;

        if (_timerUpdateDistance > 0.5f)
        {
            distanceEnemyToPlayer = Vector3.Distance(enemyUnit.transform.position, transformPlayer.position);
            _distanceRandomPointToPlayer = Vector3.Distance(_positionRandomPointNearPlayer, transformPlayer.position);

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

        if(_timerAudioPlayback >= _soundFrequency)
        {
            // ������������� ����
            if (enemyUnit.AudioController)
                enemyUnit.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Agro);

            // �������� ������
            _timerAudioPlayback = 0;
        }    

        // ������ ����� �� ��������� �� ��� ����
        //Debug.DrawLine(enemy.transform.position, enemy.NavMeshAgent.destination, Color.yellow);

        // ������ �������� ��������
        enemyUnit.Animator.SetFloat(HashAnimStringEnemy.Speed, enemyUnit.NavMeshAgent.velocity.magnitude / (enemyUnit.MovementSpeed.Max/100f));
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// ���� ��������� � ������� ��������� ����� ����� ������
    /// </summary>
    protected void MoveToRandomPointNearPlayer()
    {
        // �������� ��������� ��������� ���������
        enemyUnit.NavMeshAgent.stoppingDistance = 0f;
        // ��������� ���� ���������� �� ����� ��������� ����� ����� � �������
        enemyUnit?.NavMeshAgent?.SetDestination(_positionRandomPointNearPlayer);
    }

    /// <summary>
    /// ���� ��������� � ������� ������
    /// </summary>
    protected void MoveToPlayer()
    {
        // �������� ��������� ��������� ���������
        enemyUnit.NavMeshAgent.stoppingDistance = enemyUnit.AttackDistance - 0.5f;
        // ��������� ���� ���������� �� ������
        enemyUnit.NavMeshAgent.SetDestination(transformPlayer.position);
    }

    /// <summary>
    /// ����� ��������� ��������� ����� �� ������� ����� � �������
    /// </summary>
    /// <returns>������� ��������� �����</returns>
    protected void GenerateRandomPointNearPlayer()
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
}   

