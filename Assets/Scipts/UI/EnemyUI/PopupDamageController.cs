using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс контроллер отвечает за вывод всплывающих цифр получаемого урона над противником
/// </summary>
public class PopupDamageController : MonoBehaviour
{
    #region Serialize fields
    /// <summary>
    /// Поле для префаба объекта с текстом, которое и будет текстом с получаемым уроном
    /// </summary>
    [SerializeField] private GameObject _prefabPopupDamageText;
    /// <summary>
    /// Скорость плавного показа/скрытия текста урона
    /// </summary>
    [SerializeField] [Range (0.1f, 10f)] private float _rateShowingPopupDamageText = 2.5f;
    [SerializeField][Range(0.1f, 10f)] private float _rateHidePopupDamageText = 2.5f;
    [SerializeField] [Range (0.0f, 10f)] private float _durationShowPopupDamageText = 1f;
    #endregion Serialize fields

    #region Public fields
    /// <summary>
    /// Цвет текста цифр урона для каждого из типов урона
    /// </summary>
    public Dictionary<TypeDamage, Color> TYPE_DAMAGE_COLOR = new Dictionary<TypeDamage, Color>
    {
        { TypeDamage.Physical, new Color(0.9f, 0.9f, 0.9f) },
        { TypeDamage.Fire, new Color(0.81f, 0.32f, 0.07f) },
        { TypeDamage.Bleeding, Color.red },
        { TypeDamage.Poison, Color.green }
    };
    #endregion Public fields

    #region Public methods
    /// <summary>
    /// Показывает всплывающий полученный урон над противником. Создает объект с текстом и задает параметры
    /// </summary>
    /// <param name="damage">Значение урона</param>
    /// <param name="typeDamage">Тип получаемого урона (Необходимо для определения цвета текста урона)</param>
    public void ShowPopupDamage(int damage, TypeDamage typeDamage)
    {
        GameObject popupDamageText = Instantiate(_prefabPopupDamageText);
        popupDamageText.transform.SetParent(gameObject.transform, false);

        PopupDamage popupDamage = popupDamageText.GetComponent<PopupDamage>();
        if (popupDamage != null)
            popupDamage.ShowPopupDamageText(damage, TYPE_DAMAGE_COLOR[typeDamage], _rateShowingPopupDamageText, _rateHidePopupDamageText, _durationShowPopupDamageText);
    }
    #endregion Public methods
}
