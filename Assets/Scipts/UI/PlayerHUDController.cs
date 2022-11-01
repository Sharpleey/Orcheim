using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNotification;
    [SerializeField] private TextMeshProUGUI _waveCounter;
    [SerializeField] private TextMeshProUGUI _enemiesRemaining;
    [SerializeField] private TextMeshProUGUI _countEnemyOnScene;
    [SerializeField] private TextMeshProUGUI _countEnemyOnWavePool;

    private void Awake()
    {
        Messenger.AddListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, StartingNewGameModeOrccheim_EventHandler);
        Messenger<int>.AddListener(GlobalGameEvent.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger<int>.AddListener(GlobalGameEvent.WAVE_IN_COMMING, WaveIsComing_EventHandler);
        Messenger<int>.AddListener(GlobalGameEvent.ENEMIES_REMAINING, SetTextEnemiesRemaining);
        Messenger.AddListener(GlobalGameEvent.WAVE_IS_OVER, WaveIsOver_EventHandler);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, StartingNewGameModeOrccheim_EventHandler);
        Messenger<int>.RemoveListener(GlobalGameEvent.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger<int>.RemoveListener(GlobalGameEvent.WAVE_IN_COMMING, WaveIsComing_EventHandler);
        Messenger<int>.RemoveListener(GlobalGameEvent.ENEMIES_REMAINING, SetTextEnemiesRemaining);
        Messenger.RemoveListener(GlobalGameEvent.WAVE_IS_OVER, WaveIsOver_EventHandler);
    }

    private void Start()
    {
        if (_playerNotification)
            _playerNotification.text = "";
        if (_waveCounter)
            _waveCounter.text = "";
        if (_enemiesRemaining)
            _enemiesRemaining.text = "";
        if (_countEnemyOnScene)
            _countEnemyOnScene.text = "";
        if (_countEnemyOnWavePool)
            _countEnemyOnWavePool.text = "";
    }

    /// <summary>
    /// Вывод оповещения с задержкой на экран игрока
    /// </summary>
    /// <param name="text">Текст оповещения</param>
    /// <param name="delay">Задержка в секундах</param>
    /// <returns></returns>
    private IEnumerator SetTextPlayerNotificationWithDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);

        if(_playerNotification)
        {
            _playerNotification.text = text;
        }
    }

    /// <summary>
    /// Метод устанавлиает текст для счетчика волн
    /// </summary>
    /// <param name="wave">Номер волны</param>
    private void SetTextWaveCounter(int wave)
    {
        if (_waveCounter)
            _waveCounter.text = PlayerNotification.WAVE_COUNTER + wave.ToString();
    }

    /// <summary>
    /// Метод устанавливает текст для счетчика оставшихся врагов на волне
    /// </summary>
    /// <param name="enemiesRemaining">Кол-во оставшихся врагов</param>
    private void SetTextEnemiesRemaining(int enemiesRemaining)
    {
        if (_enemiesRemaining)
            _enemiesRemaining.text = PlayerNotification.ENEMIES_REMAIMING + enemiesRemaining.ToString();
    }

    private void StartingNewGameModeOrccheim_EventHandler()
    {
        StartCoroutine(SetTextPlayerNotificationWithDelay(PlayerNotification.CLEAR_VILLAGE, 5));
    }

    private void PreparingForWave_EventHandler(int wave)
    {
        StartCoroutine(SetTextPlayerNotificationWithDelay(PlayerNotification.PREPARING_FOR_WAVE, 0));
    }

    private void WaveIsComing_EventHandler(int wave)
    {
        StartCoroutine(SetTextPlayerNotificationWithDelay(PlayerNotification.WAVE_IS_COMING, 0));
        SetTextWaveCounter(wave);
    }

    private void WaveIsOver_EventHandler()
    {
        StartCoroutine(SetTextPlayerNotificationWithDelay(PlayerNotification.WAVE_IS_OVER, 0));
    }
}
