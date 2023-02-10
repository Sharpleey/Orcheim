using UnityEngine;

/// <summary>
/// Способность при применении накладывает на союзных юнитов эффект ArmorUp, увеличивающий броню юнитов на некоторое время
/// </summary>
public class Warcry : ActiveAbility
{
    /// <summary>
    /// Радиус действия способности
    /// </summary>
    public Parameter Radius { get; private set; }

    /// <summary>
    /// Эффект данной способности, который накладывается на юниты
    /// </summary>
    public Effect ArmorUp { get; private set; }

    /// <summary>
    /// Маска слоев с одним слоем Enemy, который мы будем искать
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

        // Каждому юниту противника, который находится в радиусе действия, навешиваем эффект
        foreach (Collider unitCollider in hitColliders)
        {
            EnemyUnit enemyUnit = unitCollider.GetComponent<EnemyUnit>();
            enemyUnit?.SetEffect(ArmorUp);
        }
    }
}
