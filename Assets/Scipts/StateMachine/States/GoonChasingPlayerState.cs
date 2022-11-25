using UnityEngine;
using UnityEngine.AI;

public class GoonChasingPlayerState : ChasingPlayerState
{
    /// <summary>
    /// ������ �������� ������� �������
    /// </summary>
    private float _timerCheckAlliesNearby;

    /// <summary>
    /// ����� ����� � ����� ����� Enemy, ������� �� ����� ������
    /// </summary>
    private LayerMask collisionMask = 4096;

    public GoonChasingPlayerState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // �������� ������
        _timerCheckAlliesNearby = 0;

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);
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
            if (countAlliesNearby > 2 && !((Goon)enemy).IsWarcryInCooldown)
            {
                enemy.SetState<InspirationState>();
            }

            // �������� ������
            _timerCheckAlliesNearby = 0;
        }

        // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
        if (distanceEnemyToPlayer < enemy.AttackDistance)
        {
            // �������� ��������� �� ��������� �����
            enemy.SetState<GoonAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }

    private int GetCountAlliesNearby()
    {
        Vector3 center = enemy.transform.position;
        float radius = 8;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, collisionMask);

        return hitColliders.Length;

    }
}
