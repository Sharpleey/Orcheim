using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _load;
    [SerializeField] private GameObject _scenes;

    private GameObject _activeMenu;

    private GameSceneManager _gameSceneManager;
    
    private void Start()
    {
        HideAll();

        ShowMenu(_mainMenu);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        _gameSceneManager = GameSceneManager.Instance;

        AudioManager.Instance.PlayRandomSound(SoundType.MainMenuTheme);
    }

    public void OnClickNewGame()
    {

    }
    public void OnClickLoad()
    {
        ShowMenu(_load);
    }
    public void OnClickScenes()
    {
        ShowMenu(_scenes);
    }
    public void OnClickSettings()
    {
        ShowMenu(_settings);
    }
    public void OnClickBackMainMenu()
    {
        ShowMenu(_mainMenu);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickTestMap1()
    {
        GlobalGameEventManager.NewGame(GameMode.Orccheim);

        if(_gameSceneManager)
            _gameSceneManager.SwitchToScene(HashSceneNameString.TEST_MAP_1);
    }

    public void OnClickTestMap2()
    {
        GlobalGameEventManager.NewGame(GameMode.Orccheim);

        if (_gameSceneManager)
            _gameSceneManager.SwitchToScene(HashSceneNameString.TEST_MAP_2);
    }

    public void OnClickTestPlane()
    {
        if (_gameSceneManager)
            _gameSceneManager.SwitchToScene(HashSceneNameString.TEST_PLANE);
    }

    private void ShowMenu(GameObject menu)
    {
        _activeMenu?.SetActive(false);
        _activeMenu = menu;
        _activeMenu?.SetActive(true);
    }

    private void HideAll()
    {
        _mainMenu?.SetActive(false);
        _settings?.SetActive(false);
        _load?.SetActive(false);
        _scenes?.SetActive(false);
    }
}
