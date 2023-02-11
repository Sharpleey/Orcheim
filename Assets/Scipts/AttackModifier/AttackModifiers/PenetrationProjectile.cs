
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

    /// <summary>
    /// Текущее кол-во пробитых снарядом целей
    /// </summary>
    public float CurrentPenetration
    {
        get => _currentPenetration;
        set => _currentPenetration = Mathf.Clamp(value, 0, MaxPenetrationCount.Value);
    }
    private float _currentPenetration = 0;

    public PenetrationProjectile(
        int defaultValueMaxPenetrationCount = 2, int increaseMaxPenetrationCountPerLevel = 1, int maxLevelPenetrationCount = 1,
        int defaultValuePenetrationDamageDecrease = 50, int decreasePenetrationDamageDecreasePerLevel = -5, int levelPenetrationDamageDecrease = 1, int maxLevelPenetrationDamageDecrease = 10)
    {
        MaxPenetrationCount = new Parameter(defaultValue: defaultValueMaxPenetrationCount, changeValuePerLevel: increaseMaxPenetrationCountPerLevel, level: maxLevelPenetrationCount);

        PenetrationDamageDecrease = new Parameter(defaultValue: defaultValuePenetrationDamageDecrease, changeValuePerLevel: decreasePenetrationDamageDecreasePerLevel, maxLevel: maxLevelPenetrationDamageDecrease, level: levelPenetrationDamageDecrease);

        MaxPenetrationCount.UpgradeDescription = HashAttackModString.PENETRATION_PROJECTILE_MAX_COUNT_UPGRADE_DESCRIPTION;
        PenetrationDamageDecrease.UpgradeDescription = HashAttackModString.PENETRATION_DAMAGE_DECREASE_COUNT_UPGRADE_DESCRIPTION;
    }
}

