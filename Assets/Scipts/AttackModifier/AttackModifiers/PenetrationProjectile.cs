
using UnityEngine;

/// <summary>
/// Класс модификатора атаки, позволяющий пробивать врагов насквозь
/// </summary>
public class PenetrationProjectile : AttackModifier
{
    public override string Name => HashAttackModString.PENETRATION_PROJECTILE_NAME;

    public override string Description => string.Format(HashAttackModString.PENETRATION_PROJECTILE_DESCRIPTION, MaxPenetrationCount.Value, PenetrationDamageDecrease.Value);

    /// <summary>
    /// Максимальное кол-во пробиваемый целей
    /// </summary>
    public Parameter MaxPenetrationCount { get; private set; }

    /// <summary>
    /// Уменьшение урона с каждым пробитием
    /// </summary>
    public Parameter PenetrationDamageDecrease { get; private set; }

    public PenetrationProjectile(
        int defaultValueMaxPenetrationCount = 2, int increaseMaxPenetrationCountPerLevel = 1, int maxLevelPenetrationCount = 1,
        int defaultValuePenetrationDamageDecrease = 50, int decreasePenetrationDamageDecreasePerLevel = -5, int levelPenetrationDamageDecrease = 1, int maxLevelPenetrationDamageDecrease = 10)
    {
        MaxPenetrationCount = new Parameter(defaultValue: defaultValueMaxPenetrationCount, changeValuePerLevel: increaseMaxPenetrationCountPerLevel, level: maxLevelPenetrationCount);

        PenetrationDamageDecrease = new Parameter(defaultValue: defaultValuePenetrationDamageDecrease, changeValuePerLevel: decreasePenetrationDamageDecreasePerLevel, maxLevel: maxLevelPenetrationDamageDecrease, level: levelPenetrationDamageDecrease);

        MaxPenetrationCount.UpgradeDescription = HashAttackModString.PENETRATION_PROJECTILE_MAX_COUNT_UPGRADE_DESCRIPTION;
        PenetrationDamageDecrease.UpgradeDescription = HashAttackModString.PENETRATION_DAMAGE_DECREASE_COUNT_UPGRADE_DESCRIPTION;
    }

    /// <summary>
    /// Возвращает уменьшенный урон с учетом конкретного значения пробития
    /// </summary>
    /// <param name="damage">Базовый урон</param>
    /// <param name="currentPenetration">Какое пробитие по счету</param>
    /// <returns></returns>
    public float GetValueDamage(float damage, int currentPenetration)
    {
        float resultPenetrationDamageDecrease = PenetrationDamageDecrease.Value;
        float extraPenetrationDamageDecrease = PenetrationDamageDecrease.Value;

        for (int i = 1; i < currentPenetration; i++)
        {
            extraPenetrationDamageDecrease /= 2;
            resultPenetrationDamageDecrease += extraPenetrationDamageDecrease;
        }

        return damage * (1 - (resultPenetrationDamageDecrease / 100f));
    }
}

