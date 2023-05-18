using UnityEngine;

public abstract class Upgratable
{
    #region Properties

    /// <summary>
    /// ������������ ������� ��������� ���������
    /// </summary>
    public int MaxLevel
    {
        get => _maxLevel;
        protected set => _maxLevel = Mathf.Clamp(value, 1, int.MaxValue);
    }

    /// <summary>
    /// ������� ������� ���������
    /// </summary>
    public int Level
    {
        get => _level;
        private set => _level = Mathf.Clamp(value, 1, _maxLevel);
    }

    /// <summary>
    /// �������� �������� ��������� �� ������� ���������
    /// </summary>
    public virtual float ChangeValuePerLevel
    {
        get => _changeValuePerLevel;
        protected set => _changeValuePerLevel = Mathf.Clamp(value, 0, float.MaxValue);
    }

    /// <summary>
    /// �������� ��������� ���������
    /// </summary>
    public virtual string UpgradeDescription { get => _upgradeDescription; set => _upgradeDescription = value; }

    #endregion Properties

    #region Private fields

    private int _maxLevel;
    private int _level;
    protected float _changeValuePerLevel;
    protected string _upgradeDescription;

    #endregion Private fields

    public Upgratable(float changeValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1)
    {
        ChangeValuePerLevel = changeValuePerLevel;
        Level = level;
        MaxLevel = maxLevel;
    }


    /// <summary>
    /// �������� �������� �� ������� ��� ��������� �������
    /// </summary>
    /// <param name="levelUp">��������, �� ������� ������� �������� �������� (�� ��������� �� 1)</param>
    public virtual void LevelUp(int levelUp = 1)
    {
        Level += levelUp;

        SetLevel(Level);
    }

    /// <summary>
    /// ���������� ������� ���������
    /// </summary>
    /// <param name="newLevel">�������, ������� ����� ����������</param>
    public virtual void SetLevel(int newLevel)
    {
        Level = newLevel;
    }
}
