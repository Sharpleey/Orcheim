using UnityEngine;

/// <summary>
/// Класс параметр, для различный параметров, которые хотим улучшать
/// </summary>
public class Parameter : Upgratable
{
    public int Value
    {
        get => _value;
        protected set => _value = Mathf.Clamp(value, 0, int.MaxValue);
    }

    private int _value;

    public override string UpgradeDescription 
    {
        get => string.Format(_upgradeDescription, IncreaseValuePerLevel, Value);
        set => _upgradeDescription = value;
    }

    public Parameter(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) :base(increaseValuePerLevel, maxLevel, level)
    {
        Value = defaultValue;

        SetLevel(Level);
    }

    public override void Upgrade(int levelUp = 1)
    {
        base.Upgrade(levelUp);

        Value += (int)IncreaseValuePerLevel * (Level - 1);
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        Value += (int)IncreaseValuePerLevel * (Level - 1);
    }
}
