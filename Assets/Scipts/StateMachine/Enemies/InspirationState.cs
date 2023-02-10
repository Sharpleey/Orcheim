public class InspirationState : EnemyState
{
    public InspirationState(EnemyUnit enemyUnit) : base(enemyUnit)
    {
    }

    public override void Enter()
    {
        enemyUnit.NavMeshAgent.isStopped = true;

        // �������� ��������
        enemyUnit?.Animator?.SetTrigger(HashAnimStringEnemy.IsInspiration);

        // ����
        enemyUnit?.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.UsingAbility);
    }

    public override void Exit()
    {
        enemyUnit.NavMeshAgent.isStopped = false;
    }
}
