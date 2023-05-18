using System.Collections;
using UnityEngine;

/// <summary>
/// Абстрактный класс активной способности 
/// </summary>
public abstract class ActiveAbility : Ability
{
    /// <summary>
    /// Находится ли способность в перезарядке
    /// </summary>
    public bool IsCooldown { get; protected set; }

    /// <summary>
    /// Время отката способности
    /// </summary>
    public Parameter TimeCooldown { get; protected set; }

    public ActiveAbility(Unit unit, int timeCooldown, int decreaseTimeCooldownPerLevel = 1, bool isActive = false) : base(unit: unit, isActive: isActive)
    {
        TimeCooldown = new Parameter(defaultValue: timeCooldown, changeValuePerLevel: decreaseTimeCooldownPerLevel);
    }

    /// <summary>
    /// Метод применения способности
    /// </summary>
    public abstract void Apply();

    /// <summary>
    /// Откат способности
    /// </summary>
    /// <returns></returns>
    public IEnumerator Cooldown()
    {
        IsCooldown = true;

        yield return new WaitForSeconds(TimeCooldown.Value);

        IsCooldown = false;
    }
}
