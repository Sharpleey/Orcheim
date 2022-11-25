using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour, IGameManager
{
    [SerializeField] private int _wave = 1;

    [Space(10)]
    [SerializeField] private int _delayToFirstBroadcastPreparingForWave = 30;
    [SerializeField] private int _delayToBroadcastWaveIsComing = 10;
    [SerializeField] private int _delayToBroadcastPreparingForWave = 5;

    public ManagerStatus Status { get; private set; }

    private bool _isFirstTriggerGame = false;

    private IEnumerator _coroutineBroadcastPreparingForWave;
    private IEnumerator _coroutineBroadcastWaveIsComing;

    public void Startup()
    {
        Debug.Log("Wave manager starting...");

        Status = ManagerStatus.Started;
    }

    private void Awake()
    {
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        WaveEventManager.OnPreparingForWave.AddListener(EventHandler_PreparingForWave);
        WaveEventManager.OnWaveIsOver.AddListener(EventHandler_WaveIsOver);
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

    private void EventHandler_WaveIsOver()
    {
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
