public class Warrior : EnemyUnit
{
    public override void InitStates()
    {
        base.InitStates();

        States[typeof(ChasingState)] = new WarriorChasingState(this);
        States[typeof(WarriorIdleAttackState)] = new WarriorIdleAttackState(this);
        States[typeof(WarriorAttackState)] = new WarriorAttackState(this);
    }

    /// <summary>
    /// Метод для смены состояния при срабатывании события по окончанию анимации атаки.
    /// </summary>
    private void SetIdleAttackState()
    {
        SetState<WarriorIdleAttackState>();
    }
}