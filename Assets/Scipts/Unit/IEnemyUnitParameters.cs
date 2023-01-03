
public interface IEnemyUnitParameters : IUnitParameters
{
    /// <summary>
    /// Дистанция атаки юнита противника
    /// </summary>
    public float AttackDistance { get; }

    /// <summary>
    /// Стоимость за убиство в золоте
    /// </summary>
    public CostInGold CostInGold { get; }

    /// <summary>
    /// Стоимость за убийство в опыте
    /// </summary>
    public CostInExp CostInExp { get; }
}
