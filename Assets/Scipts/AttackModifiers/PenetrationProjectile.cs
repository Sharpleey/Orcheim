
using UnityEngine;

/// <summary>
/// Класс модификатора атаки, позволяющий пробивать врагов насквозь
/// </summary>
public class PenetrationProjectile : AttackModifier
{
    public override string Name => "Пробивающий снаряд";

    public override string Description => $"Cнаряд пробивает несколько ({MaxPenetrationCount.Value}) целей и наносит урон уменьщающийся на {PenetrationDamageDecrease.Value}% с каждым пробитием";

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
    public int CurrentPenetration
    {
        get => _currentPenetration;
        set => _currentPenetration = Mathf.Clamp(value, 0, MaxPenetrationCount.Value);
    }
    private int _currentPenetration = 0;

    public PenetrationProjectile(
        int defaultValueMaxPenetrationCount = 2, int increaseMaxPenetrationCountPerLevel = 1, int maxLevelPenetrationCount = 1,
        int defaultValuePenetrationDamageDecrease = 50, int decreasePenetrationDamageDecreasePerLevel = 5, int levelPenetrationDamageDecrease = 1, int maxLevelPenetrationDamageDecrease = 10)
    {
        MaxPenetrationCount = new Parameter(defaultValue: defaultValueMaxPenetrationCount, increaseValuePerLevel: increaseMaxPenetrationCountPerLevel, level: maxLevelPenetrationCount);

        PenetrationDamageDecrease = new Parameter(defaultValue: defaultValuePenetrationDamageDecrease, increaseValuePerLevel: decreasePenetrationDamageDecreasePerLevel, maxLevel: maxLevelPenetrationDamageDecrease, level: levelPenetrationDamageDecrease);

        MaxPenetrationCount.UpgradeDescription = $"Число пробиваемые целей +{MaxPenetrationCount.Value} (Текущее значение: {MaxPenetrationCount.Value})";
        PenetrationDamageDecrease.UpgradeDescription = $"Уменьшение урона с каждым пробитием -{PenetrationDamageDecrease.Value}% (Текущее уменьшение {PenetrationDamageDecrease.Value}%)";
    }
}

