using TMPro;
using UnityEngine;

public class GoldHUDElementController : MonoBehaviour
{
    [Header("Текст для вывода значения")]
    [SerializeField] private TextMeshProUGUI _valueText;

    private PlayerUnit _playerUnit;

    private void Awake()
    {
        PlayerEventManager.OnPlayerGoldChanged.AddListener(UpdatetValueText);
    }

    private void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        UpdatetValueText();
    }

    private void UpdatetValueText()
    {
        if (_playerUnit)
            SetValueText(_playerUnit.Gold);
    }

    private void SetValueText(int gold)
    {
        if (_valueText)
            _valueText.text = gold.ToString();
    }
}
