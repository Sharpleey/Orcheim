using UnityEngine;
using UnityEngine.AI;

public class GoonChasingState : ChasingState
{
    /// <summary>
    /// ������ �������� ������� �������
    /// </summary>
    private float _timerCheckAlliesNearby;

    /// <summary>
    /// ����� ����� � ����� ����� Enemy, ������� �� ����� ������
    /// </summary>
    private LayerMask collisionMask = 4096;

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

        if (_timerCheckAlliesNearby > 3f)
        {
            // �������� ���-�� ������� ������� ����������
            int countAlliesNearby = GetCountAlliesNearby();

            // ���� �� ������ 2�, �� ������� ���
            if (countAlliesNearby > 2 && !((Goon)enemyUnit).IsWarcryInCooldown)
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
            enemyUnit.SetState<GoonAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }

    private int GetCountAlliesNearby()
    {
        Vector3 center = enemyUnit.transform.position;
        float radius = 8;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, collisionMask);

        return hitColliders.Length;

    }
}
