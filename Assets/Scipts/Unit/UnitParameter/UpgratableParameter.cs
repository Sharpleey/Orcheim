using UnityEngine;

public abstract class UpgratableParameter
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
    public float IncreaseValuePerLevel
    {
        get => _increaseValuePerLevel;
        protected set => _increaseValuePerLevel = Mathf.Clamp(value, 0, float.MaxValue);
    }

    #endregion Properties

    #region Private fields

    private int _maxLevel;
    private int _level;
    private float _increaseValuePerLevel;

    #endregion Private fields


    /// <summary>
    /// �������� �������� �� ������� ��� ��������� �������
    /// </summary>
    /// <param name="levelUp">��������, �� ������� ������� �������� �������� (�� ��������� �� 1)</param>
    public virtual void Upgrade(int levelUp = 1)
    {
        Level += levelUp;
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
