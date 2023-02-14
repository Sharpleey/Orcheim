using UnityEngine.Events;

/// <summary>
/// �������� ������� ��� SpawnEnemyManager-�
/// </summary>
public class SpawnEnemyEventManager
{
    #region Events
    /// <summary>
    /// ������� �������� ���������� ������ �� �����
    /// </summary>
    public static readonly UnityEvent<int> OnEnemiesRemaining = new UnityEvent<int>();
    #endregion

    #region Methods
    /// <summary>
    /// ����� ��� ������ ������� OnEnemiesRemaining
    /// </summary>
    /// <param name="enemiesRemaining"></param>
    public static void EnemiesRemaining(int enemiesRemaining)
    {
        OnEnemiesRemaining.Invoke(enemiesRemaining);
    }
    #endregion
}
