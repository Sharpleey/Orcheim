using UnityEngine;

public class NewGameMenuController : MonoBehaviour
{
    [SerializeField] GameObject _subMenuGameModeSelection;
    [SerializeField] GameObject _subMenuGameModeClassic;
    [SerializeField] GameObject _subMenuGameModeOrcheim;

    private MainMenuCanvasController _mainMenuCanvasController;

    private GameObject _activeSubMenu;

    void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }
    private void OnEnable()
    {
        HideAll();

        ShowSubMenu(_subMenuGameModeSelection);
    }

    private void ShowSubMenu(GameObject menu)
    {
        _activeSubMenu?.SetActive(false);
        _activeSubMenu = menu;
        _activeSubMenu?.SetActive(true);
    }

    private void HideAll()
    {
        _subMenuGameModeSelection?.SetActive(false);
        _subMenuGameModeClassic?.SetActive(false);
        _subMenuGameModeOrcheim?.SetActive(false);
    }

    public void OnClickBackToMainMenu()
    {
        _mainMenuCanvasController?.BackToMainMenu();
    }

    public void OnClickBackToGameModeSelectionMenu()
    {
        ShowSubMenu(_subMenuGameModeSelection);
    }

    public void OnClickGameModeClassic()
    {
        ShowSubMenu(_subMenuGameModeClassic);
    }

    public void OnClickGameModeOrcheim()
    {
        ShowSubMenu(_subMenuGameModeOrcheim);
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
}