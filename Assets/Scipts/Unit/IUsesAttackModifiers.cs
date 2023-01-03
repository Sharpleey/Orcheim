using System;
using System.Collections.Generic;

/// <summary>
/// Использует модификаторы атаки
/// </summary>
public interface IUsesAttackModifiers
{
    ///// <summary>
    ///// Словарь используемых юнитом модификаторов атак
    ///// </summary>
    //public Dictionary<Type, AttackModifaer> ActiveAttackModifaers { get; }

    CriticalAttack CriticalAttack { get; }
    FlameAttack FlameAttack { get; }
    SlowAttack SlowAttack { get; }
    PenetrationProjectile PenetrationProjectile { get; }
}
