using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Контроллер отвечает за полосу здоровья над противником
/// </summary>
public class HealthBarController : MonoBehaviour
{
    #region Serialize fields

    [SerializeField] private Slider _hpSlider;
    [SerializeField] private float _rateShowing = 1.5f;
    [SerializeField] private float _durationShown = 2f;

    [Space(5)]
    [SerializeField] private Slider _hpDamageEffectSlider;
    [SerializeField] private float _durationEffect = 0.2f;

    #endregion Serialize fields

    #region Private fields
    /// <summary>
    /// Состояние плавного появления
    /// </summary>
    private bool _isShown = false;
    /// <summary>
    /// Состояние, когда полоса здровья полностью показана
    /// </summary>
    private bool _isShowing = false;
    /// <summary>
    /// Состояние плавного сокрытия полосы хп
    /// </summary>
    private bool _isHide = false;

    private float _currentAlpha = 0f;

    private float _timer = 0;

    private Image[] _images;
    #endregion Private fields

    #region Mono
    private void Start()
    {
        _images = GetComponentsInChildren<Image>();

        SetAlphaHealthBar(_currentAlpha);
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        // Плавно показываем
        if (_isShowing)
        {
            SetAlphaHealthBar(_currentAlpha);

            if (_currentAlpha < 1.0f)
            {
                _currentAlpha += _rateShowing * Time.deltaTime;
            }
            else
            {
                _currentAlpha = 1.0f;
                _isShowing = false;
                _isShown = true;
            }
        }

        // Задержка до сокрытия
        if (_isShown)
        {
            if (_timer < _durationShown)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _timer = 0;
                _isShown = false;
                _isHide = true;
            }
        }

        // Плавно скрываем
        if (_isHide)
        {
            SetAlphaHealthBar(_currentAlpha);

            if (_currentAlpha > 0f)
            {
                _currentAlpha -= _rateShowing * Time.deltaTime;
            }
            else
            {
                _currentAlpha = 0f;
                _isHide = false;
            }

        }
    }

    /// <summary>
    /// Метод устанавливает прозрачность для всех элементов объекта HealthBar
    /// </summary>
    /// <param name="curAlpha">Значение alpha</param>
    private void SetAlphaHealthBar(float curAlpha)
    {
        foreach (Image image in _images)
        {
            image.canvasRenderer.SetAlpha(curAlpha);
        }
    }

    private void DamageEffectAnimation(float health)
    {
        _hpDamageEffectSlider.DOKill();
        _hpDamageEffectSlider.DOValue(health, _durationEffect);
    }


    #endregion Private methods

    #region Public methods

    /// <summary>
    /// Плавно показываем и плавно скрываем полосу хп
    /// </summary>
    public void ShowHealthBar()
    {
        if (!_isShowing && !_isShown)
        {
            _isShowing = true;
            _isHide = false;
        }
        if (_isShown)
        {
            _timer = 0;
        }
    }

    /// <summary>
    /// Устанавливаем начальные значения для полосы здоровья
    /// </summary>
    /// <param name="maxHealth">Значение максимального здоровья</param>
    /// <param name="health">Текущее значение здоровья</param>
    public void SetDefaultParameters(float maxHealth, float health)
    {
        _hpSlider.maxValue = maxHealth;
        _hpDamageEffectSlider.maxValue = maxHealth;

        _hpSlider.value = health;
        _hpDamageEffectSlider.value = health;
    }

    /// <summary>
    /// Устанавливаем максимальное значение полосы хп
    /// </summary>
    /// <param name="maxHealth">Значение максимального здоровья</param>
    public void SetMaxHealth(float maxHealth)
    {
        _hpSlider.maxValue = maxHealth;
        _hpDamageEffectSlider.maxValue = maxHealth;
    }

    /// <summary>
    /// Устанавливаем текущее значение здоровья для полосы хп
    /// </summary>
    /// <param name="health">Текущее значение здоровья</param>
    public void SetHealth(float health, bool onEffectDamage = false)
    {
        _hpSlider.value = health;

        if (onEffectDamage)
            DamageEffectAnimation(health);
    }

    /// <summary>
    /// Метод для активации/деактивации полосы здоровья
    /// </summary>
    /// <param name="active"></param>
    public void SetActiveHealthBar(bool active)
    {
        gameObject.SetActive(active);
    }

    #endregion Public methods
}
