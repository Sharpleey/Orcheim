
public interface IUnitParameters
{
    /// <summary>
    /// Здоровье юнита
    /// </summary>
    public Health Health { get; }

    /// <summary>
    /// Броня юнита
    /// </summary>
    public Armor Armor { get; }

    /// <summary>
    /// Урон юнита
    /// </summary>
    public Damage Damage { get; }

    /// <summary>
    /// Скорость передвижения юнита
    /// </summary>
    public MovementSpeed MovementSpeed { get; }

    /// <summary>
    /// Скорость атаки юнита
    /// </summary>
    public AttackSpeed AttackSpeed { get; }

    /// <summary>
    /// Метод инициализирует параметры юнита, инициализирует значениями из конфига
    /// </summary>
    public void InitParameters();
}
