
/// <summary>
/// Класс параметр стоимость юнита в опыте за убийство
/// </summary>
public class CostInExp : UnitParameter
{
    public CostInExp(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {

    }
}
