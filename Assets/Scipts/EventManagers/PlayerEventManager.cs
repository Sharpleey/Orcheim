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
    #endregion
}
