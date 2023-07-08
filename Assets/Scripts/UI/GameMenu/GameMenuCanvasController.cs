using UnityEngine;
using Zenject;

public class GameMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _learn;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _awardsMenu;

    [Space]
    [SerializeField] private KeyCode _showGameMenuKey = KeyCode.Escape;

    private GameObject _activeMenu;
    private Canvas _canvas;

    private GameSceneManager _sceneManager;

    private void Awake()
    {
        PlayerEventManager.OnPlayerDead.AddListener(EventHandlerPlayerDead);
        PlayerEventManager.OnPlayerLevelUp.AddListener(EventHandlerPlayerLevelUp);
    }

    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = false;

        AllMenuDisable();
    }

    [Inject]
    private void Construct(GameSceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(_showGameMenuKey))
        {
            if(!_canvas.enabled)
            {
                _sceneManager?.PauseGame(true);

                ShowCanvas(true);
                ShowMenu(_pauseMenu);

                return;
            }

            _sceneManager?.PauseGame(false);

            ShowCanvas(false);
        }
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
        _awardsMenu.SetActive(false);
    }

    public void ShowCanvas(bool isShowCanvas)
    {
        if (isShowCanvas)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            _canvas.enabled = true;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _canvas.enabled = false;
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

        _sceneManager?.RestartScene();
    }

    public void OnClickButtonExitMainMenu()
    {
        ///
        /// ���������� ���������� ������ ����� �������
        ///
        GlobalGameEventManager.GameOver();

        _sceneManager?.SwitchToScene(HashSceneNameString.MAIN_MENU);
    }

    #region Event handlers
    private void EventHandlerPlayerDead()
    {
        _sceneManager?.PauseGame(true);

        ShowCanvas(true);
        ShowMenu(_gameOverMenu);
    }

    private void EventHandlerPlayerLevelUp()
    {
        _sceneManager?.PauseGame(true);

        ShowCanvas(true);
        ShowMenu(_awardsMenu);
    }

    #endregion
}
