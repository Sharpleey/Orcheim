
public class Flame : Effect
{
    public override string Name => HashEffectString.FLAME_NAME;
    public override string Description => string.Format(HashEffectString.FLAME_DESCRIPTION, Frequency, Duration.Value, Damage.Max, ArmorDecrease.Value);
    public Damage Damage { get; private set; }
    public Parameter ArmorDecrease { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damageFlame">Урон от горения за тик</param>
    /// <param name="increaseDamageFlamePerLevel">Прирост урона от горения за уровень</param>
    /// <param name="armorDecrease">Уменьшение брони юнита за тик</param>
    /// <param name="increaseArmorDecreasePerLevel">Прирост значения уменьшения брони за уровень</param>
    /// <param name="durationEffect">Длительность эффекта в секундах</param>
    /// <param name="increaseDurationEffectPerLevel">Прирост длительности за уровень улучшения параметра</param>
    public Flame(
        int damageFlame = 10, int increaseDamageFlamePerLevel = 2, int levelDamageFlame = 1, 
        int armorDecrease = 1, int increaseArmorDecreasePerLevel = 1, int levelArmorDecrease = 1, 
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        Damage = new Damage(
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

        EffectType = EffectType.Negative;

        Damage.UpgradeDescription = HashEffectString.FLAME_DAMAGE_UPGRADE_DESCRIPTION;
        ArmorDecrease.UpgradeDescription = HashEffectString.FLAME_ARMOR_DECREASE_UPGRADE_DESCRIPTION;
        Duration.UpgradeDescription = HashEffectString.FLAME_DURATION_UPGRADE_DESCRIPTION;
    }

    public override void Enable()
    {
        base.Enable();

        if (enemyUnit)
        {
            // Включаем иконку горения над противником
            enemyUnit?.IconEffectsController?.EnableIcon<Flame>(true);

            // Включаем визульный эффект горения
            if (enemyUnit.BurningEffectController)
                enemyUnit.BurningEffectController.enabled = true;
        }

        if(player)
        {
            //TODO Реализовать действие эффекта на игрока
        }
       
    }

    public override void Tick()
    {
        // Эффект наносит урон за один тик
        unit.TakeDamage(Damage.Actual, Damage.Type);

        // Уменьшение брони за один тик
        unit.Armor.Actual -= ArmorDecrease.Value;
    }

    public override void Disable()
    {
        if (enemyUnit)
        {
            // Выключаем иконку горения над противником
            enemyUnit?.IconEffectsController?.EnableIcon<Flame>(false);

            // Выключаем визульный эффект горения
            if (enemyUnit.BurningEffectController)
                enemyUnit.BurningEffectController.enabled = false;
        }

        if (player)
        {
            //TODO Реализовать действие эффекта на игрока
        }
    }
}
