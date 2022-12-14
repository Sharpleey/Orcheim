
using UnityEngine;

/// <summary>
/// ����� ������������ �����, ����������� ��������� ������ ��������
/// </summary>
public class PenetrationProjectile : AttackModifaer
{
    public override string Name => "����������� ������";

    public override string Description => "C����� ��������� ��������� ����� � ������� ���� ������������� � ������ ���������";

    /// <summary>
    /// ������������ ���-�� ����������� �����
    /// </summary>
    public int MaxPenetrationCount
    {
        get => _maxPenetrationCount;
        set => _maxPenetrationCount = Mathf.Clamp(value, 2, 10);
    }

    /// <summary>
    /// ���������� ����� � ������ ���������
    /// </summary>
    public float PenetrationDamageDecrease
    {
        get => _penetrationDamageDecrease;
        set => _penetrationDamageDecrease = Mathf.Clamp(value, 0, 0.9f);
    }

    /// <summary>
    /// ������� ���-�� �������� �������� �����
    /// </summary>
    public int CurrentPenetration
    {
        get => _currentPenetration;
        set => _currentPenetration = Mathf.Clamp(value, 0, _maxPenetrationCount);
    }

    private int _maxPenetrationCount = 2;
    private float _penetrationDamageDecrease = 0.5f;
    private int _currentPenetration = 0;
}

