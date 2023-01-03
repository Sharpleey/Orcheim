using UnityEngine;

public class WarriorAttackState : EnemyState
{
    //TODO �������� ��� ��������� � ������� Enum, ���������� ����� ����� ���������. ������� ��� ������������� ��� ������ �� ���� � ������� ��������

    /// <summary>
    /// ���-�� ��������� ����
    /// </summary>
    private int _attackVariantCount = 5;

    public WarriorAttackState(EnemyUnit enemyUnit) : base(enemyUnit)
    {
    }

    public override void Enter()
    {
        enemyUnit.NavMeshAgent.isStopped = true;

        enemyUnit.Animator.SetInteger(HashAnimStringEnemy.AttackVariant, Random.Range(0, _attackVariantCount));
        enemyUnit.Animator.SetTrigger(HashAnimStringEnemy.IsAttack);

        enemyUnit.Animator.speed = enemyUnit.AttackSpeed.Actual / 100f;
    }

    public override void Exit()
    {
        enemyUnit.NavMeshAgent.isStopped = false;

        enemyUnit.Animator.speed = 1f;
    }
}
