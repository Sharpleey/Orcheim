using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����� ���������� ������ �������� ����� �������. �������� �� ������� ������� ����� �������
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
    /// ����������� ������� ���������
    /// </summary>
    public Image ProgressBar => _imageProgressBar;
    /// <summary>
    /// ������������� �� �������� ������ ������������ ������ ��� ���
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
        // ��������� ��������
        _animator.enabled = false;
        _isShow = false;
    }
    private void Update()
    {
        // ��������� ������ ���������
        if (_animator.enabled && Managers.GameSceneManager.AsyncOperationLoadingScene != null)
            _imageProgressBar.fillAmount = Managers.GameSceneManager.AsyncOperationLoadingScene.progress;
    }
    #endregion Mono

    #region Private methods
    /// <summary>
    /// ����� ������� ��������� �������� ������ ������ ��������
    /// </summary>
    private void OnAnimationShowOver()
    {
        _isShow = false;
    }
    /// <summary>
    /// ����� ������� ��� ��������� �������� �������� ������ ��������
    /// </summary>
    private void OnAnimationHideOver()
    {
        _animator.enabled = false;
    }
    #endregion Private methods

    #region Public methods
    /// <summary>
    /// ����� ������ ������ ��������
    /// </summary>
    public void Show()
    {
        _imageProgressBar.fillAmount = 0;
        _isShow = true;
        _animator.enabled = true;
        _animator.SetTrigger("IsShow");
    }
    /// <summary>
    /// ����� ������� ������ ��������
    /// </summary>
    public void Hide()
    {
        _animator.SetTrigger("IsHide");
    }
    #endregion Public methods
}
