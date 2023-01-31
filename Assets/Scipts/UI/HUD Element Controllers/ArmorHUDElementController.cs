using TMPro;
using UnityEngine;

public class ArmorHUDElementController : MonoBehaviour
{
    [Header("Текст для вывода значения")]
    [SerializeField] private TextMeshProUGUI _valueText;

    private PlayerUnit _playerUnit;

    private void Awake()
    {
        PlayerEventManager.OnPlayerArmorChanged.AddListener(UpdatetValueText);
    }

    private void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        UpdatetValueText();
    }

    private void UpdatetValueText()
    {
        if (_playerUnit)
            SetValueText(_playerUnit.Armor.Actual);
    }

    private void SetValueText(int armor)
    {
        if (_valueText)
            _valueText.text = armor.ToString();
    }
}
