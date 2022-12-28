using UnityEngine;

[CreateAssetMenu(menuName = "UnitConfig/EnemyUnitConfig", fileName = "EnemyUnitConfig", order = 0)]
public class EnemyUnitConfig : UnitConfig
{
    #region Fields

    [Header("������� �� ������� � ������")]
    [Tooltip("��������� ���������")]
    [SerializeField, Min(0)] private int _gold;
    [Tooltip("������� ��������� �� ������� �����")]
    [SerializeField, Min(0)] private int _increaseGold;

    [Header("������� �� ������� � �����")]
    [Tooltip("��������� ����")]
    [SerializeField, Min(0)] private int _exp;
    [Tooltip("������� ��������� �� ������� �����")]
    [SerializeField, Min(0)] private int _increaseExp;

    [Header("�������� � �������� ����� �����")]
    [Tooltip("��������")]
    [SerializeField] private string _name;
    [Tooltip("��������"), TextArea]
    [SerializeField] private string _description;

    #endregion Fields

    #region Properties

    /// <summary>
    /// ������ �� ��������
    /// </summary>
    public int Gold => _gold;
    /// <summary>
    /// ���������� ������� � ���� ������ �� ������� �����
    /// </summary>
    public int IncreaseGold => _increaseGold;

    /// <summary>
    /// ����� �� ��������
    /// </summary>
    public int Experience => _exp;
    /// <summary>
    /// ���������� ������� � ���� ����� �� ������� �����
    /// </summary>
    public int IncreaseExperience => _increaseExp;
    #endregion Properties
}
