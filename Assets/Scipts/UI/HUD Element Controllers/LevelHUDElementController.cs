using TMPro;
using UnityEngine;

public class LevelHUDElementController : MonoBehaviour
{
    [Header("Текст для вывода значения")]
    [SerializeField] private TextMeshProUGUI _levelValueText;

    private PlayerUnit _playerUnit;

    private void Awake()
    {
        PlayerEventManager.OnPlayerLevelUp.AddListener(UpdatetLevelValueText);
    }

    private void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        UpdatetLevelValueText();
    }

    private void UpdatetLevelValueText()
    {
        if (_playerUnit)
            SetLevelValueText(_playerUnit.Level);
    }

    private void SetLevelValueText(int level)
    {
        if (_levelValueText)
            _levelValueText.text = level.ToString();
    }
}
