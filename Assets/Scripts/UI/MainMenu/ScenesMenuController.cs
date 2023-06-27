using UnityEngine;

public class ScenesMenuController : MonoBehaviour
{
    private MainMenuCanvasController _mainMenuCanvasController;

    void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }

    public void OnClickBack()
    {
        _mainMenuCanvasController?.BackToMainMenu();
    }

    public void OnClickTestMap1()
    {
        GlobalGameEventManager.NewGame(GameMode.Orccheim);

        GameSceneManager.Instance?.SwitchToScene(HashSceneNameString.TEST_MAP_1);
    }

    public void OnClickTestMap2()
    {
        GlobalGameEventManager.NewGame(GameMode.Orccheim);

        GameSceneManager.Instance?.SwitchToScene(HashSceneNameString.TEST_MAP_2);
    }

    public void OnClickTestPlane()
    {
        GameSceneManager.Instance?.SwitchToScene(HashSceneNameString.TEST_PLANE);
    }
}
