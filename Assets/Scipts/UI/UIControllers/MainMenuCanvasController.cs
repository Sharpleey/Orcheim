using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _load;
    [SerializeField] private GameObject _scenes;

    private GameObject _activeMenu;
    
    private void Start()
    {
        ShowMenu(_mainMenu);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        Messenger<MusicSoundName>.Broadcast(AudioManager.Event.PLAY_MUSIC, MusicSoundName.MainMenuTheme);
    }

    private void OnDestroy()
    {
        Messenger.Broadcast(AudioManager.Event.STOP_MUSIC);
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

    public void OnClickTestSceneAI()
    {
        Messenger.Broadcast(GameEvent.NEW_GAME_MODE_ORCCHEIM);
        Messenger<string>.Broadcast(GameSceneManager.Event.SWITCH_TO_SCENE, GameSceneManager.Scene.TEST_AI);
    }

    private void ShowMenu(GameObject menu)
    {
        _activeMenu?.SetActive(false);
        _activeMenu = menu;
        _activeMenu?.SetActive(true);
    }
}
