using UnityEngine;

public class Warcry : ActiveAbility
{
    /// <summary>
    /// Радиус действия способности
    /// </summary>
    public Parameter Radius { get; private set; }

    public Effect ArmorUp { get; private set; }

    /// <summary>
    /// Маска слоев с одним слоем Enemy, который мы будем искать
    /// </summary>
    private LayerMask collisionMask = 4096;

    public Warcry(Unit unit, int timeCooldown, int radius, int decreaseTimeCooldownPerLevel = 1, bool isActive = false) : base(unit: unit, timeCooldown: timeCooldown, decreaseTimeCooldownPerLevel: decreaseTimeCooldownPerLevel, isActive: isActive)
    {
        Radius = new Parameter(radius);
        ArmorUp = new ArmorUp(5, 100);
    }

    public override void Apply()
    {
        Vector3 center = unit.gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, Radius.Value, collisionMask);

        if (hitColliders == null)
            return;

        // Каждому юниту противника, который находится в радиусе действия, навешиваем эффект
        foreach (Collider unitCollider in hitColliders)
        {
            EnemyUnit enemyUnit = unitCollider.GetComponent<EnemyUnit>();

            if (enemyUnit)
                enemyUnit.SetEffect(ArmorUp);
        }
    }
}
