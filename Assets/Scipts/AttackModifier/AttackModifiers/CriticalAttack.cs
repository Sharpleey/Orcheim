
/// <summary>
/// ����� ������������ �����, ����������� �������� ����������� ���� �����
/// </summary>
public class CriticalAttack : ProcableAttackModifier
{
    public override string Name => HashAttackModString.CRITICAL_ATTACK_NAME;
    public override string Description => string.Format(HashAttackModString.CRITICAL_ATTACK_DESCRIPTION, Chance.Value, DamageMultiplier.Value * 100);  

    /// <summary>
    /// ��������� ����� ����������� �����
    /// </summary>
    public Multiplier DamageMultiplier { get; protected set; }

    public CriticalAttack(bool isActive = false,
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, int maxLevelProcChance = 18,
        float defaultValueDamageMultiplier = 1.5f, float increaseDamageMultiplierPerLevel= 0.25f, int levelDamageMultiplier = 1): base(isActive, procChance, increaseProcChancePerLevel, levelProcChance, maxLevelProcChance)
    {
        DamageMultiplier = new Multiplier(defaultValue: defaultValueDamageMultiplier, changeValuePerLevel: increaseDamageMultiplierPerLevel, level: levelDamageMultiplier);

        Chance.UpgradeDescription = HashAttackModString.CRITICAL_ATTACK_CHANCE_UPGRADE_DESCRIPTION;
        DamageMultiplier.UpgradeDescription = HashAttackModString.CRITICAL_ATTACK_DAMAGEMULTIPLY_UPGRADE_DESCRIPTION;
    }
}
