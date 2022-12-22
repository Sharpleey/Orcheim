
public interface IArmor
{
    /// <summary>
    /// Стартовая броня
    /// </summary>
    public int DefaultArmor { get; }

    /// <summary>
    /// Максимальное значение брони
    /// </summary>
    public int MaxArmor { get; }

    /// <summary>
    /// Актуальное значение брони
    /// </summary>
    public int ActualArmor { get; set; }
}
