
using UnityEngine;

/// <summary>
/// ����� ������������ �����, ����������� ��������� ������ ��������
/// </summary>
public class PenetrationProjectile : AttackModifier
{
    public override string Name => "����������� ������";

    public override string Description => $"C����� ��������� ��������� ({MaxPenetrationCount.Value}) ����� � ������� ���� ������������� �� {PenetrationDamageDecrease.Value}% � ������ ���������";

    /// <summary>
    /// ������������ ���-�� ����������� �����
    /// </summary>
    public Parameter MaxPenetrationCount { get; private set; }

    /// <summary>
    /// ���������� ����� � ������ ���������
    /// </summary>
    public Parameter PenetrationDamageDecrease { get; private set; }

    /// <summary>
    /// ������� ���-�� �������� �������� �����
    /// </summary>
    public int CurrentPenetration
    {
        get => _currentPenetration;
        set => _currentPenetration = Mathf.Clamp(value, 0, MaxPenetrationCount.Value);
    }
    private int _currentPenetration = 0;

    public PenetrationProjectile(
        int defaultValueMaxPenetrationCount = 2, int increaseMaxPenetrationCountPerLevel = 1, int maxLevelPenetrationCount = 1,
        int defaultValuePenetrationDamageDecrease = 50, int decreasePenetrationDamageDecreasePerLevel = 5, int levelPenetrationDamageDecrease = 1, int maxLevelPenetrationDamageDecrease = 10)
    {
        MaxPenetrationCount = new Parameter(defaultValue: defaultValueMaxPenetrationCount, increaseValuePerLevel: increaseMaxPenetrationCountPerLevel, level: maxLevelPenetrationCount);

        PenetrationDamageDecrease = new Parameter(defaultValue: defaultValuePenetrationDamageDecrease, increaseValuePerLevel: decreasePenetrationDamageDecreasePerLevel, maxLevel: maxLevelPenetrationDamageDecrease, level: levelPenetrationDamageDecrease);

        MaxPenetrationCount.UpgradeDescription = $"����� ����������� ����� +{MaxPenetrationCount.Value} (������� ��������: {MaxPenetrationCount.Value})";
        PenetrationDamageDecrease.UpgradeDescription = $"���������� ����� � ������ ��������� -{PenetrationDamageDecrease.Value}% (������� ���������� {PenetrationDamageDecrease.Value}%)";
    }
}

