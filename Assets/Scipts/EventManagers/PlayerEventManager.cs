using UnityEngine.Events;

public class PlayerEventManager
{
    #region Events
    /// <summary>
    /// �������, ����� ������ ������� ����
    /// </summary>
    public static readonly UnityEvent<int> OnPlayerDamaged = new UnityEvent<int>();

    /// <summary>
    /// ������� ������ ������
    /// </summary>
    public static readonly UnityEvent OnPlayerDead = new UnityEvent();

    /// <summary>
    /// ������� ��������� ��� ������
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
    /// ����� ������ ������� OnPlayerDamaged
    /// </summary>
    /// <param name="damage">�������� ����� ����������� ������</param>
    public static void PlayerDamaged(int damage)
    {
        OnPlayerDamaged.Invoke(damage);
    }

    /// <summary>
    /// ����� ������� ������ ������
    /// </summary>
    public static void PlayerDead()
    {
        OnPlayerDead.Invoke();
    }

    /// <summary>
    /// ����� ������� ��������� ������ ������
    /// </summary>
    public static void PlayerLevelUp(int level)
    {
        OnPlayerLevelUp.Invoke(level);
    }

    /// <summary>
    /// ����� ������� ��������� �������� ������
    /// </summary>
    public static void PlayerHealthChanged()
    {
        OnPlayerHealthChanged.Invoke();
    }

    /// <summary>
    /// ����� ������� ��������� ����� ������
    /// </summary>
    public static void PlayerArmorChanged()
    {
        OnPlayerArmorChanged.Invoke();
    }

    /// <summary>
    /// ����� ������� ��������� ������ ������
    /// </summary>
    public static void PlayerGoldChanged()
    {
        OnPlayerGoldChanged.Invoke();
    }

    /// <summary>
    /// ����� ������� ��������� ����� ������
    /// </summary>
    public static void PlayerExperienceChanged()
    {
        OnPlayerExperienceChanged.Invoke();
    }

    /// <summary>
    /// ����� ������� ��������� ������ ������
    /// </summary>
    public static void PlayerLevelUp()
    {
        OnPlayerLevelChanged.Invoke();
    }
    #endregion
}
