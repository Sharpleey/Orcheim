public class InspirationState : EnemyState
{
    public InspirationState(EnemyUnit enemyUnit) : base(enemyUnit)
    {
    }

    public override void Enter()
    {
        enemyUnit.NavMeshAgent.isStopped = true;

        // ¬ключаем анимацию
        enemyUnit?.Animator?.SetTrigger(HashAnimStringEnemy.IsInspiration);

        // «вук
        enemyUnit?.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.UsingAbility);
    }

    public override void Exit()
    {
        enemyUnit.NavMeshAgent.isStopped = false;
    }
}
