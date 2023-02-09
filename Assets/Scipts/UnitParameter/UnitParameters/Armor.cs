
using UnityEngine;
/// <summary>
/// ����� �������� ����� �����
/// </summary>
public class Armor : UnitParameter
{
    public override int Actual
    {
        get => _actual;
        set => _actual = Mathf.Clamp(value, 0, int.MaxValue);
    }
    public Armor(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = HashUnitParameterString.ARMOR;
        UpgradeDescription = HashUnitParameterString.ARMOR_UPGRADE_DESCRIPTION;
    }
}
