public class InspirationState : EnemyState
{
    public InspirationState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.NavMeshAgent.isStopped = true;

        // ¬ключаем анимацию
        enemy.Animator.SetTrigger(HashAnimStringEnemy.IsInspiration);

        enemy.IsBlockChangeState = true;
    }

    public override void Update()
    {
        if (!enemy.IsBlockChangeState)
            enemy.SetState<ChasingState>();
    }

    public override void Exit()
    {
        enemy.NavMeshAgent.isStopped = false;
    }
}
