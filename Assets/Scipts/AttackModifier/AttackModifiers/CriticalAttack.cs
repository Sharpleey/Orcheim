
/// <summary>
///  ласс модификатора атаки, позвол€ющий наносить критический урон цел€м
/// </summary>
public class CriticalAttack : ProcableAttackModifier
{
    public override string Name => HashAttackModString.CRITICAL_ATTACK_NAME;
    public override string Description => string.Format(HashAttackModString.CRITICAL_ATTACK_DESCRIPTION, Chance.Value, DamageMultiplier.Value);  

    /// <summary>
    /// ћножитель урона критической атаки
    /// </summary>
    public Parameter DamageMultiplier { get; protected set; }

    public CriticalAttack(bool isActive = false,
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, int maxLevelProcChance = 18,
        int defaultValueDamageMultiplier = 150, int increaseDamageMultiplierPerLevel= 10, int levelDamageMultiplier = 1): base(isActive, procChance, increaseProcChancePerLevel, levelProcChance, maxLevelProcChance)
    {
        DamageMultiplier = new Parameter(defaultValue: defaultValueDamageMultiplier, changeValuePerLevel: increaseDamageMultiplierPerLevel, level: levelDamageMultiplier);

        Chance.UpgradeDescription = HashAttackModString.CRITICAL_ATTACK_CHANCE_UPGRADE_DESCRIPTION;
        DamageMultiplier.UpgradeDescription = HashAttackModString.CRITICAL_ATTACK_DAMAGEMULTIPLY_UPGRADE_DESCRIPTION;
    }
}
