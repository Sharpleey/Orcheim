using UnityEngine.Events;

/// <summary>
/// Менеджер событий для WaveManager-а
/// </summary>
public class WaveEventManager
{
    #region Events

    /// <summary>
    /// Событие запуска игровой логики спавна волн. Событие первого нанесение урона по противнику, после которого начинается игра, спавн волн
    /// </summary>
    public static readonly UnityEvent OnStartWaveLogic = new UnityEvent();

    /// <summary>
    /// Событие, когда волна врагов закончилась
    /// </summary>
    public static readonly UnityEvent<int> OnWaveIsOver = new UnityEvent<int>();

    /// <summary>
    /// Событие подготовки волны
    /// </summary>
    public static readonly UnityEvent<int> OnPreparingForWave = new UnityEvent<int>();

    /// <summary>
    /// Событик начала волны
    /// </summary>
    public static readonly UnityEvent<int> OnWaveIsComing = new UnityEvent<int>();

    #endregion

    #region Methods

    /// <summary>
    /// Метод вызова события OnStartingTrigger
    /// </summary>
    public static void StartingTrigger()
    {
        OnStartWaveLogic.Invoke();
    }

    /// <summary>
    /// Метод вызова события OnWaveIsOver
    /// </summary>
    public static void WaveIsOver(int wave)
    {
        OnWaveIsOver.Invoke(wave);
    }

    /// <summary>
    /// Метод вызова события OnPreparingForWave
    /// </summary>
    public static void PreparingForWave(int wave)
    {
        OnPreparingForWave.Invoke(wave);
    }

    /// <summary>
    /// Метод вызова события OnWaveIsComing
    /// </summary>
    public static void WaveIsComing(int wave)
    {
        OnWaveIsComing.Invoke(wave);
    }

    #endregion
}
