
/// <summary>
/// Класс модификатора атаки, позволяющий поджигать цели
/// </summary>
public class FlameAttack : ProcableAttackModifier
{
    public override string Name => HashAttackModString.FLAME_ATTACK_NAME;
    public override string Description => string.Format(HashAttackModString.FLAME_ATTACK_DESCRIPTION, Chance.Value, Effect.Name, Effect.Description);

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
    public FlameAttack(bool isActive = true,
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, int maxLevelProcChance = 18,
        int damageFlame = 10, int increaseDamageFlamePerLevel = 2, int levelDamageFlame = 1, 
        int armorDecrease = 1, int increaseArmorDecreasePerLevel = 1, int levelArmorDecrease = 1, 
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1) : base(isActive, procChance, increaseProcChancePerLevel, levelProcChance, maxLevelProcChance)
    {
        Effect = new Flame( //TODO Реализовать через конфиги
            damageFlame: damageFlame, increaseDamageFlamePerLevel: increaseDamageFlamePerLevel, levelDamageFlame: levelDamageFlame,
            armorDecrease: armorDecrease, increaseArmorDecreasePerLevel: increaseArmorDecreasePerLevel, levelArmorDecrease: levelArmorDecrease,
            durationEffect: durationEffect, increaseDurationEffectPerLevel: increaseDurationEffectPerLevel, levelDurationEffect: levelDurationEffect);

        Chance.UpgradeDescription = HashAttackModString.FLAME_ATTACK_CHANCE_UPGRADE_DESCRIPTION;
    }
}
