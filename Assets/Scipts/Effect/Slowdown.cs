
public class Slowdown : Effect
{
    public override string Name => "Замедление";
    public override string Description => $"Эффект замедляет цель на ";

    public ParameterModifier MovementSpeedPercentageDecrease { get; set; }
    public ParameterModifier AttackSpeedPercentageDecrease { get; set; }

    public Slowdown(
        int defaultMovementSpeedPercentageDecrease = 20, int increaseMovementSpeedPercentageDecrease = 5, int levelMovementSpeedPercentageDecrease = 1,
        int defaultAttackSpeedPercentageDecrease = 20, int increaseAttackSpeedPercentageDecrease = 5, int levelAttackSpeedPercentageDecrease = 1,
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        MovementSpeedPercentageDecrease = new ParameterModifier(
            valueOfModify: defaultMovementSpeedPercentageDecrease,
            parameterModifierType: ParameterModifierType.Decrease,
            increaseValuePerLevel: increaseMovementSpeedPercentageDecrease,
            maxLevel: 10,
            level: levelMovementSpeedPercentageDecrease);

        AttackSpeedPercentageDecrease = new ParameterModifier(
            valueOfModify: defaultAttackSpeedPercentageDecrease,
            parameterModifierType: ParameterModifierType.Decrease,
            increaseValuePerLevel: increaseAttackSpeedPercentageDecrease,
            maxLevel: 10,
            level: levelAttackSpeedPercentageDecrease);

        Duration = new Duration(
            defaultValue: durationEffect,
            increaseValuePerLevel: increaseDurationEffectPerLevel,
            level: levelDurationEffect);

        EffectType = EffectType.Negative;

        MovementSpeedPercentageDecrease.UpgradeDescription = $"Замедление скорости передвижения цели +{MovementSpeedPercentageDecrease.IncreaseValuePerLevel}% (Текущее {MovementSpeedPercentageDecrease.ValueOfModify}%)";
        AttackSpeedPercentageDecrease.UpgradeDescription = $"Замедление скорости атаки цели +{AttackSpeedPercentageDecrease.IncreaseValuePerLevel}% (Текущее {AttackSpeedPercentageDecrease.ValueOfModify}%)";
        Duration.UpgradeDescription = $"Длительность эффекта {Name} +{Duration.IncreaseValuePerLevel} сек. (Текущая {Duration.Max})";
    }

    public override void Enable()
    {
        base.Enable();

        unit.MovementSpeed.Actual = (int)(unit.MovementSpeed.Max * (1f - (MovementSpeedPercentageDecrease.ValueOfModify/100f)));
        unit.AttackSpeed.Actual = (int)(unit.AttackSpeed.Max * (1f - (AttackSpeedPercentageDecrease.ValueOfModify/100f)));

        if (enemyUnit)
        {
            if(enemyUnit.NavMeshAgent)
                enemyUnit.NavMeshAgent.speed = unit.MovementSpeed.Actual / 100f;

            if(enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconSlowdown(true);
        }

        if (playerUnit)
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

        if (playerUnit)
        {
            //TODO Реализовать действие эффекта на игрока
        }
    }
}
