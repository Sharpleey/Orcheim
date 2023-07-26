using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class PopupDamage : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroupPopupDamage;
    [SerializeField] private Image _criticalHitIcon;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    #region Private fields
    private float _defaultFontSize;

    private Vector3 targetPosition;

    private Pool<PopupDamage> _popupDamagePool;

    private Sequence _sequenceShowing;
    private Tween _tweenScale;
    private Tween _tweenMove;
    #endregion 

    #region Mono

    [Inject]
    private void Construct(Pool<PopupDamage> pool)
    {
        _popupDamagePool = pool;
    }

    private void Awake()
    {
        _defaultFontSize = _textMeshPro.fontSize;
    }

    private void OnEnable()
    {
        SetDefaultParameters();
    }

    #endregion Mono

    #region Private methods
    private void SetDefaultParameters()
    {
        transform.localPosition = new Vector3(0, 25f, 0);
        transform.eulerAngles = new Vector3(0, 180, 0);
        transform.localScale = Vector3.zero;

        targetPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(35f, 45f), 0);

        _canvasGroupPopupDamage.alpha = 0;
        _textMeshPro.fontSize = _defaultFontSize;

        _criticalHitIcon.enabled = false;
    }

    #endregion

    #region Public methods

    /// <summary>
    /// ”станавливает данные и параметры дл€ текста и запускает анимацию всплывающего урона
    /// </summary>
    /// <param name="damage">«начение урона</param>
    /// <param name="isCriticalHit">явл€етс€ ли урон критическим</param>
    /// <param name="colorText">÷вет текста урона</param>
    public void SetDataAndStartAnimation(float damage, bool isCriticalHit, Color colorText)
    {
        SetData(damage, isCriticalHit, colorText);
        StartAnimation();
    }

    /// <summary>
    /// ћетод задает параметры отображени€ текста: значение урона, цвет урона
    /// </summary>
    /// <param name="damage">«начение урона</param>
    /// <param name="isCriticalHit">явл€етс€ ли урон критическим</param>
    /// <param name="colorText">÷вет текста урона</param>
    public void SetData(float damage, bool isCriticalHit, Color colorText)
    {
        _textMeshPro.text = $"-{damage}";
        _textMeshPro.color = colorText;

        if (isCriticalHit)
        {
            _criticalHitIcon.enabled = true;
            _textMeshPro.fontSize = 42;
        }
    }

    /// <summary>
    /// ћетод запускает анимацию всплывающего урона
    /// </summary>
    public void StartAnimation()
    {
        _tweenMove = transform.DOLocalMove(targetPosition, 0.15f).SetEase(Ease.InOutQuad);
        _tweenScale = transform.DOScale(1f, 0.15f).SetEase(Ease.OutElastic);

        _sequenceShowing = DOTween.Sequence().SetEase(Ease.Linear);
        _sequenceShowing.Append(_canvasGroupPopupDamage.DOFade(1f, 0.15f))
            .AppendInterval(0.4f)
            .Append(_canvasGroupPopupDamage.DOFade(0f, 0.25f))
            .AppendCallback(() =>
            {
                _popupDamagePool?.ReturnToContainerPool(this);
            });
    }
    #endregion Public methods
}
