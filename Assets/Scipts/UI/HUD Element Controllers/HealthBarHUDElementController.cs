using UnityEngine;
using UnityEngine.UI;

public class HealthBarHUDElementController : HUDElementController
{
    [SerializeField] private Slider _healthSlider;

    protected override void AddListeners()
    {
        PlayerEventManager.OnPlayerHealthChanged.AddListener(UpdatetValueText);
    }

    protected override void UpdatetValueText()
    {
        SetValueText($"{_playerUnit.Health.Actual} / {_playerUnit.Health.Max}");
        SetValueHealthBarSlider(_playerUnit.Health.Actual, _playerUnit.Health.Max);
    }

    private void SetValueHealthBarSlider(float currentHealth, float maxHealth)
    {
        if (_healthSlider)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = currentHealth;
        }
    }
}
