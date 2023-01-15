
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

    /// <summary>
    /// Метод устанавливает модификатор атаки на юнита
    /// </summary>
    /// <param name="attackModifier"></param>
    public void SetAttackModifier(AttackModifier attackModifier);
    
    /// <summary>
    /// Метод инициализирует модификаторы атак из конфиг файла
    /// </summary>
    public void InitAttackModifiers();
}
