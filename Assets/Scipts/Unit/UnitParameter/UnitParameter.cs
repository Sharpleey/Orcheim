using UnityEngine;

public abstract class UnitParameter : UpgratableParameter
{
    #region Properties

    /// <summary>
    /// Начальное значение параметра
    /// </summary>
    public virtual int Default
    {
        get => _default;
        protected set => _default = Mathf.Clamp(value, 0, int.MaxValue);
    }

    /// <summary>
    /// Максимальное значение параметра
    /// </summary>
    public virtual int Max
    {
        get => _max;
        protected set => _max = Mathf.Clamp(value, _default, int.MaxValue);
    }

    /// <summary>
    /// Текущее значение параметра
    /// </summary>
    public virtual int Actual
    {
        get => _actual;
        set => _actual = Mathf.Clamp(value, 0, _max);
    }

    #endregion Properties

    #region Private fields

    protected int _default;
    protected int _max;
    protected int _actual;

    #endregion Private fields

    public UnitParameter(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1)
    {
        Default = defaultValue;
        IncreaseValuePerLevel = increaseValuePerLevel;
        MaxLevel = maxLevel;

        SetLevel(level);
    }

    public override void Upgrade(int levelUp = 1)
    {
        base.Upgrade(levelUp);

        Max = Default + (int)IncreaseValuePerLevel * (Level - 1);
        Actual = Max;
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        Max = Default + (int)IncreaseValuePerLevel * (Level - 1);
        Actual = Max;
    }


}




