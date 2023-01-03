using UnityEngine;

/// <summary>
/// Повреждаемый
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Метод применения урона на юнита
    /// </summary>
    /// <param name="damage">Объект класса параметра урона</param>
    void TakeDamage(Damage damage, Collider hitBox = null);
}
