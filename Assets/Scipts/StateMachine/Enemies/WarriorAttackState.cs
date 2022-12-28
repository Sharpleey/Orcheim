using UnityEngine;

public class WarriorAttackState : EnemyState
{
    //TODO �������� ��� ��������� � ������� Enum, ���������� ����� ����� ���������. ������� ��� ������������� ��� ������ �� ���� � ������� ��������

    /// <summary>
    /// ���-�� ��������� ����
    /// </summary>
    private int _attackVariantCount = 5;

    public WarriorAttackState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.NavMeshAgent.isStopped = true;

        enemy.Animator.SetInteger(HashAnimStringEnemy.AttackVariant, Random.Range(0, _attackVariantCount));
        enemy.Animator.SetTrigger(HashAnimStringEnemy.IsAttack);

        enemy.Animator.speed = enemy.AttackSpeed.Actual / 100f;
    }

    public override void Exit()
    {
        enemy.NavMeshAgent.isStopped = false;

        enemy.Animator.speed = 1f;
    }
}
