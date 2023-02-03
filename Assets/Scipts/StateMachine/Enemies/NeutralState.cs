
public class NeutralState : EnemyState
{
    public NeutralState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }

    public override void Enter()
    {
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdle, true);
    }
}
