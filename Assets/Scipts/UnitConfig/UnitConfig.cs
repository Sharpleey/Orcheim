using UnityEngine;

public abstract class UnitConfig : ScriptableObject
{
    #region Fields

    [Header("Уровень")]
    [Tooltip("Начальный уровень")]
    [SerializeField, Min(1)] private int _level = 1;

    [Header("Урон")]
    [Tooltip("Начальный средний урон юнита")]
    [SerializeField, Min(0)] private int _damage;
    [Tooltip("Увеличение урона за уровень")]
    [SerializeField, Min(0)] private int _damageIncreasePerLevel;
    [Tooltip("Максимальный уровень улучшения")]
    [SerializeField, Min(1)] private int _damageMaxLevel = 100;

    [Header("Здоровье")]
    [Tooltip("Начальное здоровье")]
    [SerializeField, Min(1)] private int _health;
    [Tooltip("Увеличение здоровья за уровень")]
    [SerializeField, Min(0)] private int _healthIncreasePerLevel;
    [Tooltip("Максимальный уровень улучшения")]
    [SerializeField, Min(1)] private int _healthMaxLevel = 100;

    [Header("Броня")]
    [Tooltip("Начальное значение брони")]
    [SerializeField, Min(0)] private int _armor;
    [Tooltip("Увеличение брони за уровень")]
    [SerializeField, Min(0)] private int _armorIncreasePerLevel;
    [Tooltip("Максимальный уровень улучшения")]
    [SerializeField, Min(1)] private int _armorMaxLevel = 100;

    [Header("Скорость передвижения")]
    [Tooltip("Начальное значение скорости передвижения")]
    [SerializeField, Min(100)] private int _movementSpeed;
    [Tooltip("Увеличение скорости передвижения за уровень")]
    [SerializeField, Min(0)] private int _movementSpeedIncreasePerLevel;
    [Tooltip("Максимальный уровень улучшения")]
    [SerializeField, Min(1)] private int _movementSpeedMaxLevel = 100;

    [Header("Скорость атаки")]
    [Tooltip("Начальное значение скорости атаки")]
    [SerializeField, Min(10)] private int _attackSpeed;
    [Tooltip("Увеличение скорости атаки за уровень")]
    [SerializeField, Min(0)] private int _attackSpeedIncreasePerLevel;
    [Tooltip("Максимальный уровень улучшения")]
    [SerializeField, Min(1)] private int _attackSpeedMaxLevel = 100;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Начальный уровень
    /// </summary>
    public int Level => _level;

    /// <summary>
    /// Начальный урон
    /// </summary>
    public int DefaultDamage => _damage;
    /// <summary>
    /// Прирост урона за уровень улучшения
    /// </summary>
    public int DamageIncreasePerLevel => _damageIncreasePerLevel;
    /// <summary>
    /// Максимальный уровень улучшения урона
    /// </summary>
    public int DamageMaxLevel => _damageMaxLevel;

    /// <summary>
    /// Начальное здоровье
    /// </summary>
    public int DefaultHealth => _health;
    /// <summary>
    /// Прирост здоровья за уровень улучшения
    /// </summary>
    public int HealthIncreasePerLevel => _healthIncreasePerLevel;
    /// <summary>
    /// Максимальный уровень улучшения здоровья
    /// </summary>
    public int HealthMaxLevel => _healthMaxLevel;

    /// <summary>
    /// Начальная броня
    /// </summary>
    public int DefaultArmor => _armor;
    /// <summary>
    /// Прирост брони за уровень улучшения
    /// </summary>
    public int ArmorIncreasePerLevel => _armorIncreasePerLevel;
    /// <summary>
    /// Максимальный уровень улучшения брони
    /// </summary>
    public int ArmorMaxLevel => _armorMaxLevel;

    /// <summary>
    /// Начальная скорость передвижения
    /// </summary>
    public int DefaultMovementSpeed => _movementSpeed;
    /// <summary>
    /// Прирост скорости передвижения за уровень улучшения
    /// </summary>
    public int MovementSpeedIncreasePerLevel => _movementSpeedIncreasePerLevel;
    /// <summary>
    /// Максимальный уровень улучшения скорости передвижения
    /// </summary>
    public int MovementSpeedMaxLevel => _movementSpeedMaxLevel;

    /// <summary>
    /// Начальная скорость атаки
    /// </summary>
    public int DefaultAttackSpeed => _attackSpeed;
    /// <summary>
    /// Прирост скорости атаки за уровень улучшения
    /// </summary>
    public int AttackSpeedIncreasePerLevel => _attackSpeedIncreasePerLevel;
    /// <summary>
    /// Максимальный уровень улучшения скорости атаки
    /// </summary>
    public int AttackSpeedMaxLevel => _attackSpeedMaxLevel;

    #endregion Properties
}