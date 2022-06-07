using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopupDamage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private bool _isShow = false;
    private bool _isHide = false;

    private float _currentAlpha = 0f;

    [SerializeField]
    private float _rateShowing = 2.5f;
 
    // Start is called before the first frame update
    void Start()
    {
        // _textMeshPro = GetComponent<TextMeshProUGUI>();
        _textMeshPro.alpha = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if(_isShow)
        {

            _textMeshPro.alpha = _currentAlpha;
            if(_currentAlpha < 1.0f)
            {
                _currentAlpha += _rateShowing * Time.deltaTime;
            }
            else
            {
                _currentAlpha = 1.0f;
                _isShow = false;
                _isHide = true;
            }
        }

        if(_isHide)
        {

            _textMeshPro.alpha = _currentAlpha;
            if(_currentAlpha > 0f)
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

}
