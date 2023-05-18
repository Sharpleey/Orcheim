using UnityEngine;

/// <summary>
/// Класс параметр здоровья юнита
/// </summary>
public class Health : UnitParameter
{
    public override float Default
    {
        get => _default;
        protected set => _default = Mathf.Round(Mathf.Clamp(value, 1, int.MaxValue));
    }

    public Health(float defaultValue, float increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = HashUnitParameterString.HEALTH;
        UpgradeDescription = HashUnitParameterString.HEALTH_UPGRADE_DESCRIPTION;
    }
}
