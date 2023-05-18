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
    public virtual float Default
    {
        get => _default;
        protected set => _default = Mathf.Round(Mathf.Clamp(value, 0, int.MaxValue));
    }

    /// <summary>
    /// ������������ �������� ���������
    /// </summary>
    public virtual float Max
    {
        get => _max;
        protected set => _max = Mathf.Round(Mathf.Clamp(value, _default, int.MaxValue));
    }

    /// <summary>
    /// ������� �������� ���������
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
    /// ����������� ������������ ������ ��������� �����
    /// </summary>
    /// <param name="defaultValue">��������� �������� ���������</param>
    /// <param name="changeValuePerLevel">������� �������� �� �������</param>
    /// <param name="maxLevel">������������ ������� ��������� ���������</param>
    /// <param name="level">������� ������� ��������� ���������</param>
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




