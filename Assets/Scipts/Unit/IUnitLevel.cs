
/// <summary>
/// Имеет уровень
/// </summary>
public interface IUnitLevel
{
    /// <summary>
    /// Уровень юнита
    /// </summary>
    public int Level { get; }

    /// <summary>
    /// Повысить уровень юнита на определенное кол-во уровней (По умолчанию на 1)
    /// </summary>
    /// <param name="levelUp">На сколько уровней хотим увеличить</param>
    void LevelUp(int levelUp = 1);

    /// <summary>
    /// Устанавливает определенный уровень юнита
    /// </summary>
    /// <param name="newLevel">Уровень, который хотим установить</param>
    void SetLevel(int newLevel);
}

