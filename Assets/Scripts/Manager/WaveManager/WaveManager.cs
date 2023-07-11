using System.Collections;
using UnityEngine;

/// <summary>
/// (P.S. In progress)
/// Менеджер волн. Отвечает за основую логику, механику волн врагов. Контролирует номер волны и рассылает сообщения остальным менеджерам
/// </summary>
public class WaveManager : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private WaveManagerConfig _config;
    #endregion

    #region Properties
    /// <summary>
    /// Номер текущей волны
    /// </summary>
    public int Wave
    {
        get => _wave;
        set
        {
            _wave = Mathf.Clamp(value, 1, int.MaxValue);
        }
    }
    private int _wave;
    #endregion Properties

    #region Private fields
    private bool _isFirstTriggerGame = false;

    private IEnumerator _coroutineBroadcastPreparingForWave;
    private IEnumerator _coroutineBroadcastWaveIsComing;
    #endregion

    #region Mono
    private void Awake()
    {
        AddListeners();
    }

    private void Start()
    {
        SetDefaultParameters();
    }
    #endregion Mono

    #region Private methods
    private void AddListeners()
    {
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        WaveEventManager.OnPreparingForWave.AddListener(EventHandler_PreparingForWave);
        EnemyEventManager.OnEnemiesOver.AddListener(EventHandler_EnemiesOver);
        GlobalGameEventManager.OnEnemyKilled.AddListener(EventHandler_EnemyKilled);
        GlobalGameEventManager.OnGameOver.AddListener(EventHandler_GameOver);
    }

    /// <summary>
    /// Метод устанавливает начальные настройки менеджера волн
    /// </summary>
    private void SetDefaultParameters()
    {
        _wave = _config.StartWave;
        _isFirstTriggerGame = false;
    }

    /// <summary>
    /// Метод рассылает с задержкой событие PREPARING_FOR_WAVE
    /// </summary>
    /// <param name="delay">Задержка до рассылки события</param>
    /// <returns></returns>
    private IEnumerator BroadcastPreparingForWave(int delay)
    {
#if UNITY_EDITOR
        Debug.Log("Preparing for wave" + _wave.ToString() + " in " + delay.ToString() + " second...");
#endif
        yield return new WaitForSeconds(delay);

        WaveEventManager.PreparingForWave(_wave);
    }

    /// <summary>
    /// Метод рассылает с задержкой событие WAVE_IN_COMMING
    /// </summary>
    /// <param name="delay">Задержка до рассылки события</param>
    /// <returns></returns>
    private IEnumerator BroadcastWaveIsComing(int delay)
    {
#if UNITY_EDITOR
        Debug.Log("Wave" + _wave.ToString() + " is coming in " + delay.ToString() + " second...");
#endif
        yield return new WaitForSeconds(delay);

        WaveEventManager.WaveIsComing(_wave);
    }

    #endregion Private methods

    #region Event handlers
    private void EventHandler_NewGame(GameMode gameMode)
    {
        SetDefaultParameters();
    }

    private void EventHandler_EnemyKilled(EnemyUnit enemyUnit)
    {
        if (!_isFirstTriggerGame)
        {
            _coroutineBroadcastPreparingForWave = BroadcastPreparingForWave(_config.DelayToFirstBroadcastPreparingForWave);
            StartCoroutine(_coroutineBroadcastPreparingForWave);

            _isFirstTriggerGame = true; //TODO
        }
    }

    private void EventHandler_PreparingForWave(int wave)
    {
        _coroutineBroadcastWaveIsComing = BroadcastWaveIsComing(_config.DelayToBroadcastWaveIsComing);
        StartCoroutine(_coroutineBroadcastWaveIsComing);
    }

    private void EventHandler_EnemiesOver()
    {
        WaveEventManager.WaveIsOver(_wave);

        Wave++;

        StartCoroutine(BroadcastPreparingForWave(_config.DelayToBroadcastPreparingForWave));
    }

    private void EventHandler_GameOver()
    {
        SetDefaultParameters();
        StopAllCoroutines();
    }
    #endregion
}
