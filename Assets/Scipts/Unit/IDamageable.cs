using UnityEngine;

/// <summary>
/// Повреждаемый
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Метод применения урона на юнита
    /// </summary>
    /// <param name="damage">Объект параметра Damage</param>
    /// <param name="isCriticalHit">Критическая атаки или нет</param>
    /// <param name="hitBox">Хитбокс (Коллайдер) попадания</param>
    void TakeDamage(Damage damage, bool isCriticalHit = false, Collider hitBox = null);
}
