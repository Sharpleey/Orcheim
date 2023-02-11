using UnityEngine;

public class UnitParameter : Upgratable
{
    #region Properties

    /// <summary>
    /// Название параметра юнита
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Начальное значение параметра
    /// </summary>
    public virtual float Default
    {
        get => _default;
        protected set => _default = Mathf.Round(Mathf.Clamp(value, 0, int.MaxValue));
    }

    /// <summary>
    /// Максимальное значение параметра
    /// </summary>
    public virtual float Max
    {
        get => _max;
        protected set => _max = Mathf.Round(Mathf.Clamp(value, _default, int.MaxValue));
    }

    /// <summary>
    /// Текущее значение параметра
    /// </summary>
    public virtual float Actual
    {
        get => _actual;
        set => _actual = Mathf.Round(Mathf.Clamp(value, 0, _max));
    }

    public override string UpgradeDescription
    {
        get => string.Format(_upgradeDescription, ChangeValuePerLevel, Max);
        set => _upgradeDescription = value;
    }

    #endregion Properties

    #region Private fields

    protected float _default;
    protected float _max;
    protected float _actual;

    #endregion Private fields

    /// <summary>
    /// Конструктор абстрактного класса параметра юнита
    /// </summary>
    /// <param name="defaultValue">Начальное значение параметра</param>
    /// <param name="changeValuePerLevel">Прирост значения за уровень</param>
    /// <param name="maxLevel">Максимальный уровень улучшения параметра</param>
    /// <param name="level">Текущий уровень улучшения параметра</param>
    public UnitParameter(float defaultValue, float changeValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base (changeValuePerLevel, maxLevel, level)
    {
        Default = defaultValue;

        SetLevel(Level);
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        Max = Default + ChangeValuePerLevel * (Level - 1);
        Actual = Max;
    }
}




