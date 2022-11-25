using UnityEngine.Events;

public class PlayerEventManager
{
    #region Events
    /// <summary>
    /// События, когда игроку нанесли урон
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerDamaged = new UnityEvent<int>();

    /// <summary>
    /// Событие смерти игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerDead = new UnityEvent();
    #endregion

    #region Methods
    /// <summary>
    /// Метод вызова события OnPlayerDamaged
    /// </summary>
    /// <param name="damage">Значение урона ненесенного игроку</param>
    public static void PlayerDamaged(int damage)
    {
        OnPlayerDamaged.Invoke(damage);
    }

    /// <summary>
    /// Вызов события смерти игрока
    /// </summary>
    public static void PlayerDead()
    {
        OnPlayerDead.Invoke();
    }
    #endregion
}
