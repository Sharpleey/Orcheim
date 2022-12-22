
public interface IMovementSpeed
{
    /// <summary>
    /// Стартовая скорость
    /// </summary>
    public float DefaultSpeed { get; }

    /// <summary>
    /// Максимальное значение скорости
    /// </summary>
    public float MaxSpeed { get; }

    /// <summary>
    /// Актуальное значение скорости
    /// </summary>
    public float ActualSpeed { get; set; }
}
