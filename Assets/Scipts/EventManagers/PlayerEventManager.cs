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

    /// <summary>
    /// Событие повышения лвл игрока
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerLevelUp = new UnityEvent<int>();

    public static readonly UnityEvent OnPlayerHealthChanged = new UnityEvent();

    public static readonly UnityEvent OnPlayerArmorChanged = new UnityEvent();

    public static readonly UnityEvent OnPlayerGoldChanged = new UnityEvent();

    public static readonly UnityEvent OnPlayerExperienceChanged = new UnityEvent();

    public static readonly UnityEvent OnPlayerLevelChanged = new UnityEvent();
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

    /// <summary>
    /// Вызов события повышения уровня игрока
    /// </summary>
    public static void PlayerLevelUp(int level)
    {
        OnPlayerLevelUp.Invoke(level);
    }

    /// <summary>
    /// Вызов события изменения здоровья игрока
    /// </summary>
    public static void PlayerHealthChanged()
    {
        OnPlayerHealthChanged.Invoke();
    }

    /// <summary>
    /// Вызов события изменения брони игрока
    /// </summary>
    public static void PlayerArmorChanged()
    {
        OnPlayerArmorChanged.Invoke();
    }

    /// <summary>
    /// Вызов события изменения золота игрока
    /// </summary>
    public static void PlayerGoldChanged()
    {
        OnPlayerGoldChanged.Invoke();
    }

    /// <summary>
    /// Вызов события изменения опыта игрока
    /// </summary>
    public static void PlayerExperienceChanged()
    {
        OnPlayerExperienceChanged.Invoke();
    }

    /// <summary>
    /// Вызов события изменения уровня игрока
    /// </summary>
    public static void PlayerLevelUp()
    {
        OnPlayerLevelChanged.Invoke();
    }
    #endregion
}
