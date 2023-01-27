using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI _notification;

    [Header("Wave info")]
    //[SerializeField] private TextMeshProUGUI _waveCounter;
    //[SerializeField] private TextMeshProUGUI _enemiesRemaining;

    [Header("Health Bar")]
    [SerializeField] private TextMeshProUGUI _healthPlayer;
    [SerializeField] private Slider _healthSlider;

    [Header("Armor")]
    [SerializeField] private TextMeshProUGUI _armorPlayer;

    [Header("Experience")]
    [SerializeField] private TextMeshProUGUI _experiencePlayer;

    [Header("Gold")]
    [SerializeField] private TextMeshProUGUI _goldPlayer;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI _levelPlayer;

    private PlayerUnit _playerUnit;

    private void Awake()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(StartingNewGameModeOrccheim_EventHandler);
        PlayerEventManager.OnPlayerDamaged.AddListener(PlayerDamaged_EventHandler);
        WaveEventManager.OnPreparingForWave.AddListener(PreparingForWave_EventHandler);
        WaveEventManager.OnWaveIsComing.AddListener(WaveIsComing_EventHandler);
        WaveEventManager.OnWaveIsOver.AddListener(WaveIsOver_EventHandler);
        //SpawnEnemyEventManager.OnEnemiesRemaining.AddListener(SetTextEnemiesRemaining);
    }

    private void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        ClearAllText();

        if (_playerUnit)
        {
            SetTextHealthBar(_playerUnit.Health.Actual, _playerUnit.Health.Max);
            SetTextArmor(_playerUnit.Armor.Actual);
            SetTextGold(_playerUnit.Gold);
            SetTextExperience(_playerUnit.Experience);
            SetTextLevel(_playerUnit.Level);
        }
    }

    private void ClearAllText()
    {
        if (_notification)
            _notification.text = "";
        //if (_waveCounter)
        //    _waveCounter.text = "";
        //if (_enemiesRemaining)
        //    _enemiesRemaining.text = "";
        if (_healthPlayer)
            _healthPlayer.text = "";
        if (_armorPlayer)
            _armorPlayer.text = "";
        if (_experiencePlayer)
            _experiencePlayer.text = "";
        if (_goldPlayer)
            _goldPlayer.text = "";
        if (_levelPlayer)
            _levelPlayer.text = "";
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

        if (_notification)
            _notification.text = text;
    }

    ///// <summary>
    ///// Метод устанавлиает текст для счетчика волн
    ///// </summary>
    ///// <param name="wave">Номер волны</param>
    //private void SetTextWaveCounter(int wave)
    //{
    //    if (_waveCounter)
    //        _waveCounter.text = PlayerNotification.WAVE_COUNTER + wave.ToString();
    //}

    ///// <summary>
    ///// Метод устанавливает текст для счетчика оставшихся врагов на волне
    ///// </summary>
    ///// <param name="enemiesRemaining">Кол-во оставшихся врагов</param>
    //private void SetTextEnemiesRemaining(int enemiesRemaining)
    //{
    //    if (_enemiesRemaining)
    //        _enemiesRemaining.text = PlayerNotification.ENEMIES_REMAINING + enemiesRemaining.ToString();
    //}

    private void SetTextHealthBar(int currentHealth, int maxHealth)
    {
        if (_healthPlayer)
            _healthPlayer.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        SetValueHealthBarSlider(_playerUnit.Health.Actual, _playerUnit.Health.Max);
    }

    private void SetValueHealthBarSlider(int currentHealth, int maxHealth)
    {
        if (_healthSlider)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = currentHealth;
        }
    }

    private void SetTextArmor(int armor)
    {
        if(_armorPlayer)
            _armorPlayer.text = armor.ToString();
    }

    private void SetTextLevel(int level)
    {
        if (_levelPlayer)
            _levelPlayer.text = level.ToString();
    }

    private void SetTextGold(int gold)
    {
        if (_goldPlayer)
            _goldPlayer.text = gold.ToString();
    }

    private void SetTextExperience(int experience)
    {
        if (_experiencePlayer)
            _experiencePlayer.text = experience.ToString();
    }

    #region Event handlers
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

        //SetTextWaveCounter(wave);
    }

    private void WaveIsOver_EventHandler()
    {
        StartCoroutine(SetTextPlayerNotificationWithDelay(PlayerNotification.WAVE_IS_OVER, 0));
    }

    private void PlayerDamaged_EventHandler(int damage)
    {
        if(_playerUnit)
        {
            SetTextHealthBar(_playerUnit.Health.Actual, _playerUnit.Health.Max);
            SetValueHealthBarSlider(_playerUnit.Health.Actual, _playerUnit.Health.Max);
        }
       
    }
    #endregion Event handlers
}
