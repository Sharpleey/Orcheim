using UnityEngine;

/// <summary>
/// Класс параметр, для различный параметров, которые хотим улучшать. Для использования в модификаторах и т.д.
/// </summary>
public class Parameter : Upgratable
{
    #region Properties

    public virtual float Value
    {
        get => _value;
        protected set => _value = Mathf.Round(Mathf.Clamp(value, 0, int.MaxValue));
    }

    public override string UpgradeDescription 
    {
        get => string.Format(_upgradeDescription, ChangeValuePerLevel, Value);
        set => _upgradeDescription = value;
    }

    public override float ChangeValuePerLevel 
    { 
        get => _changeValuePerLevel;
        protected set => _changeValuePerLevel = Mathf.Round(Mathf.Clamp(value, int.MinValue, int.MaxValue));
    }

    #endregion Properties

    #region Private fields

    protected float _value;
    protected float _defaultValue;

    #endregion Private fields

    #region Public methods

    public Parameter(float defaultValue, float changeValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(changeValuePerLevel, maxLevel, level)
    {
        _defaultValue = defaultValue;
        Value = _defaultValue;

        SetLevel(Level);
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        Value = _defaultValue + ChangeValuePerLevel * (Level - 1);
    }

    #endregion Public methods
}
