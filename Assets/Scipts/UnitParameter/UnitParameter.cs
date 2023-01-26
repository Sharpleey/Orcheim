using UnityEngine;

public class UnitParameter : Upgratable
{
    #region Properties

    /// <summary>
    /// �������� ��������� �����
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// ��������� �������� ���������
    /// </summary>
    public virtual int Default
    {
        get => _default;
        protected set => _default = Mathf.Clamp(value, 0, int.MaxValue);
    }

    /// <summary>
    /// ������������ �������� ���������
    /// </summary>
    public virtual int Max
    {
        get => _max;
        protected set => _max = Mathf.Clamp(value, _default, int.MaxValue);
    }

    /// <summary>
    /// ������� �������� ���������
    /// </summary>
    public virtual int Actual
    {
        get => _actual;
        set => _actual = Mathf.Clamp(value, 0, _max);
    }

    public override string UpgradeDescription
    {
        get => string.Format(_upgradeDescription, IncreaseValuePerLevel, Max);
        set => _upgradeDescription = value;
    }

    #endregion Properties

    #region Private fields

    protected int _default;
    protected int _max;
    protected int _actual;

    #endregion Private fields

    /// <summary>
    /// ����������� ������������ ������ ��������� �����
    /// </summary>
    /// <param name="defaultValue">��������� �������� ���������</param>
    /// <param name="increaseValuePerLevel">������� �������� �� �������</param>
    /// <param name="maxLevel">������������ ������� ��������� ���������</param>
    /// <param name="level">������� ������� ��������� ���������</param>
    public UnitParameter(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base (increaseValuePerLevel, maxLevel, level)
    {
        Default = defaultValue;

        SetLevel(Level);
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        Max = Default + (int)IncreaseValuePerLevel * (Level - 1);
        Actual = Max;
    }


}




