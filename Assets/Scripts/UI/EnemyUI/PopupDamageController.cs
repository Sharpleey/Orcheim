using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Класс контроллер отвечает за вывод всплывающих цифр получаемого урона над противником
/// </summary>
public class PopupDamageController : MonoBehaviour
{
    #region Public fields

    /// <summary>
    /// Цвет текста цифр урона для каждого из типов урона
    /// </summary>
    public Dictionary<DamageType, Color> TYPE_DAMAGE_COLOR = new Dictionary<DamageType, Color>
    {
        { DamageType.Physical, new Color(0.9f, 0.9f, 0.9f) },
        { DamageType.Fire, new Color(0.81f, 0.32f, 0.07f) },
        { DamageType.Bleeding, Color.red },
        { DamageType.Poison, Color.green }
    };

    #endregion Public fields

    #region Private fields
    private Pool<PopupDamage> _popupDamagePool;
    #endregion

    #region Mono
    [Inject]
    private void Construct(Pool<PopupDamage> pool)
    {
        _popupDamagePool = pool;
    }
    #endregion

    #region Public methods

    /// <summary>
    /// Показывает всплывающий полученный урон над противником. Создает объект с текстом и задает параметры
    /// </summary>
    /// <param name="damage">Значение урона</param>
    /// <param name="isCriticalHit">Критическое попадание или нет</param>
    /// <param name="typeDamage">Тип получаемого урона (Необходимо для определения цвета текста урона)</param>
    public void ShowPopupDamage(float damage, bool isCriticalHit, DamageType typeDamage)
    {
        PopupDamage popupDamage = _popupDamagePool?.GetFreeElement();

        popupDamage?.transform.SetParent(transform, false);

        popupDamage?.SetDataAndStartAnimation(Mathf.Round(damage), isCriticalHit, TYPE_DAMAGE_COLOR[typeDamage]);
    }

    #endregion Public methods
}
