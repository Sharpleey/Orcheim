using UnityEngine;

/// <summary>
/// ����� ��������� ����� �����
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
    }

    /// <summary>
    /// ������������� �����, ��� ��������� �����
    /// </summary>
    public bool IsArmorIgnore { get; private set; }

    /// <summary>
    /// ��� �����
    /// </summary>
    public DamageType DamageType { get; private set; }

    public Damage(int defaultValue, int increaseValuePerLevel = 0, DamageType damageType = DamageType.Physical, bool isArmorIgnore = false, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        DamageType = damageType;
        IsArmorIgnore = isArmorIgnore;
    }
}
