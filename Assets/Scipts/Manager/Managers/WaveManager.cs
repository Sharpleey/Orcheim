using System.Collections;
using UnityEngine;

/// <summary>
/// (P.S. In progress)
/// Менеджер волн. Отвечает за основую логику, механику волн врагов. Контролирует номер волны и рассылает сообщения остальным менеджерам
/// </summary>
public class WaveManager : MonoBehaviour, IGameManager
{
    public static WaveManager Instance { get; private set; }

    #region Serialize fields

    [SerializeField] private int _wave = 1;

    [Space(10)]
    [SerializeField] private int _delayToFirstBroadcastPreparingForWave = 30;
    [SerializeField] private int _delayToBroadcastWaveIsComing = 10;
    [SerializeField] private int _delayToBroadcastPreparingForWave = 5;

    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    #endregion Properties

    #region Private fields

    private bool _isFirstTriggerGame = false;

    private IEnumerator _coroutineBroadcastPreparingForWave;
    private IEnumerator _coroutineBroadcastWaveIsComing;

    #endregion Private fields

    #region Mono
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        AddListeners();
    }

    #endregion Mono

    #region Private methods

    private void AddListeners()
    {
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        WaveEventManager.OnPreparingForWave.AddListener(EventHandler_PreparingForWave);
        EnemyEventManager.OnEnemiesOver.AddListener(EventHandler_EnemiesOver);
        WaveEventManager.OnStartWaveLogic.AddListener(EventHandler_StartWaveLogic);
        GlobalGameEventManager.OnGameOver.AddListener(EventHandler_GameOver);
    }

    /// <summary>
    /// Метод устанавливает начальные настройки менеджера волн
    /// </summary>
    private void SetDefaultParameters()
    {
        _wave = 1;
        _isFirstTriggerGame = false;
    }

    /// <summary>
    /// Метод устанавливает номер волны
    /// </summary>
    /// <param name="numWave">Номер волны</param>
    private void SetNumWave(int numWave)
    {
        _wave = numWave;
    }

    /// <summary>
    /// Метод рассылает с задержкой событие PREPARING_FOR_WAVE
    /// </summary>
    /// <param name="delay">Задержка до рассылки события</param>
    /// <returns></returns>
    private IEnumerator BroadcastPreparingForWave(int delay)
    {
        Debug.Log("Preparing for wave" + _wave.ToString() + " in " + delay.ToString() + " second...");

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
        Debug.Log("Wave" + _wave.ToString() + " is coming in " + delay.ToString() + " second...");

        yield return new WaitForSeconds(delay);

        WaveEventManager.WaveIsComing(_wave);
    }

    #endregion Private methods

    #region Public methods

    public void Startup()
    {
        Debug.Log("Wave manager starting...");

        Status = ManagerStatus.Started;
    }


    #endregion Public methods

    #region Event handlers
    private void EventHandler_NewGame(GameMode gameMode)
    {
        SetDefaultParameters();
    }

    private void EventHandler_StartWaveLogic()
    {
        if (!_isFirstTriggerGame)
        {
            _coroutineBroadcastPreparingForWave = BroadcastPreparingForWave(_delayToFirstBroadcastPreparingForWave);
            StartCoroutine(_coroutineBroadcastPreparingForWave);

            _isFirstTriggerGame = true; //TODO
        }
    }

    private void EventHandler_PreparingForWave(int wave)
    {
        _coroutineBroadcastWaveIsComing = BroadcastWaveIsComing(_delayToBroadcastWaveIsComing);
        StartCoroutine(_coroutineBroadcastWaveIsComing);
    }

    private void EventHandler_EnemiesOver()
    {
        WaveEventManager.WaveIsOver(_wave);

        SetNumWave(_wave + 1);
        StartCoroutine(BroadcastPreparingForWave(_delayToBroadcastPreparingForWave));
    }

    private void EventHandler_GameOver()
    {
        SetDefaultParameters();
        StopAllCoroutines();
    }
    #endregion
}
