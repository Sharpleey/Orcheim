
public interface IUnitDamage : IDamage
{
    /// <summary>
    /// Стартовый урон средний урон
    /// </summary>
    public int DefaultAvgDamage { get; }

    /// <summary>
    /// Средний урон
    /// </summary>
    int AvgDamage { get; set; }

    /// <summary>
    /// Актуальное значение урона
    /// </summary>
    int ActualDamage { get; }
}
