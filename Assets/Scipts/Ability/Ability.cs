
/// <summary>
/// Абстрактный класс способности
/// </summary>
public abstract class Ability
{
    /// <summary>
    /// Активна способность или нет
    /// </summary>
    public bool IsActive { get; protected set; }

    protected Unit unit;

    public Ability(Unit unit, bool isActive = false)
    {
        this.unit = unit;
        IsActive = isActive;
    }
}
