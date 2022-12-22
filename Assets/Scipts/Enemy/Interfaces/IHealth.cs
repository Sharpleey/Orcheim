
public interface IHealth
{
    /// <summary>
    /// Стартовое здоровье
    /// </summary>
    public int DefaultHealth { get; }

    /// <summary>
    /// Максимальное значение здоровья
    /// </summary>
    public int MaxHealth { get; }

    /// <summary>
    /// Актуальное значение здоровья
    /// </summary>
    public int ActualHealth { get; set; }
}
