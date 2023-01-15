
/// <summary>
/// Класс модификатора атаки, позволяющий поджигать цели
/// </summary>
public class FlameAttack : ProcableAttackModifier
{
    public override string Name => "Поджигающая атака";
    public override string Description => $"Атаки c шансом {Сhance.Actual}% накладывают на цель эффект {Effect.Name}\n{Effect.Description}";

    

    /// <summary>
    /// Эффект, который накладывает модификатор на юнита
    /// </summary>
    public Flame Effect { get; private set; }

    /// <summary>
    /// Конструктор класса модификатора атаки 
    /// </summary>
    /// <param name="procChance">Шанс прока, шанс срабатывания модификатора</param>
    /// <param name="increaseProcChancePerLevel">Прирост шанса за уровень улучшения параметра</param>
    /// <param name="levelProcChance">Наначальный уровень параметра</param>
    /// <param name="damageFlame"></param>
    /// <param name="increaseDamageFlamePerLevel"></param>
    /// <param name="levelDamageFlame"></param>
    /// <param name="armorDecrease"></param>
    /// <param name="increaseArmorDecreasePerLevel"></param>
    /// <param name="levelArmorDecrease"></param>
    /// <param name="durationEffect"></param>
    /// <param name="increaseDurationEffectPerLevel"></param>
    /// <param name="levelDurationEffect"></param>
    public FlameAttack(
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, 
        int damageFlame = 10, int increaseDamageFlamePerLevel = 2, int levelDamageFlame = 1, 
        int armorDecrease = 1, int increaseArmorDecreasePerLevel = 1, int levelArmorDecrease = 1, 
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        Сhance = new ProcСhance(defaultValue: procChance, increaseValuePerLevel: increaseProcChancePerLevel, level: levelProcChance);

        Effect = new Flame( //TODO Реализовать через конфиги
            damageFlame: damageFlame, increaseDamageFlamePerLevel: increaseDamageFlamePerLevel, levelDamageFlame: levelDamageFlame,
            armorDecrease: armorDecrease, increaseArmorDecreasePerLevel: increaseArmorDecreasePerLevel, levelArmorDecrease: levelArmorDecrease,
            durationEffect: durationEffect, increaseDurationEffectPerLevel: increaseDurationEffectPerLevel, levelDurationEffect: levelDurationEffect);

        Сhance.UpgradeDescription = $"Шанс наложить эффект +{Сhance.IncreaseValuePerLevel}% (Текущий {Сhance.Max}%)";
    }
}
