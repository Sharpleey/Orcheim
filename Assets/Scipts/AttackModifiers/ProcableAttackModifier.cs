using UnityEngine;

public abstract class ProcableAttackModifier : AttackModifier, IProcable
{
    /// <summary>
    /// Шанс прока модификатора
    /// </summary>
    public ProcСhance Сhance { get; protected set; }

    /// <summary>
    /// Прокнул модификатор или нет
    /// </summary>
    /// <returns>true/false</returns>
    public bool IsProc
    {
        get => Random.Range(0, 100) <= Сhance.Actual;
    }
}
