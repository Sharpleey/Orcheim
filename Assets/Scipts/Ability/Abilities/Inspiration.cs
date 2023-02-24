using UnityEngine;

public class Inspiration : ActiveAbility
{
    /// <summary>
    /// ������ �������� �����������
    /// </summary>
    public Parameter Radius { get; private set; }

    /// <summary>
    /// ������ ������ �����������, ������� ������������� �� �����
    /// </summary>
    public Effect DamageUp { get; private set; }

    /// <summary>
    /// ����� ����� � ����� ����� Enemy, ������� �� ����� ������
    /// </summary>
    private LayerMask _collisionMask = 4096;

    public Inspiration(Unit unit, int timeCooldown, int radius, int decreaseTimeCooldownPerLevel = 1, bool isActive = false) : base(unit, timeCooldown, decreaseTimeCooldownPerLevel, isActive)
    {
        Radius = new Parameter(radius);

        DamageUp = new DamageUp(15, 50);
    }

    public override void Apply()
    {
        Vector3 center = unit.gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, Radius.Value, _collisionMask);

        if (hitColliders == null)
            return;

        // ������� ����� ����������, ������� ��������� � ������� ��������, ���������� ������
        foreach (Collider unitCollider in hitColliders)
        {
            EnemyUnit enemyUnit = unitCollider.GetComponent<EnemyUnit>();
            enemyUnit?.SetEffect(DamageUp);
        }
    }
}
