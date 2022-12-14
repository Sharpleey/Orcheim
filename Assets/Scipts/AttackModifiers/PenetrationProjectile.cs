
using UnityEngine;

/// <summary>
/// Класс модификатора атаки, позволяющий пробивать врагов насквозь
/// </summary>
public class PenetrationProjectile : AttackModifaer
{
    public override string Name => "Пробивающий снаряд";

    public override string Description => "Cнаряд пробивает несколько целей и наносит урон уменьщающийся с каждым пробитием";

    /// <summary>
    /// Максимальное кол-во пробиваемый целей
    /// </summary>
    public int MaxPenetrationCount
    {
        get => _maxPenetrationCount;
        set => _maxPenetrationCount = Mathf.Clamp(value, 2, 10);
    }

    /// <summary>
    /// Уменьшение урона с каждым пробитием
    /// </summary>
    public float PenetrationDamageDecrease
    {
        get => _penetrationDamageDecrease;
        set => _penetrationDamageDecrease = Mathf.Clamp(value, 0, 0.9f);
    }

    /// <summary>
    /// Текущее кол-во пробитых снарядом целей
    /// </summary>
    public int CurrentPenetration
    {
        get => _currentPenetration;
        set => _currentPenetration = Mathf.Clamp(value, 0, _maxPenetrationCount);
    }

    private int _maxPenetrationCount = 2;
    private float _penetrationDamageDecrease = 0.5f;
    private int _currentPenetration = 0;
}

