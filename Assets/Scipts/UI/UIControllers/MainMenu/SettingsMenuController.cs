using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _subMenuGameSettings;
    [SerializeField] private GameObject _subMenuControlsSettings;
    [SerializeField] private GameObject _subMenuAudioSettings;
    [SerializeField] private GameObject _subMenuVideoSettings;
    [SerializeField] private GameObject _subMenuOtherSettings;

    private GameObject _activeSubMenu;

    private MainMenuCanvasController _mainMenuCanvasController;
    private void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }

    private void OnEnable()
    {
        ShowMenu(_subMenuGameSettings);
    }

    private void ShowMenu(GameObject menu)
    {
        _activeSubMenu?.SetActive(false);
        _activeSubMenu = menu;
        _activeSubMenu?.SetActive(true);
    }

    public void OnClickGameSettings()
    {
        ShowMenu(_subMenuGameSettings);
    }

    public void OnClickControlsSettings()
    {
        ShowMenu(_subMenuControlsSettings);
    }

    public void OnClickAudioSettings()
    {
        ShowMenu(_subMenuAudioSettings);
    }

    public void OnClickVideoSettings()
    {
        ShowMenu(_subMenuVideoSettings);
    }

    public void OnClickOtherSettings()
    {
        ShowMenu(_subMenuOtherSettings);
    }

    public void OnClickBack()
    {
        _mainMenuCanvasController?.BackToMainMenu();
    }
}
