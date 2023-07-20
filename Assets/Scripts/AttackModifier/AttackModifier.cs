
public abstract class AttackModifier : IEntity
{
    /// <summary>
    /// Название модификатора атаки
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Описание модификатора атаки
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// Активен модификатор или нет
    /// </summary>
    public bool IsActive { get; set; }

    public AttackModifier(bool isActive = true)
    {
        IsActive = isActive;
    }
}
