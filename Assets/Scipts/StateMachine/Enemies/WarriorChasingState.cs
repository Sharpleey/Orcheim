
public class WarriorChasingState : ChasingState
{
    public WarriorChasingState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);
    }

    public override void Update()
    {
        base.Update();

        // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
        if (distanceEnemyToPlayer < enemyUnit.AttackDistance)
        {
            // �������� ��������� �� ��������� �����
            enemyUnit.SetState<WarriorAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // ������ �������� ��������, ��������� �������� ��� ����� ���������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }
}
