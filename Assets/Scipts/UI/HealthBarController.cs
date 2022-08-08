using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private float _rateShowing = 1.5f;
    [SerializeField] private float _durationShown = 2f;
    #endregion Serialize fields

    #region Private fields
    private bool _isShown = false;
    private bool _isShowing = false;
    private bool _isHide = false;

    private float _currentAlpha = 0f;

    private float _timer = 0;

    private Image[] _images;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        
    }
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

    private void SetAlphaHealthBar(float curAlpha)
    {
        foreach (Image image in _images)
        {
            image.canvasRenderer.SetAlpha(curAlpha);
        }
    }

    #endregion Private methods

    #region Public methods
    public void ShowHealthBar()
    {
        if(!_isShowing && !_isShown)
        {
            _isShowing = true;
            _isHide = false;
        }
        if (_isShown)
        {
            _timer = 0;
        }
    }
    public void SetMaxHealth(int maxHealth)
    {
        _hpSlider.maxValue = maxHealth;
    }
    public void SetHealth(int health)
    {
        _hpSlider.value = health;

        if (_hpSlider.value <= 0)
            gameObject.SetActive(false);
    }
    #endregion Public methods
}
