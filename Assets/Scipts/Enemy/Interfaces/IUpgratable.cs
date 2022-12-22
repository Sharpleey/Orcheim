
public interface IUpgratable
{
    /// <summary>
    /// Максимальный уровень улучшения
    /// </summary>
    public int MaxLevel { get; }

    /// <summary>
    /// Уровень прокачки
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Значение прироста прокачеваемого параметра
    /// </summary>
    public float UpgradeValue { get; }

    /// <summary>
    /// Описание улучшения
    /// </summary>
    public string DescriptionUpdate { get; }

    /// <summary>
    /// Прокачать параметр, повысить уровень (по умолчанию на 1)
    /// </summary>
    /// <param name="levelUp">На сколько уровней пркоачать</param>
    public void Upgrade(int levelUp = 1);

    /// <summary>
    /// Установить определенный уровень параметра
    /// </summary>
    /// <param name="level"></param>
    public void SetLevel(int level);
}
