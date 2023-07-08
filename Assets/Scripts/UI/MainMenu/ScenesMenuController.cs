using UnityEngine;
using Zenject;

public class ScenesMenuController : MonoBehaviour
{
    private MainMenuCanvasController _mainMenuCanvasController;

    private GameSceneManager _sceneManager;

    private void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }

    [Inject]
    private void Construct(GameSceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    public void OnClickBack()
    {
        _mainMenuCanvasController?.BackToMainMenu();
    }

    public void OnClickTestMap1()
    {
        GlobalGameEventManager.NewGame(GameMode.Orccheim);

        _sceneManager?.SwitchToScene(HashSceneNameString.TEST_MAP_1);
    }

    public void OnClickTestMap2()
    {
        GlobalGameEventManager.NewGame(GameMode.Orccheim);

        _sceneManager?.SwitchToScene(HashSceneNameString.TEST_MAP_2);
    }

    public void OnClickTestPlane()
    {
        _sceneManager?.SwitchToScene(HashSceneNameString.TEST_PLANE);
    }
}
