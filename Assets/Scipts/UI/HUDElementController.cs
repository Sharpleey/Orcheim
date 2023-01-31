using TMPro;
using UnityEngine;

/// <summary>
/// ����������� ����� ��� HUD ��������� ���������� ������
/// </summary>
public abstract class HUDElementController : MonoBehaviour
{
    [Header("����� ��� ������ ��������")]
    [SerializeField] private TextMeshProUGUI _valueText;

    /// <summary>
    /// ������ �� ������ ����� ������ �� �����
    /// </summary>
    protected PlayerUnit _playerUnit;

    protected virtual void Awake()
    {
        AddListeners();
    }

    protected virtual void Start()
    {
        _playerUnit = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerUnit>();

        SetValueText(string.Empty);

        UpdatetValueText();
    }

    /// <summary>
    /// ����� ����������� ������� �� �������, ������� ���������� �����������
    /// </summary>
    protected virtual void AddListeners()
    {

    }

    /// <summary>
    /// ����� ���������� �������� ��������
    /// </summary>
    protected virtual void UpdatetValueText()
    {

    }

    /// <summary>
    /// ����� ������ ����� ��� �������� HUD ����������
    /// </summary>
    /// <param name="valueString">����� �� ���������</param>
    protected void SetValueText(string valueString)
    {
        if (_valueText)
            _valueText.text = valueString;
    }
}
