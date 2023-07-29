using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

/// <summary>
/// ���������� �������� �� ������ �������� ��� �����������
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
        // ���� hp bar �����, �������� ������ �� ������������
        if (_isShow)
        {
            _timerDelayBeforeFade -= Time.deltaTime;

            // ���� ������ �����, ��������� �������� �������� ������������� hp bar-�
            if (_timerDelayBeforeFade <= 0)
                ShowHealthBarFadeAnimation();
        }
    }

    #endregion Mono

    #region Private methods

    /// <summary>
    /// �������� �������� ������� ��������� �����
    /// </summary>
    /// <param name="health">���������� �������� ��������</param>
    private void ShowDamageEffectAnimation(float health)
    {
        _hpDamageEffectSlider.DOKill();
        _hpDamageEffectSlider.DOValue(health, _durationEffect);
    }

    /// <summary>
    /// �������� �������� �������� ��������� ������ ��������
    /// </summary>
    private void ShowHealthBarAppearAnimation()
    {
        // ���� hp bar ��� �����, �� ������ ��������� ������ �� ������������
        if (_isShow)
        {
            _timerDelayBeforeFade = _shownDuration;
            return;
        }

        // ��������� �������� ��������� hp bar-�
        _canvasGroupHPBar.DOKill();
        _canvasGroupHPBar.DOFade(1f, _appearanceDuration);

        // ������������� ����� ������� �� ������� �������� ������������
        _timerDelayBeforeFade = _shownDuration + _appearanceDuration;

        _isShow = true;
    }

    /// <summary>
    /// �������� �������� �������� ������������ ������ ��������
    /// </summary>
    private void ShowHealthBarFadeAnimation()
    {
        // ��������� �������� ������������� hp bar-�
        _canvasGroupHPBar.DOKill();
        _canvasGroupHPBar.DOFade(0f, _fadeDuration);

        _isShow = false;
    }

    #endregion Private methods

    #region Public methods

    /// <summary>
    /// ������������� ��������� �������� ��� ������ ��������
    /// </summary>
    /// <param name="maxHealth">�������� ������������� ��������</param>
    /// <param name="health">������� �������� ��������</param>
    public void SetDefaultParameters(float maxHealth, float health)
    {
        _hpSlider.maxValue = maxHealth;
        _hpDamageEffectSlider.maxValue = maxHealth;

        _hpSlider.value = health;
        _hpDamageEffectSlider.value = health;
    }

    /// <summary>
    /// ������������� ������������ �������� ������ ��
    /// </summary>
    /// <param name="maxHealth">�������� ������������� ��������</param>
    public void SetMaxHealth(float maxHealth)
    {
        _hpSlider.maxValue = maxHealth;
        _hpDamageEffectSlider.maxValue = maxHealth;
    }

    /// <summary>
    /// ������������� ������� �������� �������� ��� ������ ��������
    /// </summary>
    /// <param name="health">��������� �������� ��������</param>
    /// <param name="onShowHpBar">�������� ������ �������� ��� ���������</param>
    /// <param name="onEffectDamage">�������� ������ ��������� ����� ��� ���������</param>
    public void SetHealth(float health, bool onShowHpBar = false, bool onEffectDamage = false)
    {
        _hpSlider.value = health;

        if (onShowHpBar)
            ShowHealthBarAppearAnimation();

        if (onEffectDamage)
            ShowDamageEffectAnimation(health);
    }

    /// <summary>
    /// ����� ��� ���������/����������� ������ ��������
    /// </summary>
    /// <param name="active"></param>
    public void SetActiveHealthBar(bool active)
    {
        gameObject.SetActive(active);
    }

    #endregion Public methods
}
