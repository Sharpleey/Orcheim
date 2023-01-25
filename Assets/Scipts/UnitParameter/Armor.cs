
/// <summary>
/// Класс параметр брони юнита
/// </summary>
public class Armor : UnitParameter
{
    public Armor(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = HashUnitParameterString.ARMOR;
        UpgradeDescription = HashUnitParameterString.ARMOR_UPGRADE_DESCRIPTION;
    }
}
