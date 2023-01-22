using UnityEngine;

public abstract class ProcableAttackModifier : AttackModifier
{
    /// <summary>
    /// Шанс прока модификатора
    /// </summary>
    public Parameter Сhance { get; protected set; }

    /// <summary>
    /// Прокнул модификатор или нет
    /// </summary>
    /// <returns>true/false</returns>
    public bool IsProc
    {
        get => Random.Range(0, 100) <= Сhance.Value;
    }

    public ProcableAttackModifier(bool isActive = false, int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, int maxLevelProcChance = 18) : base(isActive)
    {
        Сhance = new Parameter(defaultValue: procChance, increaseValuePerLevel: increaseProcChancePerLevel, level: levelProcChance, maxLevel: maxLevelProcChance);
    }
}
