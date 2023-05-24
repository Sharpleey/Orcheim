
public class Flame : Effect
{
    #region Properties

    public override string Name => HashEffectString.FLAME_NAME;
    public override string Description => string.Format(HashEffectString.FLAME_DESCRIPTION, Frequency, Duration.Value, DamagePerSecond.Max, ArmorDecrease.Value);
    public Damage DamagePerSecond { get; private set; }
    public Parameter ArmorDecrease { get; private set; }

    #endregion Properties

    #region Methods

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
        DamagePerSecond = new Damage(
            defaultValue: damageFlame,
            increaseValuePerLevel: increaseDamageFlamePerLevel,
            level: levelDamageFlame,
            damageType: DamageType.Fire);

        ArmorDecrease = new Parameter(
            defaultValue: armorDecrease,
            changeValuePerLevel: increaseArmorDecreasePerLevel,
            level: levelArmorDecrease);

        Duration = new Parameter(
            defaultValue: durationEffect,
            changeValuePerLevel: increaseDurationEffectPerLevel,
            level: levelDurationEffect);

        Type = EffectType.Negative;

        DamagePerSecond.UpgradeDescription = HashEffectString.FLAME_DAMAGE_UPGRADE_DESCRIPTION;
        ArmorDecrease.UpgradeDescription = HashEffectString.FLAME_ARMOR_DECREASE_UPGRADE_DESCRIPTION;
        Duration.UpgradeDescription = HashEffectString.FLAME_DURATION_UPGRADE_DESCRIPTION;
    }

    public override void Enable()
    {
        base.Enable();

        if (enemyUnit)
        {
            // �������� ������ ������� ��� �����������
            enemyUnit?.IconEffectsController?.EnableIcon<Flame>(true);

            // �������� ��������� ������ �������
            enemyUnit?.EnemyVisualEffectsController?.EnableEffect<Flame>(true);
        }

        if(player)
        {
            //TODO ����������� �������� ������� �� ������
        }
       
    }

    public override void Tick()
    {
        // ������ ������� ���� �� ���� ���
        unit.TakeDamage(DamagePerSecond.Actual, DamagePerSecond.Type);

        // ���������� ����� �� ���� ���
        unit.Armor.Actual -= ArmorDecrease.Value;
    }

    public override void Disable()
    {
        if (enemyUnit)
        {
            // ��������� ������ ������� ��� �����������
            enemyUnit?.IconEffectsController?.EnableIcon<Flame>(false);

            // ��������� ��������� ������ �������
            enemyUnit?.EnemyVisualEffectsController?.EnableEffect<Flame>(false);
        }

        if (player)
        {
            //TODO ����������� �������� ������� �� ������
        }
    }

    #endregion Methods
}
