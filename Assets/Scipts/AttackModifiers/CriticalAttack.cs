
/// <summary>
/// Класс модификатора атаки, позволяющий наносить критический урон целям
/// </summary>
public class CriticalAttack : ProcableAttackModifier
{
    public override string Name => "Критическая атака";
    public override string Description => $"Атаки c шансом {Сhance.Value}% могут нанести {DamageMultiplier.Value}% урона";

    /// <summary>
    /// Множитель урона критической атаки
    /// </summary>
    public Parameter DamageMultiplier { get; protected set; }

    public CriticalAttack(
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1,
        int defaultValueDamageMultiplier = 150, int increaseDamageMultiplierPerLevel= 10, int levelDamageMultiplier = 1)
    {
        Сhance = new Parameter(defaultValue: procChance, increaseValuePerLevel: increaseProcChancePerLevel, level: levelProcChance, maxLevel: 18);
        DamageMultiplier = new Parameter(defaultValue: defaultValueDamageMultiplier, increaseValuePerLevel: increaseDamageMultiplierPerLevel, level: levelDamageMultiplier);

        Сhance.UpgradeDescription = $"Шанс нанести {DamageMultiplier.Value}% урона  +{Сhance.IncreaseValuePerLevel}% (Текущий {Сhance.Value}%)";
        DamageMultiplier.UpgradeDescription = $"Множитель критического урона  +{DamageMultiplier.IncreaseValuePerLevel}% (Текущий {DamageMultiplier.Value}%)";
    }
}
