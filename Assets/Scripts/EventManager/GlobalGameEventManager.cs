using UnityEngine.Events;

/// <summary>
/// �������� ���������� ������� ������������ � ����
/// </summary>
public class GlobalGameEventManager
{
    #region Events

    /// <summary>
    /// ������� ��������� ����
    /// </summary>
    public static readonly UnityEvent OnGameOver = new UnityEvent();

    /// <summary>
    /// ������� �������� �����
    /// </summary>
    public static readonly UnityEvent<EnemyUnit> OnEnemyKilled = new UnityEvent<EnemyUnit>();

    public static readonly UnityEvent<GameMode> OnNewGame = new UnityEvent<GameMode>();

    #endregion

    #region Methods

    /// <summary>
    /// ����� �������� ������� OnGameOver
    /// </summary>
    public static void GameOver()
    {
        OnGameOver.Invoke();
    }

    /// <summary>
    /// ����� �������� ������� OnEnemyKilled
    /// </summary>
    public static void EnemyKilled(EnemyUnit enemyUnit)
    {
        OnEnemyKilled.Invoke(enemyUnit);
    }

    /// <summary>
    /// ����� �������� ������� OnEnemyKilled
    /// </summary>
    public static void NewGame(GameMode gameMode)
    {
        OnNewGame.Invoke(gameMode);
    }
    #endregion
}
