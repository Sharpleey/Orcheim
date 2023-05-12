using UnityEngine;

public class GoonChasingState : ChasingState
{
    /// <summary>
    /// ������ �������� ������� �������
    /// </summary>
    private float _timerCheckAlliesNearby;

    public GoonChasingState(EnemyUnit enemyUnit) : base(enemyUnit)
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

        if (_timerCheckAlliesNearby > 2f)
        {
            // �������� ���-�� ������� ������� ����������
            int countAlliesNearby = GetCountAlliesNearby(((Goon)enemyUnit).Warcry.Radius.Value);

            // ���� �� ������ 2�, �� ������� ���
            if (countAlliesNearby > 2 && !((Goon)enemyUnit).Warcry.IsCooldown)
            {
                enemyUnit.SetState<InspirationState>();
            }

            // �������� ������
            _timerCheckAlliesNearby = 0;
        }

        // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
        if (distanceEnemyToTarget < enemyUnit.AttackDistance)
        {
            // �������� ��������� �� ��������� �����
            enemyUnit.SetState<GoonAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }
}
