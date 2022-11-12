public class InspirationState : State
{
    public InspirationState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.NavMeshAgent.isStopped = true;

        // ¬ключаем анимацию
        enemy.Animator.SetTrigger(HashAnimString.IsInspiration);

        enemy.IsBlockChangeState = true;
    }

    public override void Update()
    {
        if (!enemy.IsBlockChangeState)
            enemy.SetState<ChasingPlayerState>();
    }

    public override void Exit()
    {
        enemy.NavMeshAgent.isStopped = false;
    }
}
