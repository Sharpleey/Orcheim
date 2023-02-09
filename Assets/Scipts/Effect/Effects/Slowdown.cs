
public class Slowdown : Effect
{
    public override string Name => HashEffectString.SLOWDOWN_NAME;
    public override string Description => string.Format(HashEffectString.SLOWDOWN_DESCRIPTION, Duration.Value, AttackSpeedPercentageDecrease.Value, MovementSpeedPercentageDecrease.Value);

    public Parameter MovementSpeedPercentageDecrease { get; set; }
    public Parameter AttackSpeedPercentageDecrease { get; set; }

    public Slowdown(
        int defaultMovementSpeedPercentageDecrease = 20, int increaseMovementSpeedPercentageDecrease = 5, int levelMovementSpeedPercentageDecrease = 1,
        int defaultAttackSpeedPercentageDecrease = 20, int increaseAttackSpeedPercentageDecrease = 5, int levelAttackSpeedPercentageDecrease = 1,
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        MovementSpeedPercentageDecrease = new Parameter(
            defaultValue: defaultMovementSpeedPercentageDecrease,
            changeValuePerLevel: increaseMovementSpeedPercentageDecrease,
            maxLevel: 10,
            level: levelMovementSpeedPercentageDecrease);

        AttackSpeedPercentageDecrease = new Parameter(
            defaultValue: defaultAttackSpeedPercentageDecrease,
            changeValuePerLevel: increaseAttackSpeedPercentageDecrease,
            maxLevel: 10,
            level: levelAttackSpeedPercentageDecrease);

        Duration = new Parameter(
            defaultValue: durationEffect,
            changeValuePerLevel: increaseDurationEffectPerLevel,
            level: levelDurationEffect);

        EffectType = EffectType.Negative;

        MovementSpeedPercentageDecrease.UpgradeDescription = HashEffectString.SLOWDOWN_MOVEMENT_SPEED_PERCENTAGE_DECREASE_UPGRADE_DESCRIPTION;
        AttackSpeedPercentageDecrease.UpgradeDescription = HashEffectString.SLOWDOWN_ATTACK_SPEED_PERCENTAGE_DECREASE_UPGRADE_DESCRIPTION;
        Duration.UpgradeDescription = HashEffectString.SLOWDOWN_DURATION_UPGRADE_DESCRIPTION;
    }

    public override void Enable()
    {
        base.Enable();

        unit.MovementSpeed.Actual = (int)(unit.MovementSpeed.Max * (1f - (MovementSpeedPercentageDecrease.Value/100f)));
        unit.AttackSpeed.Actual = (int)(unit.AttackSpeed.Max * (1f - (AttackSpeedPercentageDecrease.Value/100f)));

        if (enemyUnit)
        {
            if(enemyUnit.NavMeshAgent)
                enemyUnit.NavMeshAgent.speed = unit.MovementSpeed.Actual / 100f;

            enemyUnit?.IconEffectsController?.EnableIcon<Slowdown>(true);
        }

        if (player)
        {
            //TODO Реализовать действие эффекта на игрока
        }
    }

    public override void Disable()
    {
        unit.MovementSpeed.Actual = unit.MovementSpeed.Max;
        unit.AttackSpeed.Actual = unit.AttackSpeed.Max;

        if (enemyUnit)
        {
            if (enemyUnit.NavMeshAgent)
                enemyUnit.NavMeshAgent.speed = unit.MovementSpeed.Actual / 100f;

            enemyUnit?.IconEffectsController?.EnableIcon<Slowdown>(false);

        }

        if (player)
        {
            //TODO Реализовать действие эффекта на игрока
        }
    }
}
