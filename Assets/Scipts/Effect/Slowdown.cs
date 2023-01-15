
public class Slowdown : Effect
{
    public override string Name => "Замедление";
    public override string Description => $"Эффект замедляет цель на ";

    public Parameter MovementSpeedPercentageDecrease { get; set; }
    public Parameter AttackSpeedPercentageDecrease { get; set; }

    public Slowdown(
        int defaultMovementSpeedPercentageDecrease = 20, int increaseMovementSpeedPercentageDecrease = 5, int levelMovementSpeedPercentageDecrease = 1,
        int defaultAttackSpeedPercentageDecrease = 20, int increaseAttackSpeedPercentageDecrease = 5, int levelAttackSpeedPercentageDecrease = 1,
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        MovementSpeedPercentageDecrease = new Parameter(
            defaultValue: defaultMovementSpeedPercentageDecrease,
            increaseValuePerLevel: increaseMovementSpeedPercentageDecrease,
            maxLevel: 10,
            level: levelMovementSpeedPercentageDecrease);

        AttackSpeedPercentageDecrease = new Parameter(
            defaultValue: defaultAttackSpeedPercentageDecrease,
            increaseValuePerLevel: increaseAttackSpeedPercentageDecrease,
            maxLevel: 10,
            level: levelAttackSpeedPercentageDecrease);

        Duration = new Parameter(
            defaultValue: durationEffect,
            increaseValuePerLevel: increaseDurationEffectPerLevel,
            level: levelDurationEffect);

        EffectType = EffectType.Negative;

        MovementSpeedPercentageDecrease.UpgradeDescription = $"Замедление скорости передвижения цели +{MovementSpeedPercentageDecrease.IncreaseValuePerLevel}% (Текущее {MovementSpeedPercentageDecrease.Value}%)";
        AttackSpeedPercentageDecrease.UpgradeDescription = $"Замедление скорости атаки цели +{AttackSpeedPercentageDecrease.IncreaseValuePerLevel}% (Текущее {AttackSpeedPercentageDecrease.Value}%)";
        Duration.UpgradeDescription = $"Длительность эффекта {Name} +{Duration.IncreaseValuePerLevel} сек. (Текущая {Duration.Value})";
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

            if(enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconSlowdown(true);
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

            if (enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconSlowdown(false);

        }

        if (player)
        {
            //TODO Реализовать действие эффекта на игрока
        }
    }
}
