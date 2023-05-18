using UnityEngine;

/// <summary>
/// Атакующий юнит
/// </summary>
public interface IAttacking
{
    /// <summary>
    /// Метод применения атаки, нанесения урона
    /// </summary>
    /// <param name="attackedUnit">Атакованный юнит</param>
    /// <param name="currentPenetration">Текущее кол-во пробитий снарядом</param>
    /// <param name="hitBox">Хитбокс, в который попал снаряд дальнего оружия</param>
    void PerformAttack(Unit attackedUnit, int currentPenetration, Collider hitBox);
}
