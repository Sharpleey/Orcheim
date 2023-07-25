using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class PopupDamage : MonoBehaviour
{
    [SerializeField] private Image _criticalHitIcon;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    #region Private fields
    private bool _isShown = false;
    private bool _isShowing = false;
    private bool _isHide = false;

    private float _currentAlpha = 0f;
    private float _timer = 0f;

    private Vector3 targetPosition;

    private float _rateShowing = 2.5f;
    private float _rateHide = 2.5f;
    private float _durationShown = 1f;

    private Pool<PopupDamage> _popupDamagePool;
    #endregion Private fields

    #region Mono

    [Inject]
    private void Construct(Pool<PopupDamage> pool)
    {
        _popupDamagePool = pool;
    }

    private void OnEnable()
    {
        transform.localPosition = new Vector3(0, 25f, 0);
        transform.eulerAngles = new Vector3(0, 180, 0);
        transform.localScale = Vector3.one;


        targetPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(35f, 45f), 0);

        _textMeshPro.alpha = 0;
        _textMeshPro.fontSize = 30;

        _criticalHitIcon.enabled = false;
    }

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

                _popupDamagePool?.ReturnToContainerPool(this);
            }

        }
    }
    #endregion Mono

    #region Public methods

    /// <summary>
    /// Метод задает параметры отображения текста: значение урона, цвет урона, скорость появления/сокрытия урона, длительность показа
    /// </summary>
    /// <param name="damage">Значение урона</param>
    /// <param name="colorText">Цвет текста урона</param>
    /// <param name="rateShowing">Скорость появления/сокрытия урона</param>
    /// <param name="durationShow">Длительность показа текста с уроном</param>
    public void StartShowing(float damage, bool isCriticalHit, Color colorText, float rateShowing, float rateHide, float durationShow)
    {
        _textMeshPro.text = $"-{damage}";
        _textMeshPro.color = colorText;

        _rateShowing = rateShowing;
        _rateHide = rateHide;
        _durationShown = durationShow;

        if (isCriticalHit)
        {
            _criticalHitIcon.enabled = true;
            _textMeshPro.fontSize = 42;
        }

        _isShowing = true;
    }
    #endregion Public methods
}
