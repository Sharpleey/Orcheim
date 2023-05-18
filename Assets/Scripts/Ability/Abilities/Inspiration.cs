using UnityEngine;

public class Inspiration : ActiveAbility
{
    /// <summary>
    /// Радиус действия способности
    /// </summary>
    public Parameter Radius { get; private set; }

    /// <summary>
    /// Эффект данной способности, который накладывается на юниты
    /// </summary>
    public Effect DamageUp { get; private set; }

    /// <summary>
    /// Маска слоев с одним слоем Enemy, который мы будем искать
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

        // Каждому юниту противника, который находится в радиусе действия, навешиваем эффект
        foreach (Collider unitCollider in hitColliders)
        {
            EnemyUnit enemyUnit = unitCollider.GetComponent<EnemyUnit>();
            enemyUnit?.SetEffect(DamageUp);
        }
    }
}
