using UnityEngine;

public abstract class ProcableAttackModifaer : AttackModifaer, IProcable
{
    /// <summary>
    /// Шанс прока модификатора
    /// </summary>
    public abstract int ProcСhance { get; set; }

    /// <summary>
    /// Прокнул модификатор или нет
    /// </summary>
    /// <returns>true/false</returns>
    public bool IsProc()
    {
        return Random.Range(0, 100) <= ProcСhance;
    }
}
