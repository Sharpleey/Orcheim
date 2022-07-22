using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]

public class TakenDamageTextController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    #endregion SerializeField

    #region Private fields
    private bool _isShow = false;
    private bool _isHide = false;

    private float _currentAlpha = 0f;

    private Vector3 targetPosition;

    private float offsetX, offsetY;
    #endregion Private fields

    #region Public fields
    public float rateShowing = 2.5f;

    [HideInInspector] 
    public Color colorText = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    #endregion Public fields

    #region Mono
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _textMeshPro.color = colorText;
        _textMeshPro.alpha = 0f;

        offsetX = UnityEngine.Random.Range(-1.5f, 1.5f);
        offsetY = UnityEngine.Random.Range(0.3f, 1.2f);
        targetPosition = new Vector3(transform.position.x + offsetX, transform.position.y + 0.5f, transform.position.z);
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (_isShow)
        {
            var step = rateShowing * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            _textMeshPro.alpha = _currentAlpha;
            if (_currentAlpha < 1.0f)
            {
                _currentAlpha += rateShowing * Time.deltaTime;
            }
            else
            {
                _currentAlpha = 1.0f;
                _isShow = false;
                _isHide = true;
            }
        }

        if (_isHide)
        {

            _textMeshPro.alpha = _currentAlpha;
            if (_currentAlpha > 0f)
            {
                _currentAlpha -= rateShowing * Time.deltaTime;
            }
            else
            {
                _currentAlpha = 0f;
                _isHide = false;
            }

        }
    }
    #endregion Private methods

    #region Public methods
    public void ShowAndHide()
    {
        _textMeshPro.alpha = 0f;
        _isShow = true;
        _isHide = false;
    }

    public void SetText(String stext)
    {
        _textMeshPro.text = "-" + stext;
    }
    #endregion Public methods
}
