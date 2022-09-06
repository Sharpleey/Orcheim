using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopupDamage : MonoBehaviour
{
    #region Private fields
    private bool _isShown = false;
    private bool _isShowing = false;
    private bool _isHide = false;

    private float _currentAlpha = 0f;
    private float _timer = 0f;

    private Vector3 targetPosition;
    private float offsetX, offsetY;

    private float _rateShowing = 2.5f;
    private float _rateHide = 2.5f;
    private float _durationShown = 1f;
    //private Color _colorText = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    [SerializeField] private TextMeshProUGUI _textMeshPro; // Если без SerializeField, выдает ошибку (хз с чем связано)
    #endregion Private fields

    #region Mono
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _textMeshPro.alpha = _currentAlpha;

        offsetX = UnityEngine.Random.Range(-1.5f, 1.5f);
        offsetY = UnityEngine.Random.Range(0.3f, 1.2f);
        targetPosition = new Vector3(transform.position.x + offsetX, transform.position.y + 0.5f, transform.position.z);
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (_isShowing)
        {
            // Смещение текста
            // ------------------------------------------------
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _rateShowing * Time.deltaTime);
            // ------------------------------------------------

            _textMeshPro.alpha = _currentAlpha;

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

        if (_isHide)
        {

            _textMeshPro.alpha = _currentAlpha;
            if (_currentAlpha > 0f)
            {
                _currentAlpha -= _rateHide * Time.deltaTime;
            }
            else
            {
                _currentAlpha = 0f;
                _isHide = false;

                Destroy(gameObject);
            }

        }
    }
    private void SetText(String stext)
    {
        _textMeshPro.text = "-" + stext;
    }
    private void SetColorText(Color colorText)
    {
        _textMeshPro.color = colorText;
    }
    #endregion Private methods

    #region Public methods
    /// <summary>
    /// Метод задает параметры отображения текста: значение урона, цвет урона, скорость появления/сокрытия урона, длительность показа
    /// </summary>
    /// <param name="damage">Значение урона</param>
    /// <param name="colorText">Цвет текста урона</param>
    /// <param name="rateShowing">Скорость появления/сокрытия урона</param>
    /// <param name="durationShow">Длительность показа текста с уроном</param>
    public void ShowPopupDamageText(int damage, Color colorText, float rateShowing, float rateHide, float durationShow)
    {
        SetText(damage.ToString());
        SetColorText(colorText);

        _rateShowing = rateShowing;
        _rateHide = rateHide;
        _durationShown = durationShow;

        _isShowing = true;
    }
    #endregion Public methods
}
