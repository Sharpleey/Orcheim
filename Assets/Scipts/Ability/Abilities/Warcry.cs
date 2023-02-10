using UnityEngine;

/// <summary>
/// ����������� ��� ���������� ����������� �� ������� ������ ������ ArmorUp, ������������� ����� ������ �� ��������� �����
/// </summary>
public class Warcry : ActiveAbility
{
    /// <summary>
    /// ������ �������� �����������
    /// </summary>
    public Parameter Radius { get; private set; }

    /// <summary>
    /// ������ ������ �����������, ������� ������������� �� �����
    /// </summary>
    public Effect ArmorUp { get; private set; }

    /// <summary>
    /// ����� ����� � ����� ����� Enemy, ������� �� ����� ������
    /// </summary>
    private LayerMask collisionMask = 4096;

    public Warcry(Unit unit, int timeCooldown, int radius, int decreaseTimeCooldownPerLevel = 1, bool isActive = false) : base(unit: unit, timeCooldown: timeCooldown, decreaseTimeCooldownPerLevel: decreaseTimeCooldownPerLevel, isActive: isActive)
    {
        Radius = new Parameter(radius);

        ArmorUp = new ArmorUp(12, 100);
    }

    public override void Apply()
    {
        Vector3 center = unit.gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, Radius.Value, collisionMask);

        if (hitColliders == null)
            return;

        // ������� ����� ����������, ������� ��������� � ������� ��������, ���������� ������
        foreach (Collider unitCollider in hitColliders)
        {
            EnemyUnit enemyUnit = unitCollider.GetComponent<EnemyUnit>();
            enemyUnit?.SetEffect(ArmorUp);
        }
    }
}
