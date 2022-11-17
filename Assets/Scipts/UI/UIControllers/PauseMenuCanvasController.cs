using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _learn;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _gameOverMenu;

    private GameObject _activeMenu;
    private Canvas _canvas;

    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

    private bool _isPaused;

    private void Awake()
    {
        Messenger.AddListener(GlobalGameEvent.PLAYER_DEAD, PlayerDead_EventHandler);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GlobalGameEvent.PLAYER_DEAD, PlayerDead_EventHandler);
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
    private void Pause()
    {
        _isPaused = !_isPaused;

        Messenger<bool>.Broadcast(GameSceneManagerEvent.PAUSE_GAME, _isPaused);

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

    private void PlayerDead_EventHandler()
    {
        _isPaused = true;

        Messenger<bool>.Broadcast(GameSceneManagerEvent.PAUSE_GAME, _isPaused);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        _canvas.enabled = true;

        ShowMenu(_gameOverMenu);
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
        Messenger.Broadcast(GlobalGameEvent.NEW_GAME_MODE_ORCCHEIM);
        Messenger<string>.Broadcast(GameSceneManagerEvent.SWITCH_TO_SCENE, Scene.TEST_AI);
    }

    public void OnClickButtonExitMainMenu()
    {
        ///
        /// ѕроизводим сохранени€ данных перед выходом
        ///
        Messenger.Broadcast(GlobalGameEvent.GAME_OVER);
        Messenger<string>.Broadcast(GameSceneManagerEvent.SWITCH_TO_SCENE, Scene.MAIN_MENU);
    }
}
