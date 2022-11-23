using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNotification;
    [SerializeField] private TextMeshProUGUI _waveCounter;
    [SerializeField] private TextMeshProUGUI _enemiesRemaining;

    [Header("Health Bar")]
    [SerializeField] private TextMeshProUGUI _healthPlayer;
    [SerializeField] private Slider _healthSlider;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, StartingNewGameModeOrccheim_EventHandler);
        Messenger<int>.AddListener(WaveManager.Event.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger<int>.AddListener(WaveManager.Event.WAVE_IN_COMMING, WaveIsComing_EventHandler);
        Messenger<int>.AddListener(SpawnEnemyManager.Event.ENEMIES_REMAINING, SetTextEnemiesRemaining);
        Messenger.AddListener(WaveManager.Event.WAVE_IS_OVER, WaveIsOver_EventHandler);
        Messenger<int>.AddListener(PlayerManager.Event.TAKE_DAMAGE, PlayerDamaged_EventHandler);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, StartingNewGameModeOrccheim_EventHandler);
        Messenger<int>.RemoveListener(WaveManager.Event.PREPARING_FOR_WAVE, PreparingForWave_EventHandler);
        Messenger<int>.RemoveListener(WaveManager.Event.WAVE_IN_COMMING, WaveIsComing_EventHandler);
        Messenger<int>.RemoveListener(SpawnEnemyManager.Event.ENEMIES_REMAINING, SetTextEnemiesRemaining);
        Messenger.RemoveListener(WaveManager.Event.WAVE_IS_OVER, WaveIsOver_EventHandler);
        Messenger<int>.AddListener(PlayerManager.Event.TAKE_DAMAGE, PlayerDamaged_EventHandler);
    }

    private void Start()
    {
        if (_playerNotification)
            _playerNotification.text = "";
        if (_waveCounter)
            _waveCounter.text = "";
        if (_enemiesRemaining)
            _enemiesRemaining.text = "";
        if (_healthPlayer)
            _healthPlayer.text = "";
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
            _enemiesRemaining.text = PlayerNotification.ENEMIES_REMAINING + enemiesRemaining.ToString();
    }

    private void SetTextHealthPlayer(int currentHealth, int maxHealth)
    {
        if (_healthPlayer)
            _healthPlayer.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    private void SetValueHealthBar(int currentHealth, int maxHealth)
    {
        if (_healthSlider)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = currentHealth;
        }
            
    }

    private void StartingNewGameModeOrccheim_EventHandler()
    {
        StartCoroutine(SetTextPlayerNotificationWithDelay(PlayerNotification.CLEAR_VILLAGE, 5));

        try
        {
            SetTextHealthPlayer(Managers.PlayerManager.Health, Managers.PlayerManager.MaxHealth);
            SetValueHealthBar(Managers.PlayerManager.Health, Managers.PlayerManager.MaxHealth);
        }
        catch (Exception exeption)
        {
            Debug.Log(exeption.Message);
        }
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

    private void PlayerDamaged_EventHandler(int damage)
    {
        try
        {
            SetTextHealthPlayer(Managers.PlayerManager.Health, Managers.PlayerManager.MaxHealth);
            SetValueHealthBar(Managers.PlayerManager.Health, Managers.PlayerManager.MaxHealth);
        }
        catch (Exception exeption)
        {
            Debug.Log(exeption.Message);
        }
    }
}
