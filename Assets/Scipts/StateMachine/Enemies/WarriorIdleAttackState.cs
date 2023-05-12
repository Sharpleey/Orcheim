using UnityEngine;

/// <summary>
/// ����� ��������� ����� ���������. � ���� ��������� ��������� �������� �������� ������ ����� ������ � ��������� ����� 
/// </summary>
public class WarriorIdleAttackState : EnemyState
{  
    /// <summary>
    /// ������� �����, �.�. �������� ����� ������� � ��������
    /// </summary>
    private float _attackFrequency = 3.5f;
    
    /// <summary>
    /// �������� �������� ����� � ����
    /// </summary>
    private float _rotationSpeedToTarget = 2.5f;

    /// <summary>
    /// ������ �������� ����� �������
    /// </summary>
    private float _timerAttack;
    
    /// <summary>
    /// ������ ����� ������������ ��������
    /// </summary>
    private float _timerUpdateDistance;

    public WarriorIdleAttackState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }
    public override void Enter()
    {
        // �������� �������
        _timerAttack = 0;
        _timerUpdateDistance = 0.5f;

        // ���������� ������� ���� � ����������� �� �������� �����
        _attackFrequency = 3.5f /  (enemyUnit.AttackSpeed.Actual / 100f);

        // ���������� ��������� �� ������
        distanceEnemyToTarget = Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position);

        // �������� ��������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, true);
    }

    public override void Update()
    {
        // ����� � ������������ ��������
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _attackFrequency)
        {
            enemyUnit.SetState<WarriorAttackState>();
        }
        // -------------------------------------------------------------------------------


        // ������ ������� ���������� ��������� �� ������ ����, � ��� � ��� �������
        // -------------------------------------------------------------------------------
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            // ���������� ��������� �� ������
            distanceEnemyToTarget = Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position);
            if (distanceEnemyToTarget > enemyUnit.AttackDistance)
            {
                enemyUnit.SetState<ChasingState>();
            }

            _timerUpdateDistance = 0;
        }
        // -------------------------------------------------------------------------------

        LookAtTarget();
    }

    public override void Exit()
    {
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, false);
    }

    /// <summary>
    /// ����� ������ ������������ � ������������ ��������� ����� � ������
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 direction = -(enemyUnit.transform.position - enemyUnit.TargetUnit.transform.position);
        enemyUnit.transform.rotation = Quaternion.Lerp(enemyUnit.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
