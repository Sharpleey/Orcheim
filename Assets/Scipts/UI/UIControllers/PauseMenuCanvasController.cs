using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _learn;
    [SerializeField] private GameObject _settings;

    private GameObject _activeMenu;
    private Canvas _canvas;

    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

    private bool _isPaused;

    // Start is called before the first frame update
    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = false;

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

    public void OnClickButtonExitMainMenu()
    {
        ///
        /// ѕроизводим сохранени€ данных перед выходом
        ///
        Messenger.Broadcast(GlobalGameEvent.GAME_OVER);
        Messenger<string>.Broadcast(GameSceneManagerEvent.SWITCH_TO_SCENE, Scenes.MAIN_MENU);
    }
}
