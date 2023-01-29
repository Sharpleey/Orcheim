using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHUDElementController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthValue;
    [SerializeField] private Slider _healthSlider;

    private PlayerUnit _playerUnit;

    private void Awake()
    {
        //PlayerEventManager.OnPlayerDamaged.AddListener(PlayerDamaged_EventHandler);
        PlayerEventManager.OnPlayerHealthChanged.AddListener(UpdateValueHealthBar);

    }

    private void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        if(_healthValue)
            _healthValue.text = "";

        if (_playerUnit)
            UpdateValueHealthBar();
    }

    private void UpdateValueHealthBar()
    {
        SetTextValueHealthBar(_playerUnit.Health.Actual, _playerUnit.Health.Max);
        SetValueHealthBarSlider(_playerUnit.Health.Actual, _playerUnit.Health.Max);
    }

    private void SetTextValueHealthBar(int currentHealth, int maxHealth)
    {
        if (_healthValue)
            _healthValue.text = currentHealth.ToString() + "/" + maxHealth.ToString();

    }

    private void SetValueHealthBarSlider(int currentHealth, int maxHealth)
    {
        if (_healthSlider)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = currentHealth;
        }
    }
}
