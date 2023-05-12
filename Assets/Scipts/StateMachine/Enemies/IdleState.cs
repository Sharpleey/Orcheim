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

        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdle, true);

        // �������� ������� ���������� �� ������ ��������� ����
        enemyUnit.SummonTrigger?.SetEnable(true);
    }

    public override void Update()
    {
        _timerUpdate += Time.deltaTime;

        if (_timerUpdate > 0.5f && enemyUnit.TargetUnit != null)
        {
            distanceEnemyToTarget = Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position);

            // ������ ���������� �� ��������������, ���� (����� � ���� ���������� ��������� ���������) ��� (�������� ���������� ������ ������ ����� �����)
            if (distanceEnemyToTarget < _absoluteDetectionDistance || IsTargetInSight())
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
    private bool IsTargetInSight()
    {
        float realAngle = Vector3.Angle(enemyUnit.transform.forward, enemyUnit.TargetUnit.transform.position - enemyUnit.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position - enemyUnit.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position) <= _viewDetectionDistance && hit.transform == enemyUnit.TargetUnit.transform)
            {
                return true;
            }
        }
        return false;
    }

    #endregion Private methods
}
