
public class Slowdown : Effect
{
    public override string Name => "����������";
    public override string Description => $"������ ��������� ���� �� ";

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

        MovementSpeedPercentageDecrease.UpgradeDescription = $"���������� �������� ������������ ���� +{MovementSpeedPercentageDecrease.IncreaseValuePerLevel}% (������� {MovementSpeedPercentageDecrease.ValueOfModify}%)";
        AttackSpeedPercentageDecrease.UpgradeDescription = $"���������� �������� ����� ���� +{AttackSpeedPercentageDecrease.IncreaseValuePerLevel}% (������� {AttackSpeedPercentageDecrease.ValueOfModify}%)";
        Duration.UpgradeDescription = $"������������ ������� {Name} +{Duration.IncreaseValuePerLevel} ���. (������� {Duration.Max})";
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
            //TODO ����������� �������� ������� �� ������
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
            //TODO ����������� �������� ������� �� ������
        }
    }
}
