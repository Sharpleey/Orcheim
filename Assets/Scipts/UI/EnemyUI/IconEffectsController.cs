using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  онтроллер отвечает за отображени€ иконок (статусов) над противником
/// </summary>
public class IconEffectsController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private GameObject _flameIcon;
    [SerializeField] private GameObject _electroIcon;
    [SerializeField] private GameObject _slowdownIcon;

    /// <summary>
    /// ѕромежуток по ’ между иконками
    /// </summary>
    [SerializeField] [Range(0.5f, 5f)] private float _offsetX = 3f;
    #endregion Serialize fields

    #region Private fields
    /// <summary>
    /// —писок активных иконок, необходим дл€ правильного расположени€ иконок над противником
    /// </summary>
    private List<GameObject> _activeIcons;
    #endregion Private fields

    #region Mono
    void Start()
    {
        _flameIcon.SetActive(false);
        _electroIcon.SetActive(false);
        _slowdownIcon.SetActive(false);

        _activeIcons = new List<GameObject>();
    }
    #endregion Mono

    #region Private methods
    /// <summary>
    /// ћетод с учетом активных иконок перерасчитывает расположени€ каждой из иконок так, чтобы 
    /// в любом случае и при любом колличестве они раполагались по центру противника
    /// </summary>
    private void RecalculationLocationIcon()
    {
        float leftBound = (_offsetX * (_activeIcons.Count - 1)) / -2f;

        foreach (var icon in _activeIcons)
        {
            Vector3 newPosition = new Vector3(leftBound, 0, 0);
            icon.transform.localPosition = newPosition;

            leftBound += _offsetX;
        }
    }
    #endregion Private methods

    #region Public methods
    /// <summary>
    /// ћетод активирует/деактивирует иконку горени€
    /// </summary>
    /// <param name="active">активировать (true) / деактивировать (false)</param>
    public void SetActiveIconBurning(bool active)
    {
        _flameIcon.SetActive(active);

        if (active)
            _activeIcons.Add(_flameIcon);
        else
            _activeIcons.Remove(_flameIcon);

        RecalculationLocationIcon();
    }
    /// <summary>
    /// ћетод активирует/деактивирует иконку электро
    /// </summary>
    /// <param name="active">активировать (true) / деактивировать (false)</param>
    public void SetActiveIconElectric(bool active)
    {
        _electroIcon.SetActive(active);
        if (active)
            _activeIcons.Add(_electroIcon);
        else
            _activeIcons.Remove(_electroIcon);

        RecalculationLocationIcon();
    }
    /// <summary>
    /// ћетод активирует/деактивирует иконку замедлени€
    /// </summary>
    /// <param name="active">активировать (true) / деактивировать (false)</param>
    public void SetActiveIconSlowdown(bool active)
    {
        _slowdownIcon.SetActive(active);

        if (active)
            _activeIcons.Add(_slowdownIcon);
        else
            _activeIcons.Remove(_slowdownIcon);

        RecalculationLocationIcon();
    }
    /// <summary>
    /// ƒиактивируем все иконки
    /// </summary>
    public void DeactivateAllIcons()
    {
        foreach (var icon in _activeIcons)
            icon.SetActive(false);

        _activeIcons.Clear();
    }
    #endregion Public methods
}
