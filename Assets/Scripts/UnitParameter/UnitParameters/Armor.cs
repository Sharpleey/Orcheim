
using UnityEngine;
/// <summary>
/// Класс параметр брони юнита
/// </summary>
public class Armor : UnitParameter
{
    public override float Actual
    {
        get => _actual;
        set => _actual = Mathf.Round(Mathf.Clamp(value, 0, int.MaxValue));
    }
    public Armor(float defaultValue, float increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = HashUnitParameterString.ARMOR;
        UpgradeDescription = HashUnitParameterString.ARMOR_UPGRADE_DESCRIPTION;
    }
}
