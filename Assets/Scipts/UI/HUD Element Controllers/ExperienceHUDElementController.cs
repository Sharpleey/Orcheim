using UnityEngine;
using TMPro;

/// <summary>
/// ������ HUD ��������: ���� ������
/// </summary>
public class ExperienceHUDElementController : MonoBehaviour
{
    [Header("����� ��� ������ ��������")]
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
