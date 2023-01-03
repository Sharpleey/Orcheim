
/// <summary>
/// Класс параметр стоимость юнита в золоте за убийство
/// </summary>
public class CostInGold : UnitParameter
{
    public CostInGold(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {

    }
}
