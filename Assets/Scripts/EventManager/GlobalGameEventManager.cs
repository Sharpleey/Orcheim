using UnityEngine.Events;

/// <summary>
/// Менеджер глобальных событий происходящих в игре
/// </summary>
public class GlobalGameEventManager
{
    #region Events

    /// <summary>
    /// События окончания игры
    /// </summary>
    public static readonly UnityEvent OnGameOver = new UnityEvent();

    /// <summary>
    /// Событие убийства врага
    /// </summary>
    public static readonly UnityEvent<EnemyUnit> OnEnemyKilled = new UnityEvent<EnemyUnit>();

    public static readonly UnityEvent<GameMode> OnNewGame = new UnityEvent<GameMode>();

    #endregion

    #region Methods

    /// <summary>
    /// Метод отправки события OnGameOver
    /// </summary>
    public static void GameOver()
    {
        OnGameOver.Invoke();
    }

    /// <summary>
    /// Метод отправки события OnEnemyKilled
    /// </summary>
    public static void EnemyKilled(EnemyUnit enemyUnit)
    {
        OnEnemyKilled.Invoke(enemyUnit);
    }

    /// <summary>
    /// Метод отправки события OnEnemyKilled
    /// </summary>
    public static void NewGame(GameMode gameMode)
    {
        OnNewGame.Invoke(gameMode);
    }
    #endregion
}
