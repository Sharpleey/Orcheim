using UnityEngine;

/// <summary>
///  ����� ��������� ����� ����������
/// </summary>
public class IdleState : EnemyState
{
    /// <summary>
    /// ���� ������ ����
    /// </summary>
    protected float _viewAngleDetection = 120f;

    /// <summary>
    /// ��������� ������ �����
    /// </summary>
    protected float _viewDetectionDistance = 14f;

    /// <summary>
    /// ������ �����������, ��� ������� � ����� ������ ���� ������� ������
    /// </summary>
    protected float _absoluteDetectionDistance = 4f;
    
    /// <summary>
    /// ������ ����������
    /// </summary>
    protected float _timerUpdate;

    public IdleState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }

    public override void Enter()
    {
        _timerUpdate = 0.5f;

        // �������� Transform ������ ��� ������������ ��� �������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdle, true);

        // �������� ������� ���������� �� ������ ��������� ������
        enemyUnit.SummonTrigger?.SetEnable(true);
    }

    public override void Update()
    {
        _timerUpdate += Time.deltaTime;

        if (_timerUpdate > 0.5f)
        {
            // �� ������, ����� ����� ��� �� �����������
            if (!transformPlayer)
            {
                transformPlayer = GetTransformPlayer();
                return;
            }

            distanceEnemyToPlayer = Vector3.Distance(enemyUnit.transform.position, transformPlayer.position);

            // ������ ���������� �� ��������������, ���� (����� � ���� ���������� ��������� ���������) ��� (�������� ���������� ������ ������ ����� �����)
            if (distanceEnemyToPlayer < _absoluteDetectionDistance || IsPlayerInSight())
            {
                // ������������� ����
                enemyUnit?.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.Confused);

                enemyUnit.SetState<ChasingState>();
            }

            // �������� ������
            _timerUpdate = 0;
        }
    }

    public override void Exit()
    {
        enemyUnit?.Animator?.SetBool(HashAnimStringEnemy.IsIdle, false);

        // ��������� ������� ���������� �� ������ ��������� ����� ������� ������� ������
        enemyUnit.SummonTrigger?.SetEnable(false);
    }

    #region Private methods

    /// <summary>
    /// ����� ��������� ��������� �� ����� � ���� ������ �����
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerInSight()
    {
        float realAngle = Vector3.Angle(enemyUnit.transform.forward, transformPlayer.position - enemyUnit.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(enemyUnit.transform.position, transformPlayer.position - enemyUnit.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(enemyUnit.transform.position, transformPlayer.position) <= _viewDetectionDistance && hit.transform == transformPlayer.transform)
            {
                return true;
            }
        }
        return false;
    }

    #endregion Private methods
}
