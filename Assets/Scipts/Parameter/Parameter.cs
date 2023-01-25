using UnityEngine;

/// <summary>
/// Класс параметр, для различный параметров, которые хотим улучшать
/// </summary>
public class Parameter : Upgratable
{
    #region Properties

    public int Value
    {
        get => _value;
        protected set => _value = Mathf.Clamp(value, 0, int.MaxValue);
    }

    public override string UpgradeDescription 
    {
        get => string.Format(_upgradeDescription, IncreaseValuePerLevel, Value);
        set => _upgradeDescription = value;
    }

    public override float IncreaseValuePerLevel 
    { 
        get => _increaseValuePerLevel;
        protected set => _increaseValuePerLevel = Mathf.Clamp(value, -100, float.MaxValue);
    }

    #endregion Properties

    #region Private fields

    private int _value;
    private int _defaultValue;

    #endregion Private fields

    public Parameter(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) :base(increaseValuePerLevel, maxLevel, level)
    {
        _defaultValue = defaultValue;
        Value = _defaultValue;

        SetLevel(Level);
    }

    #region Public methods
    public override void Upgrade(int levelUp = 1)
    {
        base.Upgrade(levelUp);

        Value = _defaultValue + (int)IncreaseValuePerLevel * (Level - 1);
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        Value = _defaultValue + (int)IncreaseValuePerLevel * (Level - 1);
    }

    #endregion Public methods
}
