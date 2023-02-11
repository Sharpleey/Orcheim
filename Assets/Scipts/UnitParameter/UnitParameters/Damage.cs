using UnityEngine;

/// <summary>
/// ����� ��������� ����� �����
/// </summary>
public class Damage : UnitParameter
{
    public override float Actual
    {
        get
        {
            // ��������� ��������
            float range = (_actual * GeneralParameter.OFFSET_DAMAGE_HEALING);

            // �������� ��������� ���� � ������ ���������
            float actual = Random.Range(_actual - range, _actual + range);

            return Mathf.Round(actual);
        }
        set
        {
            _actual = Mathf.Round(Mathf.Clamp(value, 1, int.MaxValue));
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="defaultValue">��������� ����</param>
    /// <param name="increaseValuePerLevel">������� ����� �� ������� ��������� ���������</param>
    /// <param name="damageType">��� �����</param>
    /// <param name="isArmorIgnore">������������� ����� ����� ��� ��������� �����</param>
    /// <param name="maxLevel">������������ ������� ��������� ���������</param>
    /// <param name="level">������� �������� ������� ������</param>
    public Damage(float defaultValue, float increaseValuePerLevel = 0, DamageType damageType = DamageType.Physical, bool isArmorIgnore = false, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
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
