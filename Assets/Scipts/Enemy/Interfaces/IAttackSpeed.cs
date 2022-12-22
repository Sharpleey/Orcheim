
public interface IAttackSpeed
{
    /// <summary>
    /// Стартовая скорость атаки
    /// </summary>
    public int DefaultAttackSpeed { get; }

    /// <summary>
    /// Максимальное значение скорости атаки
    /// </summary>
    public int MaxAttackSpeed { get; }

    /// <summary>
    /// Актуальное значение скорости атаки
    /// </summary>
    public int ActualAttackSpeed { get; set; }
}
