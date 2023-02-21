using UnityEngine.Events;

/// <summary>
/// �������� ������� ��� WaveManager-�
/// </summary>
public class WaveEventManager
{
    #region Events

    /// <summary>
    /// ������� ������� ������� ������ ������ ����. ������� ������� ��������� ����� �� ����������, ����� �������� ���������� ����, ����� ����
    /// </summary>
    public static readonly UnityEvent OnStartWaveLogic = new UnityEvent();

    /// <summary>
    /// �������, ����� ����� ������ �����������
    /// </summary>
    public static readonly UnityEvent<int> OnWaveIsOver = new UnityEvent<int>();

    /// <summary>
    /// ������� ���������� �����
    /// </summary>
    public static readonly UnityEvent<int> OnPreparingForWave = new UnityEvent<int>();

    /// <summary>
    /// ������� ������ �����
    /// </summary>
    public static readonly UnityEvent<int> OnWaveIsComing = new UnityEvent<int>();

    #endregion

    #region Methods

    /// <summary>
    /// ����� ������ ������� OnStartingTrigger
    /// </summary>
    public static void StartingTrigger()
    {
        OnStartWaveLogic.Invoke();
    }

    /// <summary>
    /// ����� ������ ������� OnWaveIsOver
    /// </summary>
    public static void WaveIsOver(int wave)
    {
        OnWaveIsOver.Invoke(wave);
    }

    /// <summary>
    /// ����� ������ ������� OnPreparingForWave
    /// </summary>
    public static void PreparingForWave(int wave)
    {
        OnPreparingForWave.Invoke(wave);
    }

    /// <summary>
    /// ����� ������ ������� OnWaveIsComing
    /// </summary>
    public static void WaveIsComing(int wave)
    {
        OnWaveIsComing.Invoke(wave);
    }

    #endregion
}
