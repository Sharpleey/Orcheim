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
    public void OnClickLoad()
    {
        _mainMenuCanvasController?.ShowMenu(_mainMenuCanvasController.LoadMenu);
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
