using UnityEngine;

/// <summary>
/// Параметр шанса выпадения
/// </summary>
public class ProcСhance : UnitParameter
{
    public override int Max 
    { 
        get => _max; 
        protected set => _max = Mathf.Clamp(value, 1, 100);
    }

    public override int Default
    {
        get => _default;
        protected set => _default = Mathf.Clamp(value, 1, int.MaxValue);
    }

    public ProcСhance(int defaultValue = 10, int increaseValuePerLevel = 5, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        MaxLevel = (100 - defaultValue) / increaseValuePerLevel;
    }
}
