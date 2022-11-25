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
    public int Wave => _wave;

    private bool _isFirstTriggerGame = false;

    private IEnumerator _coroutineBroadcastPreparingForWave;
    private IEnumerator _coroutineBroadcastWaveIsComing;

    public void Startup()
    {
        Debug.Log("Wave manager starting...");

        Status = ManagerStatus.Started;
    }

    public static class Event
    {
        #region Events for manager
        public const string FIRST_TRIGGER_GAME = "FIRST_TRIGGER_GAME";
        public const string WAVE_IS_OVER = "WAVE_IS_OVER";
        #endregion

        #region Events broadcast by manager
        public const string PREPARING_FOR_WAVE = "PREPARING_FOR_WAVE";
        public const string WAVE_IN_COMMING = "WAVE_IN_COMMING";
        #endregion
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim_EventHandler);
        Messenger.AddListener(Event.FIRST_TRIGGER_GAME, FirstTriggerGame_EventHandler);
        Messenger<int>.AddListener(Event.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger.AddListener(Event.WAVE_IS_OVER, WaveIsOver_EventHandler);
        Messenger.AddListener(GameEvent.GAME_OVER, GameOver_EventHandler);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim_EventHandler);
        Messenger.RemoveListener(Event.FIRST_TRIGGER_GAME, FirstTriggerGame_EventHandler);
        Messenger<int>.RemoveListener(Event.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger.RemoveListener(Event.WAVE_IS_OVER, WaveIsOver_EventHandler);
        Messenger.RemoveListener(GameEvent.GAME_OVER, GameOver_EventHandler);
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

        Messenger<int>.Broadcast(Event.PREPARING_FOR_WAVE, _wave);
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

        Messenger<int>.Broadcast(Event.WAVE_IN_COMMING, _wave);
    }

    private void NewGameModeOrccheim_EventHandler()
    {
        SetDefaultParameters();
    }

    private void FirstTriggerGame_EventHandler()
    {
        if (!_isFirstTriggerGame)
        {
            _coroutineBroadcastPreparingForWave = BroadcastPreparingForWave(_delayToFirstBroadcastPreparingForWave);
            StartCoroutine(_coroutineBroadcastPreparingForWave);

            _isFirstTriggerGame = true; //TODO
        }
    }

    private void PreparingForWave_EventHandler(int wave)
    {
        _coroutineBroadcastWaveIsComing = BroadcastWaveIsComing(_delayToBroadcastWaveIsComing);
        StartCoroutine(_coroutineBroadcastWaveIsComing);
    }

    private void WaveIsOver_EventHandler()
    {
        SetNumWave(_wave + 1);
        StartCoroutine(BroadcastPreparingForWave(_delayToBroadcastPreparingForWave));
    }

    private void GameOver_EventHandler()
    {
        SetDefaultParameters();
        StopAllCoroutines();
    }
}
