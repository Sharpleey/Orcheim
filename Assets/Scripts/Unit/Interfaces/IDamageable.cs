using UnityEngine;

/// <summary>
/// Повреждаемый
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Метод применения урона на юнита
    /// </summary>
    /// <param name="damage">Значение урона</param>
    /// <param name="damageType">Тип урона</param>
    /// <param name="isCriticalHit">Критическая атаки или нет</param>
    /// <param name="hitBox">Хитбокс (Коллайдер) попадания</param>
    void TakeDamage(float damage, DamageType damageType, bool isArmorIgnore, bool isCriticalHit = false, Collider hitBox = null);
}
