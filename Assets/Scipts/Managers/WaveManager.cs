using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour, IGameManager
{
    [SerializeField] private int _wave = 1;
    public ManagerStatus Status { get; private set; }
    public int Wave => _wave;

    private bool _isFirstTriggerGame = false;

    public void Startup()
    {
        Debug.Log("Wave manager starting...");

        Status = ManagerStatus.Started;
    }

    private void Awake()
    {
        Messenger.AddListener(GlobalGameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim_EventHandler);
        Messenger.AddListener(GlobalGameEvent.FIRST_TRIGGER_GAME, FirstTriggerGame_EventHandler);
        Messenger<int>.AddListener(GlobalGameEvent.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger.AddListener(GlobalGameEvent.WAVE_IS_OVER, WaveIsOver_EventHandler);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GlobalGameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim_EventHandler);
        Messenger.RemoveListener(GlobalGameEvent.FIRST_TRIGGER_GAME, FirstTriggerGame_EventHandler);
        Messenger<int>.RemoveListener(GlobalGameEvent.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger.RemoveListener(GlobalGameEvent.WAVE_IS_OVER, WaveIsOver_EventHandler);
    }

    /// <summary>
    /// Метод устанавливает начальные настройки менеджера волн
    /// </summary>
    private void SetDefaultParameters()
    {
        _wave = 1;
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
    private IEnumerator BroadcastPreparingForWave(float delay)
    {
        Debug.Log("Preparing for wave" + _wave.ToString() + " in " + delay.ToString() + " second...");

        yield return new WaitForSeconds(delay);

        Messenger<int>.Broadcast(GlobalGameEvent.PREPARING_FOR_WAVE, _wave);
    }

    /// <summary>
    /// Метод рассылает с задержкой событие WAVE_IN_COMMING
    /// </summary>
    /// <param name="delay">Задержка до рассылки события</param>
    /// <returns></returns>
    private IEnumerator BroadcastWaveIsComing(float delay)
    {
        Debug.Log("Wave" + _wave.ToString() + " is coming in " + delay.ToString() + " second...");

        yield return new WaitForSeconds(delay);

        Messenger<int>.Broadcast(GlobalGameEvent.WAVE_IN_COMMING, _wave);
    }
    private void NewGameModeOrccheim_EventHandler()
    {
        SetDefaultParameters();
    }
    private void FirstTriggerGame_EventHandler()
    {
        if (!_isFirstTriggerGame)
        {
            StartCoroutine(BroadcastPreparingForWave(30));
            _isFirstTriggerGame = true; //TODO
        }
    }
    private void PreparingForWave_EventHandler(int wave)
    {
        StartCoroutine(BroadcastWaveIsComing(15));
    }
    private void WaveIsOver_EventHandler()
    {
        SetNumWave(_wave + 1);
        StartCoroutine(BroadcastPreparingForWave(5));
    }
}
