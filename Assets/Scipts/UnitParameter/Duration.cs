using UnityEngine;

/// <summary>
/// Параметер длительности 
/// </summary>
public class Duration : UnitParameter
{
    public override int Default
    {
        get => _default;
        protected set => _default = Mathf.Clamp(value, 1, int.MaxValue);
    }

    public Duration(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {

    }
}
