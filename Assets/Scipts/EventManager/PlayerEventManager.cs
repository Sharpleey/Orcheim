using UnityEngine.Events;

public class PlayerEventManager
{
    #region Events
    /// <summary>
    /// �������, ����� ������ ������� ����
    /// </summary>
    public static readonly UnityEvent<float> OnPlayerDamaged = new UnityEvent<float>();

    /// <summary>
    /// ������� ������ ������
    /// </summary>
    public static readonly UnityEvent OnPlayerDead = new UnityEvent();

    /// <summary>
    /// ������� ��������� ��� ������
    /// </summary>
    public static readonly UnityEvent OnPlayerLevelUp = new UnityEvent();

    /// <summary>
    /// ������� ��������� ���-�� �������� � ������
    /// </summary>
    public static readonly UnityEvent OnPlayerHealthChanged = new UnityEvent();

    /// <summary>
    /// ������� ��������� ���-�� ����� � ������
    /// </summary>
    public static readonly UnityEvent OnPlayerArmorChanged = new UnityEvent();

    /// <summary>
    /// ������� ��������� �������� ������������ � ������
    /// </summary>
    public static readonly UnityEvent OnPlayerMovementSpeedChanged = new UnityEvent();

    /// <summary>
    /// ������� ��������� ���-�� ������ � ������
    /// </summary>
    public static readonly UnityEvent OnPlayerGoldChanged = new UnityEvent();

    /// <summary>
    /// ������� ��������� ���-�� ����� � ������
    /// </summary>
    public static readonly UnityEvent OnPlayerExperienceChanged = new UnityEvent();

    /// <summary>
    /// ������� ������ ������� ������ �������� ���
    /// </summary>
    public static readonly UnityEvent OnPlayerChooseMeleeWeapon = new UnityEvent();

    /// <summary>
    /// ������� ������ ������� ������ �������� ���
    /// </summary>
    public static readonly UnityEvent OnPlayerChooseRangeWeapon = new UnityEvent();
    #endregion

    #region Methods

    /// <summary>
    /// ����� ������ ������� OnPlayerDamaged
    /// </summary>
    /// <param name="damage">�������� ����� ����������� ������</param>
    public static void PlayerDamaged(float damage)
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
    public static void PlayerLevelUp()
    {
        OnPlayerLevelUp.Invoke();
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
    /// ����� ������� ��������� �������� ������������ ������
    /// </summary>
    public static void PlayerMovementSpeedChanged()
    {
        OnPlayerMovementSpeedChanged.Invoke();
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
    /// ����� ������� ����� ������ ������ �������� ���
    /// </summary>
    public static void PlayerChooseMeleeWeapon()
    {
        OnPlayerChooseMeleeWeapon.Invoke();
    }

    /// <summary>
    /// ����� ������� ����� ������ ������ �������� ���
    /// </summary>
    public static void PlayerChooseRangeWeapon()
    {
        OnPlayerChooseRangeWeapon.Invoke();
    }

    #endregion
}
