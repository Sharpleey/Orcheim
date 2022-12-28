using UnityEngine;

public abstract class UnitConfig : ScriptableObject
{
    #region Fields

    [Header("�������")]
    [Tooltip("��������� �������")]
    [SerializeField, Min(1)] private int _level = 1;

    [Header("����")]
    [Tooltip("��������� ������� ���� �����")]
    [SerializeField, Min(0)] private int _damage;
    [Tooltip("���������� ����� �� �������")]
    [SerializeField, Min(0)] private int _damageIncreasePerLevel;
    [Tooltip("������������ ������� ���������")]
    [SerializeField, Min(1)] private int _damageMaxLevel = 100;

    [Header("��������")]
    [Tooltip("��������� ��������")]
    [SerializeField, Min(1)] private int _health;
    [Tooltip("���������� �������� �� �������")]
    [SerializeField, Min(0)] private int _healthIncreasePerLevel;
    [Tooltip("������������ ������� ���������")]
    [SerializeField, Min(1)] private int _healthMaxLevel = 100;

    [Header("�����")]
    [Tooltip("��������� �������� �����")]
    [SerializeField, Min(0)] private int _armor;
    [Tooltip("���������� ����� �� �������")]
    [SerializeField, Min(0)] private int _armorIncreasePerLevel;
    [Tooltip("������������ ������� ���������")]
    [SerializeField, Min(1)] private int _armorMaxLevel = 100;

    [Header("�������� ������������")]
    [Tooltip("��������� �������� �������� ������������")]
    [SerializeField, Min(100)] private int _movementSpeed;
    [Tooltip("���������� �������� ������������ �� �������")]
    [SerializeField, Min(0)] private int _movementSpeedIncreasePerLevel;
    [Tooltip("������������ ������� ���������")]
    [SerializeField, Min(1)] private int _movementSpeedMaxLevel = 100;

    [Header("�������� �����")]
    [Tooltip("��������� �������� �������� �����")]
    [SerializeField, Min(10)] private int _attackSpeed;
    [Tooltip("���������� �������� ����� �� �������")]
    [SerializeField, Min(0)] private int _attackSpeedIncreasePerLevel;
    [Tooltip("������������ ������� ���������")]
    [SerializeField, Min(1)] private int _attackSpeedMaxLevel = 100;

    #endregion Fields

    #region Properties

    /// <summary>
    /// ��������� �������
    /// </summary>
    public int Level => _level;

    /// <summary>
    /// ��������� ����
    /// </summary>
    public int DefaultDamage => _damage;
    /// <summary>
    /// ������� ����� �� ������� ���������
    /// </summary>
    public int DamageIncreasePerLevel => _damageIncreasePerLevel;
    /// <summary>
    /// ������������ ������� ��������� �����
    /// </summary>
    public int DamageMaxLevel => _damageMaxLevel;

    /// <summary>
    /// ��������� ��������
    /// </summary>
    public int DefaultHealth => _health;
    /// <summary>
    /// ������� �������� �� ������� ���������
    /// </summary>
    public int HealthIncreasePerLevel => _healthIncreasePerLevel;
    /// <summary>
    /// ������������ ������� ��������� ��������
    /// </summary>
    public int HealthMaxLevel => _healthMaxLevel;

    /// <summary>
    /// ��������� �����
    /// </summary>
    public int DefaultArmor => _armor;
    /// <summary>
    /// ������� ����� �� ������� ���������
    /// </summary>
    public int ArmorIncreasePerLevel => _armorIncreasePerLevel;
    /// <summary>
    /// ������������ ������� ��������� �����
    /// </summary>
    public int ArmorMaxLevel => _armorMaxLevel;

    /// <summary>
    /// ��������� �������� ������������
    /// </summary>
    public int DefaultMovementSpeed => _movementSpeed;
    /// <summary>
    /// ������� �������� ������������ �� ������� ���������
    /// </summary>
    public int MovementSpeedIncreasePerLevel => _movementSpeedIncreasePerLevel;
    /// <summary>
    /// ������������ ������� ��������� �������� ������������
    /// </summary>
    public int MovementSpeedMaxLevel => _movementSpeedMaxLevel;

    /// <summary>
    /// ��������� �������� �����
    /// </summary>
    public int DefaultAttackSpeed => _attackSpeed;
    /// <summary>
    /// ������� �������� ����� �� ������� ���������
    /// </summary>
    public int AttackSpeedIncreasePerLevel => _attackSpeedIncreasePerLevel;
    /// <summary>
    /// ������������ ������� ��������� �������� �����
    /// </summary>
    public int AttackSpeedMaxLevel => _attackSpeedMaxLevel;

    #endregion Properties
}