using System;
using System.Collections.Generic;

/// <summary>
/// ���������� ������������ �����
/// </summary>
public interface IUsesAttackModifiers
{
    ///// <summary>
    ///// ������� ������������ ������ ������������� ����
    ///// </summary>
    //public Dictionary<Type, AttackModifaer> ActiveAttackModifaers { get; }

    CriticalAttack CriticalAttack { get; }
    FlameAttack FlameAttack { get; }
    SlowAttack SlowAttack { get; }
    PenetrationProjectile PenetrationProjectile { get; }
}
