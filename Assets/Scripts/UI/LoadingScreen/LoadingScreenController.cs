using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Класс контроллер экрана загрузки между сценами. Отвечает за плавный переход между сценами
/// </summary>
public class LoadingScreenController : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private GameObject _canvas;

    [Space(10)]
    [SerializeField] private CanvasGroup _canvasGroupLoading;
    [SerializeField] private CanvasGroup _canvasGameName;
    [SerializeField] private Image _imageBackground;
    [SerializeField] private Image _imageProgressBar;
    [SerializeField] private Transform _transformLoadingIcon;

    [Space(10)]
    [Header("Время плавного показа экрана загрузки")]
    [SerializeField] private float _showTime = 1.5f;
    [Header("Время плавного скрытия экрана загрузки")]
    [SerializeField] private float _hideTime = 1.5f;
    #endregion

    #region Properties
    public bool IsShowing => _isShowing;
    public bool IsHiding => _isHiding;
    #endregion

    #region Private fields
    private bool _isShowing;
    private bool _isHiding;

    private Sequence _sequence;
    private Tween _tweenLoadingIcon;
    #endregion

    #region Mono
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _isShowing = false;
        _canvas.SetActive(false);
        _imageBackground.DOFade(0, 0);
        _canvasGroupLoading.alpha = 0;
        _canvasGameName.alpha = 0;
    }
    #endregion Mono

    #region Public methods
    /// <summary>
    /// Метод показа экрана загрузкы
    /// </summary>
    public void Show()
    {
        _canvas.SetActive(true);

        _isShowing = true;

        _imageProgressBar.fillAmount = 0;

        _sequence = DOTween.Sequence().SetEase(Ease.Linear).SetUpdate(true);
        _sequence.Append(_imageBackground.DOFade(1f, _showTime / 3))
            .Append(_canvasGameName.DOFade(1f, _showTime / 3))
            .Append(_canvasGroupLoading.DOFade(1f, _showTime / 3))
            .AppendCallback(() =>
            {
                _isShowing = false;
            });

        _tweenLoadingIcon = _transformLoadingIcon.DOLocalRotate(new Vector3(0, 0, -360), 0.5f, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear).SetUpdate(true);
    }

    /// <summary>
    /// Метод скрытия экрана загрузки
    /// </summary>
    public void Hide()
    {
        _isHiding = true;

        _sequence = DOTween.Sequence().SetEase(Ease.Linear).SetUpdate(true);
        _sequence.Append(_canvasGroupLoading.DOFade(0f, _hideTime / 3))
            .Append(_canvasGameName.DOFade(0f, _hideTime / 3))
            .Append(_imageBackground.DOFade(0f, _hideTime / 3))
            .AppendCallback(() =>
            {
                _sequence.Kill();
                _tweenLoadingIcon.Kill();

                _isHiding = false;

                _canvas.SetActive(false);
            });
    }

    public void SetValueProgressBar(float progress)
    {
        _imageProgressBar.fillAmount = progress;
    }
    #endregion Public methods
}
