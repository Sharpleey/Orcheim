
/// <summary>
/// Абстрактный класс награды
/// </summary>
public abstract class Award
{
    /// <summary>
    /// Название типа награды
    /// </summary>
    public string TypeName { get; protected set; }

    /// <summary>
    /// Название предмета, способности, эффекта или т.п., т.е. то что мы улучшаем
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Описание награды, улучшения
    /// </summary>
    public string Description { get; protected set; }
}
