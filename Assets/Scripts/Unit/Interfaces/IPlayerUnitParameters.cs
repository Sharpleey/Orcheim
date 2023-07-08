
public interface IPlayerUnitParameters : IUnitParameters
{
    /// <summary>
    /// Кол-во золота у игрока
    /// </summary>
    public int Gold { get; }

    /// <summary>
    /// Кол-во опыта у игрока
    /// </summary>
    public int Experience { get; }
}
