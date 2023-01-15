
/// <summary>
/// Использует модификаторы атаки
/// </summary>
public interface IUsesAttackModifiers
{
    /// <summary>
    /// Словарь используемых юнитом модификаторов атак
    /// </summary>
    //public Dictionary<Type, AttackModifaer> AttackModifaers { get; }

    CriticalAttack CriticalAttack { get; }
    FlameAttack FlameAttack { get; }
    SlowAttack SlowAttack { get; }
    PenetrationProjectile PenetrationProjectile { get; }


    public void SetAttackModifier(AttackModifier attackModifier);
    public void InitAttackModifiers();
}
