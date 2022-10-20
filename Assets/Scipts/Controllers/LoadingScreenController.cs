using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс контроллер экрана загрузки между сценами. Отвечает за плавный переход между сценами
/// </summary>
[RequireComponent(typeof(Animator))]
public class LoadingScreenController : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Image _imageProgressBar;
    [SerializeField] private Animator _animator;
    #endregion SerializeFields

    #region Properties
    /// <summary>
    /// Изображение полоски прогресса
    /// </summary>
    public Image ProgressBar => _imageProgressBar;
    /// <summary>
    /// Проигрывается ли анимация показа загрузочного экрана или нет
    /// </summary>
    public bool IsShow => _isShow;
    #endregion Properties

    #region Private fields
    private bool _isShow;
    #endregion Private fields
    
    #region Mono
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // Отключаем аниматор
        _animator.enabled = false;
        _isShow = false;
    }
    private void Update()
    {
        // Заполянем полосу прогресса
        if (_animator.enabled && Managers.GameSceneManager.AsyncOperationLoadingScene != null)
            _imageProgressBar.fillAmount = Managers.GameSceneManager.AsyncOperationLoadingScene.progress;
    }
    #endregion Mono

    #region Private methods
    /// <summary>
    /// Метод события окончания анимации показа экрана загрузки
    /// </summary>
    private void OnAnimationShowOver()
    {
        _isShow = false;
    }
    /// <summary>
    /// Метод события при окончании анимации сокрытия экрана загрузки
    /// </summary>
    private void OnAnimationHideOver()
    {
        _animator.enabled = false;
    }
    #endregion Private methods

    #region Public methods
    /// <summary>
    /// Метод показа экрана загрузкы
    /// </summary>
    public void Show()
    {
        _imageProgressBar.fillAmount = 0;
        _isShow = true;
        _animator.enabled = true;
        _animator.SetTrigger("IsShow");
    }
    /// <summary>
    /// Метод скрытия экрана загрузки
    /// </summary>
    public void Hide()
    {
        _animator.SetTrigger("IsHide");
    }
    #endregion Public methods
}
