
public class Flame : Effect
{
    public override string Name => "�������";
    public override string Description => $"������, ������ {Frequency} ���. � ������� {Duration.Actual} ������ ������� {Damage.Max} �����, ������� ����� ���� �� {ArmorDecrease.ValueOfModify}";
    public Damage Damage { get; private set; }
    public ParameterModifier ArmorDecrease { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damageFlame">���� �� ������� �� ���</param>
    /// <param name="increaseDamageFlamePerLevel">������� ����� �� ������� �� �������</param>
    /// <param name="armorDecrease">���������� ����� ����� �� ���</param>
    /// <param name="increaseArmorDecreasePerLevel">������� �������� ���������� ����� �� �������</param>
    /// <param name="durationEffect">������������ ������� � ��������</param>
    /// <param name="increaseDurationEffectPerLevel">������� ������������ �� ������� ��������� ���������</param>
    public Flame(
        int damageFlame = 10, int increaseDamageFlamePerLevel = 2, int levelDamageFlame = 1, 
        int armorDecrease = 1, int increaseArmorDecreasePerLevel = 1, int levelArmorDecrease = 1, 
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        Damage = new Damage(
            defaultValue: damageFlame,
            increaseValuePerLevel: increaseDamageFlamePerLevel,
            level: levelDamageFlame);

        ArmorDecrease = new ParameterModifier(
            valueOfModify: armorDecrease,
            parameterModifierType: ParameterModifierType.Decrease,
            increaseValuePerLevel: increaseArmorDecreasePerLevel,
            level: levelArmorDecrease);

        Duration = new Duration(
            defaultValue: durationEffect,
            increaseValuePerLevel: increaseDurationEffectPerLevel,
            level: levelDurationEffect);

        EffectType = EffectType.Negative;

        Damage.UpgradeDescription = $"���� �� ��� +{Damage.IncreaseValuePerLevel} (������� {Damage.Max})";
        ArmorDecrease.UpgradeDescription = $"�������� ����� �� ��� +{ArmorDecrease.IncreaseValuePerLevel} (������� {ArmorDecrease.ValueOfModify})";
        Duration.UpgradeDescription = $"������������ ������� {Name} +{Duration.IncreaseValuePerLevel} ���. (������� {Duration.Max})";
    }

    public override void Enable()
    {
        base.Enable();

        if (enemyUnit)
        {
            // �������� ������ ������� ��� �����������
            if (enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconBurning(true);

            // �������� ��������� ������ �������
            if (enemyUnit.BurningEffectController)
                enemyUnit.BurningEffectController.enabled = true;
        }

        if(playerUnit)
        {
            //TODO ����������� �������� ������� �� ������
        }
       
    }

    public override void Tick()
    {
        unit.TakeDamage(Damage);
    }

    public override void Disable()
    {
        if (enemyUnit)
        {
            // ��������� ������ ������� ��� �����������
            if (enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconBurning(false);

            // ��������� ��������� ������ �������
            if (enemyUnit.BurningEffectController)
                enemyUnit.BurningEffectController.enabled = false;
        }

        if (playerUnit)
        {
            //TODO ����������� �������� ������� �� ������
        }
    }
}
