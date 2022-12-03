public class InspirationState : EnemyState
{
    public InspirationState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.NavMeshAgent.isStopped = true;

        // �������� ��������
        enemy.Animator.SetTrigger(HashAnimStringEnemy.IsInspiration);
    }

    public override void Exit()
    {
        enemy.NavMeshAgent.isStopped = false;
    }
}
