using System;
using System.Collections.Generic;

/// <summary>
/// Использует модификаторы атаки
/// </summary>
public interface IUsesAttackModifiers
{
    public Dictionary<Type, AttackModifier> AttackModifiers { get; }
    public void SetAttackModifier(AttackModifier attackModifier);
    public void RemoveAttackModifier<T>() where T : AttackModifier;

    /// <summary>
    /// Метод инициализирует модификаторы атак из конфиг файла
    /// </summary>
    public void InitAttackModifiers();
}
