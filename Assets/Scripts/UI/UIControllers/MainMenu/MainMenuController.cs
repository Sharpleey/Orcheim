using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private MainMenuCanvasController _mainMenuCanvasController;

    private void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }

    public void OnClickNewGame()
    {
        _mainMenuCanvasController?.ShowMenu(_mainMenuCanvasController.NewGameMenu);
    }
    public void OnClickLearn()
    {
        _mainMenuCanvasController?.ShowMenu(_mainMenuCanvasController.LearnMenu);
    }
    public void OnClickScenes()
    {
        _mainMenuCanvasController?.ShowMenu(_mainMenuCanvasController.ScenesMenu);
    }
    public void OnClickSettings()
    {
        _mainMenuCanvasController?.ShowMenu(_mainMenuCanvasController.SettingsMenu);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
