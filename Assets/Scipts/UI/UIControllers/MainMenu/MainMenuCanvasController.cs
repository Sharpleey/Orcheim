using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{
    [field: SerializeField] public GameObject MainMenu { get; private set; }
    [field: SerializeField] public GameObject NewGameMenu { get; private set; }
    [field: SerializeField] public GameObject SettingsMenu { get; private set; }
    [field: SerializeField] public GameObject LoadMenu { get; private set; }
    [field: SerializeField] public GameObject ScenesMenu { get; private set; }

    private GameObject _activeMenu;
    
    private void Start()
    {
        HideAll();

        ShowMenu(MainMenu);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        AudioManager.Instance?.PlayRandomSound(SoundType.MainMenuTheme);
    }
    
    private void HideAll()
    {
        MainMenu?.SetActive(false);
        NewGameMenu?.SetActive(false);
        SettingsMenu?.SetActive(false);
        LoadMenu?.SetActive(false);
        ScenesMenu?.SetActive(false);
    }

    public void ShowMenu(GameObject menu)
    {
        _activeMenu?.SetActive(false);
        _activeMenu = menu;
        _activeMenu?.SetActive(true);
    }

    public void BackToMainMenu()
    {
        ShowMenu(MainMenu);
    }
}
