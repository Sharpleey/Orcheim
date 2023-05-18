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

    [Header("��������� ����� �����")]
    [Tooltip("��������� �����")]
    [SerializeField] private float _attackDistance;

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

    /// <summary>
    /// ��������� �����
    /// </summary>
    public float AttackDistance => _attackDistance;

    /// <summary>
    /// �������� �����
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// ��������� ����� �����
    /// </summary>
    public string Description => _description;
    #endregion Properties
}
