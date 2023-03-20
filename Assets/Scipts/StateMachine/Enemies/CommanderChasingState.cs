using UnityEngine;

public class CommanderChasingState : ChasingState
{
    /// <summary>
    /// ������ �������� ������� �������
    /// </summary>
    private float _timerCheckAlliesNearby;

    public CommanderChasingState(EnemyUnit enemyUnit) : base(enemyUnit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // �������� ������
        _timerCheckAlliesNearby = 0;

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);
    }

    public override void Update()
    {
        base.Update();

        _timerCheckAlliesNearby += Time.deltaTime;

        if (_timerCheckAlliesNearby > 3f)
        {
            // �������� ���-�� ������� ������� ����������
            int countAlliesNearby = GetCountAlliesNearby(((Commander)enemyUnit).Inspiration.Radius.Value);

            // ���� �� ������ 2�, �� ������� ���
            if (countAlliesNearby > 2 && !((Commander)enemyUnit).Inspiration.IsCooldown)
            {
                enemyUnit.SetState<InspirationState>();
            }

            // �������� ������
            _timerCheckAlliesNearby = 0;
        }

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

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }
}
