
public class Slowdown : Effect, IMovementSpeedPercentageDecrease, IAttackSpeedPercentageDecrease
{
    public override string Name => "Замедление";

    public override string Description => "Эффект замедляет персонажа на";

    public override EffectType EffectType => EffectType.Negative;
    public override float Duration { get; set; } = 3;
    public float MovementSpeedPercentageDecrease { get; set; } = 0.2f;
    public float AttackSpeedPercentageDecrease { get; set; } = 0.2f;

    public override void Enable()
    {
        unit.MovementSpeed.Actual = (int)(unit.MovementSpeed.Max * (1f - MovementSpeedPercentageDecrease));
        unit.AttackSpeed.Actual = (int)(unit.AttackSpeed.Max * (1f - AttackSpeedPercentageDecrease));


        EnemyUnit? enemyUnit = unit as EnemyUnit;

        if(enemyUnit)
        {
            if(enemyUnit.NavMeshAgent)
                enemyUnit.NavMeshAgent.speed = unit.MovementSpeed.Actual / 100f;

            if(enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconSlowdown(true);

        }


    }

    public override void Disable()
    {
        unit.MovementSpeed.Actual = unit.MovementSpeed.Max;
        unit.AttackSpeed.Actual = unit.AttackSpeed.Max;

        EnemyUnit? enemyUnit = unit as EnemyUnit;

        if (enemyUnit)
        {
            if (enemyUnit.NavMeshAgent)
                enemyUnit.NavMeshAgent.speed = unit.MovementSpeed.Actual / 100f;

            if (enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconSlowdown(false);

        }
    }
}
