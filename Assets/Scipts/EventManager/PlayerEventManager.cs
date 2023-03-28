using UnityEngine.Events;

public class PlayerEventManager
{
    #region Events
    /// <summary>
    /// Событие, когда игроку нанесли урон
    /// </summary>
    public static readonly UnityEvent<float> OnPlayerDamaged = new UnityEvent<float>();

    /// <summary>
    /// Событие смерти игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerDead = new UnityEvent();

    /// <summary>
    /// Событие повышения лвл игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerLevelUp = new UnityEvent();

    /// <summary>
    /// Событие изменения кол-ва здоровья у игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerHealthChanged = new UnityEvent();

    /// <summary>
    /// Событие изменения кол-ва брони у игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerArmorChanged = new UnityEvent();

    /// <summary>
    /// Событие изменения скорости передвижения у игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerMovementSpeedChanged = new UnityEvent();

    /// <summary>
    /// Событие изменения кол-ва золота у игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerGoldChanged = new UnityEvent();

    /// <summary>
    /// Событие изменения кол-ва опыта у игрока
    /// </summary>
    public static readonly UnityEvent OnPlayerExperienceChanged = new UnityEvent();

    /// <summary>
    /// Событие выбора игроком оружия ближнего боя
    /// </summary>
    public static readonly UnityEvent OnPlayerChooseMeleeWeapon = new UnityEvent();

    /// <summary>
    /// Событие выбора игроком оружия дальнего боя
    /// </summary>
    public static readonly UnityEvent OnPlayerChooseRangeWeapon = new UnityEvent();
    #endregion

    #region Methods

    /// <summary>
    /// Метод вызова события OnPlayerDamaged
    /// </summary>
    /// <param name="damage">Значение урона ненесенного игроку</param>
    public static void PlayerDamaged(float damage)
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
    public static void PlayerLevelUp()
    {
        OnPlayerLevelUp.Invoke();
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
    /// Вызов события изменения скорости передвижения игрока
    /// </summary>
    public static void PlayerMovementSpeedChanged()
    {
        OnPlayerMovementSpeedChanged.Invoke();
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
    /// Вызов события игрок выбрал оружие ближнего боя
    /// </summary>
    public static void PlayerChooseMeleeWeapon()
    {
        OnPlayerChooseMeleeWeapon.Invoke();
    }

    /// <summary>
    /// Вызов события игрок выбрал оружие дальнего боя
    /// </summary>
    public static void PlayerChooseRangeWeapon()
    {
        OnPlayerChooseRangeWeapon.Invoke();
    }

    #endregion
}
