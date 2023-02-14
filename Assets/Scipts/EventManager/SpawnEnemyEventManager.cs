using UnityEngine.Events;

/// <summary>
/// Менеджер событий для SpawnEnemyManager-а
/// </summary>
public class SpawnEnemyEventManager
{
    #region Events
    /// <summary>
    /// Событие проверки оставшихся врагов на волне
    /// </summary>
    public static readonly UnityEvent<int> OnEnemiesRemaining = new UnityEvent<int>();
    #endregion

    #region Methods
    /// <summary>
    /// Метод для вызова события OnEnemiesRemaining
    /// </summary>
    /// <param name="enemiesRemaining"></param>
    public static void EnemiesRemaining(int enemiesRemaining)
    {
        OnEnemiesRemaining.Invoke(enemiesRemaining);
    }
    #endregion
}
