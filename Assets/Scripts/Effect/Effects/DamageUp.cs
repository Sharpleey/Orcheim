
public class DamageUp : Effect
{
    public override string Name => HashEffectString.DAMAGEUP_NAME;
    public override string Description => HashEffectString.DAMAGEUP_DESCRIPTION;

    /// <summary>
    /// Процентное увелечение урона
    /// </summary>
    public Parameter PercentageDamageIncrease { get; private set; }

    /// <summary>
    /// Значение дополнительного урона от максимального значения урона юнита
    /// </summary>
    private float _extraDamage;

    public DamageUp(int duration, int defaultPercentageArmorIncrease, int increaseArmorIncreasePerLevel = 0, int level = 1)
    {
        Duration = new Parameter(duration);

        PercentageDamageIncrease = new Parameter(defaultValue: defaultPercentageArmorIncrease, changeValuePerLevel: increaseArmorIncreasePerLevel, level: level);

        Type = EffectType.Positive;
    }

    public override void Enable()
    {
        base.Enable();

        _extraDamage = unit.Damage.Max * (PercentageDamageIncrease.Value / 100f);
        unit.Armor.Actual += _extraDamage;

        // Включаем иконку эффекта
        enemyUnit?.IconEffectsController?.EnableIcon<DamageUp>(true);
    }

    public override void Disable()
    {
        unit.Armor.Actual -= _extraDamage;

        // Выключаем иконку эффекта
        enemyUnit?.IconEffectsController?.EnableIcon<DamageUp>(false);
    }
}
