using UnityEngine;

/// <summary>
/// Класс параметра урона юнита
/// </summary>
public class Damage : UnitParameter
{
    public override int Actual
    {
        get
        {
            int range = (int)(_actual * GeneralParameter.OFFSET_DAMAGE_HEALING);
            return Random.Range(_actual - range, _actual + range);
        }
        set
        {
            _actual = Mathf.Clamp(value, 1, int.MaxValue);
        }
    }

    /// <summary>
    /// Игнорирование брони, при нанесении урона
    /// </summary>
    public bool IsArmorIgnore { get; private set; }

    /// <summary>
    /// Тип урона
    /// </summary>
    public DamageType DamageType { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="defaultValue">Начальный урон</param>
    /// <param name="increaseValuePerLevel">Прирост урона за уровень улучшения параметра</param>
    /// <param name="damageType">Тип урона</param>
    /// <param name="isArmorIgnore">Игнорирование брони юнита при нанесении урона</param>
    /// <param name="maxLevel">Максимальный уровень улучшение параметра</param>
    /// <param name="level">Создать параметр данного уровня</param>
    public Damage(int defaultValue, int increaseValuePerLevel = 0, DamageType damageType = DamageType.Physical, bool isArmorIgnore = false, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = HashUnitParameterString.DAMAGE;
        UpgradeDescription = HashUnitParameterString.DAMAGE_UPGRADE_DESCRIPTION;
        DamageType = damageType;
        IsArmorIgnore = isArmorIgnore;
    }

    public Damage Copy()
    {
        return (Damage)MemberwiseClone();
    }
}
