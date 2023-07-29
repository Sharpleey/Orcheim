using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

/// <summary>
/// Контроллер отвечает за полосу здоровья над противником
/// </summary>
public class HealthBarController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private CanvasGroup _canvasGroupHPBar;
    [SerializeField, Min(0)] private float _appearanceDuration = 0.4f;
    [SerializeField, Min(0)] private float _shownDuration = 1f;
    [SerializeField, Min(0)] private float _fadeDuration = 0.4f;

    [Space(5)]
    [SerializeField] private Slider _hpSlider;

    [Space(5)]
    [SerializeField] private Slider _hpDamageEffectSlider;
    [SerializeField] private float _durationEffect = 0.2f;
    #endregion

    #region Private fields
    private bool _isShow = false;
    private float _timerDelayBeforeFade;
    #endregion

    #region Mono
    private void Start()
    {
        _canvasGroupHPBar.alpha = 0;
    }

    private void Update()
    {
        // Если hp bar виден, включаем таймер до исчезновения
        if (_isShow)
        {
            _timerDelayBeforeFade -= Time.deltaTime;

            // Если таймер истек, запускаем анимацию плавного исчезноваения hp bar-а
            if (_timerDelayBeforeFade <= 0)
                ShowHealthBarFadeAnimation();
        }
    }

    #endregion Mono

    #region Private methods

    /// <summary>
    /// Показать анимацию эффекта получения урона
    /// </summary>
    /// <param name="health">Актуальное значение здоровья</param>
    private void ShowDamageEffectAnimation(float health)
    {
        _hpDamageEffectSlider.DOKill();
        _hpDamageEffectSlider.DOValue(health, _durationEffect);
    }

    /// <summary>
    /// Показать анимацию плавного появления полосы здоровья
    /// </summary>
    private void ShowHealthBarAppearAnimation()
    {
        // Если hp bar уже виден, то просто обновляем таймер до исчезновения
        if (_isShow)
        {
            _timerDelayBeforeFade = _shownDuration;
            return;
        }

        // Запускаем анимацию появления hp bar-а
        _canvasGroupHPBar.DOKill();
        _canvasGroupHPBar.DOFade(1f, _appearanceDuration);

        // Устанавливаем время таймера до запуска анимации исчезновения
        _timerDelayBeforeFade = _shownDuration + _appearanceDuration;

        _isShow = true;
    }

    /// <summary>
    /// Показать анимацию плавного исчезновения полосы здоровья
    /// </summary>
    private void ShowHealthBarFadeAnimation()
    {
        // Запускаем анимацию исчезноваения hp bar-а
        _canvasGroupHPBar.DOKill();
        _canvasGroupHPBar.DOFade(0f, _fadeDuration);

        _isShow = false;
    }

    #endregion Private methods

    #region Public methods

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
    /// Устанавливаем текущее значение здоровья для полосы здоровья
    /// </summary>
    /// <param name="health">Актульное значение здоровья</param>
    /// <param name="onShowHpBar">Показать полосу здоровья при изменении</param>
    /// <param name="onEffectDamage">Показать эффект получения урона при изменении</param>
    public void SetHealth(float health, bool onShowHpBar = false, bool onEffectDamage = false)
    {
        _hpSlider.value = health;

        if (onShowHpBar)
            ShowHealthBarAppearAnimation();

        if (onEffectDamage)
            ShowDamageEffectAnimation(health);
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
