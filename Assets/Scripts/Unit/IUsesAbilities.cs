using System;
using System.Collections.Generic;

/// <summary>
/// Юнит использует способности
/// </summary>
public interface IUsesAbilities
{
    /// <summary>
    /// Словарь для хранения способностей юнита
    /// </summary>
    public Dictionary<Type, Ability> Abilities { get; }

    /// <summary>
    /// Инициализация способностей
    /// </summary>
    public void InitAbilities();
}
