
public class WarriorChasingState : ChasingState
{
    public WarriorChasingState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // ¬ключаем анимацию дл€ этого состо€ни€, задаем параметр анимации
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);
    }

    public override void Update()
    {
        base.Update();

        // ≈сли противник подошел на дистанцию атаки (_attackDistance), то измен€ем состо€ние
        if (distanceEnemyToPlayer < enemyUnit.AttackDistance)
        {
            // »змен€ем состо€ние на состо€ние атаки
            enemyUnit.SetState<WarriorAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // «адаем параметр анимации, выключаем анимацию дл€ этого состо€ни€
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }
}
