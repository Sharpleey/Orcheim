
using UnityEngine;

public class PauseMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _learn;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _awardsMenu;

    private GameObject _activeMenu;
    private Canvas _canvas;

    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

    private bool _isPaused;

    private void Awake()
    {
        PlayerEventManager.OnPlayerDead.AddListener(PlayerDead_EventHandler);
        PlayerEventManager.OnPlayerLevelUp.AddListener(PlayerLevelUp_EventHandler);
    }

    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = false;

        AllMenuDisable();

        _isPaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            Pause();
        }
    }

    /// <summary>
    /// ћетод с помощью которого можно поставить игру на паузу и показать меню паузы
    /// </summary>
    public void Pause()
    {
        _isPaused = !_isPaused;

        GlobalGameEventManager.PauseGame(_isPaused);

        if (_isPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            _canvas.enabled = true;

            ShowMenu(_pauseMenu);

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _canvas.enabled = false;
    }

    private void ShowMenu(GameObject menu)
    {
        _activeMenu?.SetActive(false);
        _activeMenu = menu;
        _activeMenu?.SetActive(true);
    }

    private void AllMenuDisable()
    {
        _pauseMenu.SetActive(false);
        _learn.SetActive(false);
        _settings.SetActive(false);
        _gameOverMenu.SetActive(false);
    }

    public void OnClickButtonLearn()
    {
        ShowMenu(_learn);
    }

    public void OnClickButtonSettings()
    {
        ShowMenu(_settings);
    }

    public void OnClickButtonBack()
    {
        ShowMenu(_pauseMenu);
    }

    public void OnClickButtonRestart()
    {
        GlobalGameEventManager.GameOver();
        GlobalGameEventManager.NewGame(GameMode.Orccheim);

        if(GameSceneManager.Instance)
            GameSceneManager.Instance.RestartScene();
    }

    public void OnClickButtonExitMainMenu()
    {
        ///
        /// ѕроизводим сохранени€ данных перед выходом
        ///
        GlobalGameEventManager.GameOver();

        if (GameSceneManager.Instance)
            GameSceneManager.Instance.SwitchToScene(SceneName.MAIN_MENU);
    }

    #region Event handlers
    private void PlayerDead_EventHandler()
    {
        _isPaused = true;

        GlobalGameEventManager.PauseGame(_isPaused);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        _canvas.enabled = true;

        ShowMenu(_gameOverMenu);
    }

    private void PlayerLevelUp_EventHandler(int level)
    {
        _isPaused = true;

        GlobalGameEventManager.PauseGame(_isPaused);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        _canvas.enabled = true;

        ShowMenu(_awardsMenu);
    }

    #endregion
}
