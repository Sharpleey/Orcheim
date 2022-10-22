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

    // Start is called before the first frame update
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Managers.GameSceneManager.IsGamePaused)
            {
                Managers.GameSceneManager.PauseGame();

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                _canvas.enabled = true;

                ShowMenu(_pauseMenu);
            }
            else
            {
                Managers.GameSceneManager.ResumeGame();

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                _canvas.enabled = false;
            }
        }
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
        Managers.GameSceneManager.SwitchToScene(Scenes.MAIN_MENU);
    }

   
}
