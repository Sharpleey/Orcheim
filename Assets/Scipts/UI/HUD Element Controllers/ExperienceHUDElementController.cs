using UnityEngine;
using TMPro;

/// <summary>
/// Скрипт HUD элемента: Опыт игрока
/// </summary>
public class ExperienceHUDElementController : MonoBehaviour
{
    [Header("Текст для вывода значения")]
    [SerializeField] private TextMeshProUGUI _expValueText;

    private PlayerUnit _playerUnit;

    private void Awake()
    {
        PlayerEventManager.OnPlayerExperienceChanged.AddListener(UpdatetExpValueText);
    }

    private void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        UpdatetExpValueText();
    }

    private void UpdatetExpValueText()
    {
        if(_playerUnit)
            SetExpValueText(_playerUnit.Experience, _playerUnit.ExperienceForNextLevel);
    }

    private void SetExpValueText(int actualExp, int expForNextLevel)
    {
        if (_expValueText)
            _expValueText.text = $"{actualExp} / {expForNextLevel}";
    }
}
