
/// <summary>
/// Использует модификаторы атаки
/// </summary>
public interface IUsesAttackModifiers
{
    CriticalAttack CriticalAttack { get; }
    FlameAttack FlameAttack { get; }
    SlowAttack SlowAttack { get; }
    PenetrationProjectile PenetrationProjectile { get; }

    /// <summary>
    /// Метод устанавливает модификатор атаки на юнита
    /// </summary>
    /// <param name="attackModifier"></param>
    public void SetActiveAttackModifier(AttackModifier attackModifier);
    
    /// <summary>
    /// Метод инициализирует модификаторы атак из конфиг файла
    /// </summary>
    public void InitAttackModifiers();
}
