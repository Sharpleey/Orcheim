using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Скрипт HUD элемента, отображающего используемое оружие и способности игрока
/// </summary>
public class WeaponAndAbilityHUDElementController : MonoBehaviour
{
    [SerializeField] GameObject _ability1;
    [SerializeField] GameObject _ability2;
    [SerializeField] GameObject _ability3;
    [Space(10)]
    [SerializeField] GameObject _meleeWeapon;
    [SerializeField] GameObject _rangeWeapon;

    private Vector2 _sizeSelectedWeapon, _sizeDeselectedWeapon;
    private Vector3 _positionSelectedWeapon, _positionDeselectedWeapon;
    private Color _colorSelectedWeapon, _colorDeselectedWeapon;
    private RectTransform _rectTransformMeleeWeapon, _rectTransformRangeWeapon;
    private Image _imageMeleeWeapon, _imageRangeWeapon;

    private void Awake()
    {
        AddListeners();
    }

    private void Start()
    {
        _positionSelectedWeapon = _rangeWeapon.transform.position;
        _positionDeselectedWeapon = _meleeWeapon.transform.position;

        _rectTransformMeleeWeapon = _meleeWeapon.GetComponent<RectTransform>();
        _rectTransformRangeWeapon = _rangeWeapon.GetComponent<RectTransform>();

        _imageMeleeWeapon = _meleeWeapon.GetComponent<Image>();
        _imageRangeWeapon = _rangeWeapon.GetComponent<Image>();

        _colorSelectedWeapon = _imageRangeWeapon.color;
        _colorDeselectedWeapon = _imageMeleeWeapon.color;

        _sizeSelectedWeapon = _rectTransformRangeWeapon.sizeDelta;
        _sizeDeselectedWeapon = _rectTransformMeleeWeapon.sizeDelta;
    }

    private void AddListeners()
    {
        PlayerEventManager.OnPlayerChooseMeleeWeapon.AddListener(EventHandler_PlayerChooseMeleeWeapon);
        PlayerEventManager.OnPlayerChooseRangeWeapon.AddListener(EventHandler_PlayerChooseRangeWeapon);
    }

    private void SelectWeapon(GameObject weapon, RectTransform rectTransform, Image imageWeapon)
    {
        rectTransform.sizeDelta = _sizeSelectedWeapon;
        weapon.transform.position = _positionSelectedWeapon;
        imageWeapon.color = _colorSelectedWeapon;
    }

    private void DeselectWeapon(GameObject weapon, RectTransform rectTransform, Image imageWeapon)
    {
        rectTransform.sizeDelta = _sizeDeselectedWeapon;
        weapon.transform.position = _positionDeselectedWeapon;
        imageWeapon.color = _colorDeselectedWeapon;
    }

    private void EventHandler_PlayerChooseMeleeWeapon()
    {
        SelectWeapon(_meleeWeapon, _rectTransformMeleeWeapon, _imageMeleeWeapon);
        DeselectWeapon(_rangeWeapon, _rectTransformRangeWeapon, _imageRangeWeapon);
    }

    private void EventHandler_PlayerChooseRangeWeapon()
    {
        SelectWeapon(_rangeWeapon, _rectTransformRangeWeapon, _imageRangeWeapon);
        DeselectWeapon(_meleeWeapon, _rectTransformMeleeWeapon, _imageMeleeWeapon);
    }
}
