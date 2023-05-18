using TMPro;
using UnityEngine;
using Zenject;

/// <summary>
/// Абстрактный класс для HUD элементов интерфейса игрока
/// </summary>
public abstract class HUDElementController : MonoBehaviour
{
    [Header("Текст для вывода значения")]
    [SerializeField] private TextMeshProUGUI _valueText;

    /// <summary>
    /// Ссылка на объект юнита игрока на сцене
    /// </summary>
    protected PlayerUnit _playerUnit;

    protected virtual void Awake()
    {
        AddListeners();
    }

    protected virtual void Start()
    {

    }

    [Inject]
    private void Construct(PlayerUnit playerUnit)
    {
        _playerUnit = playerUnit;

        SetValueText(string.Empty);

        UpdatetValueText();
    }

    /// <summary>
    /// Метод подписывает элемент на события, которые необходимо отлавливать
    /// </summary>
    protected virtual void AddListeners()
    {

    }

    /// <summary>
    /// Метод обновления значения элемента
    /// </summary>
    protected virtual void UpdatetValueText()
    {

    }

    /// <summary>
    /// Метод задает текст для элемента HUD интерфейса
    /// </summary>
    /// <param name="valueString">Текст со значением</param>
    protected void SetValueText(string valueString)
    {
        if (_valueText)
            _valueText.text = valueString;
    }
}
