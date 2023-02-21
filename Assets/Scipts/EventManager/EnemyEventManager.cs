using UnityEngine.Events;

/// <summary>
/// Менеджер событий для EnemyManager
/// </summary>
public class EnemyEventManager
{
    #region Events

    /// <summary>
    /// Событие обновления оставшихся врагов на волне
    /// </summary>
    public static readonly UnityEvent<int> OnUpdateCountEnemiesRemaining = new UnityEvent<int>();

    /// <summary>
    /// Событие обновления счетчика врагов на сцене
    /// </summary>
    public static readonly UnityEvent<int> OnUpdateCountEnemyOnScene = new UnityEvent<int>();

    /// <summary>
    /// Событие обновления счетчика врагов в пуле
    /// </summary>
    public static readonly UnityEvent<int> OnUpdateCountEnemyOnWavePool = new UnityEvent<int>();

    /// <summary>
    /// Событие, когда противника на волне закончились
    /// </summary>
    public static readonly UnityEvent OnEnemiesOver = new UnityEvent();

    #endregion

    #region Methods

    /// <summary>
    /// Метод для вызова события OnUpdateEnemiesRemaining
    /// </summary>
    /// <param name="enemiesRemaining">Кол-во оставшихся врагов</param>
    public static void UpdateCountEnemiesRemaining(int countEnemiesRemaining)
    {
        OnUpdateCountEnemiesRemaining.Invoke(countEnemiesRemaining);
    }

    /// <summary>
    /// Метод вызова события OnUpdateCountEnemyOnScene
    /// </summary>
    /// <param name="countEnemyOnScene">Кол-во оставшихся врагов на сцене</param>
    public static void UpdateCountEnemyOnScene(int countEnemyOnScene)
    {
        OnUpdateCountEnemyOnScene.Invoke(countEnemyOnScene);
    }

    /// <summary>
    /// Метод вызова события OnUpdateCountEnemyOnWavePool
    /// </summary>
    /// <param name="enemiesRemaining">Кол-во оставшихся врагов в пуле</param>
    public static void UpdateCountEnemyOnWavePool(int enemiesRemaining)
    {
        OnUpdateCountEnemyOnWavePool.Invoke(enemiesRemaining);
    }

    /// <summary>
    /// Метод вызова события OnEnemiesOver
    /// </summary>
    public static void EnemiesOver()
    {
        OnEnemiesOver.Invoke();
    }

    #endregion
}
