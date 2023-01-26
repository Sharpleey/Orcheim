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

    private PlayerUnit _playerUnit;

    private void Awake()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(StartingNewGameModeOrccheim_EventHandler);
        PlayerEventManager.OnPlayerDamaged.AddListener(PlayerDamaged_EventHandler);
        WaveEventManager.OnPreparingForWave.AddListener(PreparingForWave_EventHandler);
        WaveEventManager.OnWaveIsComing.AddListener(WaveIsComing_EventHandler);
        WaveEventManager.OnWaveIsOver.AddListener(WaveIsOver_EventHandler);
        SpawnEnemyEventManager.OnEnemiesRemaining.AddListener(SetTextEnemiesRemaining);
    }

    private void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        ClearAllText();

        if (_playerUnit)
        {
            SetTextHealthPlayer(_playerUnit.Health.Actual, _playerUnit.Health.Max);
            SetValueHealthBar(_playerUnit.Health.Actual, _playerUnit.Health.Max);
        }
    }

    private void ClearAllText()
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
    /// ����� ���������� � ��������� �� ����� ������
    /// </summary>
    /// <param name="text">����� ����������</param>
    /// <param name="delay">�������� � ��������</param>
    /// <returns></returns>
    private IEnumerator SetTextPlayerNotificationWithDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_playerNotification)
        {
            _playerNotification.text = text;
        }
    }

    /// <summary>
    /// ����� ������������ ����� ��� �������� ����
    /// </summary>
    /// <param name="wave">����� �����</param>
    private void SetTextWaveCounter(int wave)
    {
        if (_waveCounter)
            _waveCounter.text = PlayerNotification.WAVE_COUNTER + wave.ToString();
    }

    /// <summary>
    /// ����� ������������� ����� ��� �������� ���������� ������ �� �����
    /// </summary>
    /// <param name="enemiesRemaining">���-�� ���������� ������</param>
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

        SetTextWaveCounter(wave);
    }

    private void WaveIsOver_EventHandler()
    {
        StartCoroutine(SetTextPlayerNotificationWithDelay(PlayerNotification.WAVE_IS_OVER, 0));
    }

    private void PlayerDamaged_EventHandler(int damage)
    {
        if(_playerUnit)
        {
            SetTextHealthPlayer(_playerUnit.Health.Actual, _playerUnit.Health.Max);
            SetValueHealthBar(_playerUnit.Health.Actual, _playerUnit.Health.Max);
        }
       
    }
    #endregion Event handlers
}
