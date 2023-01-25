using UnityEngine;

/// <summary>
/// ����� �������� �������� �����
/// </summary>
public class Health : UnitParameter
{
    public override int Default
    {
        get => _default;
        protected set => _default = Mathf.Clamp(value, 1, int.MaxValue);
    }

    public Health(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = "��������";
        UpgradeDescription = "���������� �������� +{0} (������� �������� {1})";
    }
}
