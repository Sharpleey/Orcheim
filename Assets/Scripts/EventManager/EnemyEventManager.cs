using UnityEngine.Events;

/// <summary>
/// �������� ������� ��� EnemyManager
/// </summary>
public class EnemyEventManager
{
    #region Events

    /// <summary>
    /// ������� ���������� ���������� ������ �� �����
    /// </summary>
    public static readonly UnityEvent<int> OnUpdateCountEnemiesRemaining = new UnityEvent<int>();

    /// <summary>
    /// ������� ���������� �������� ������ �� �����
    /// </summary>
    public static readonly UnityEvent<int> OnUpdateCountEnemyOnScene = new UnityEvent<int>();

    /// <summary>
    /// ������� ���������� �������� ������ � ����
    /// </summary>
    public static readonly UnityEvent<int> OnUpdateCountEnemyOnWavePool = new UnityEvent<int>();

    /// <summary>
    /// �������, ����� ���������� �� ����� �����������
    /// </summary>
    public static readonly UnityEvent OnEnemiesOver = new UnityEvent();

    #endregion

    #region Methods

    /// <summary>
    /// ����� ��� ������ ������� OnUpdateEnemiesRemaining
    /// </summary>
    /// <param name="enemiesRemaining">���-�� ���������� ������</param>
    public static void UpdateCountEnemiesRemaining(int countEnemiesRemaining)
    {
        OnUpdateCountEnemiesRemaining.Invoke(countEnemiesRemaining);
    }

    /// <summary>
    /// ����� ������ ������� OnUpdateCountEnemyOnScene
    /// </summary>
    /// <param name="countEnemyOnScene">���-�� ���������� ������ �� �����</param>
    public static void UpdateCountEnemyOnScene(int countEnemyOnScene)
    {
        OnUpdateCountEnemyOnScene.Invoke(countEnemyOnScene);
    }

    /// <summary>
    /// ����� ������ ������� OnUpdateCountEnemyOnWavePool
    /// </summary>
    /// <param name="enemiesRemaining">���-�� ���������� ������ � ����</param>
    public static void UpdateCountEnemyOnWavePool(int enemiesRemaining)
    {
        OnUpdateCountEnemyOnWavePool.Invoke(enemiesRemaining);
    }

    /// <summary>
    /// ����� ������ ������� OnEnemiesOver
    /// </summary>
    public static void EnemiesOver()
    {
        OnEnemiesOver.Invoke();
    }

    #endregion
}
