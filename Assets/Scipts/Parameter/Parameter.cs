using UnityEngine;

/// <summary>
/// ����� ��������, ��� ��������� ����������, ������� ����� ��������. ��� ������������� � ������������� � �.�.
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
        get => string.Format(_upgradeDescription, ChangeValuePerLevel, Value);
        set => _upgradeDescription = value;
    }

    public override float ChangeValuePerLevel 
    { 
        get => _changeValuePerLevel;
        protected set => _changeValuePerLevel = Mathf.Clamp(value, -100, float.MaxValue);
    }

    #endregion Properties

    #region Private fields

    private int _value;
    private int _defaultValue;

    #endregion Private fields

    public Parameter(int defaultValue, int changeValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) :base(changeValuePerLevel, maxLevel, level)
    {
        _defaultValue = defaultValue;
        Value = _defaultValue;

        SetLevel(Level);
    }

    #region Public methods

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        Value = _defaultValue + (int)ChangeValuePerLevel * (Level - 1);
    }

    #endregion Public methods
}
