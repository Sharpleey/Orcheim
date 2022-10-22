using UnityEngine;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _subMenuGameSettings;
    [SerializeField] private GameObject _subMenuControlsSettings;
    [SerializeField] private GameObject _subMenuAudioSettings;
    [SerializeField] private GameObject _subMenuVideoSettings;
    [SerializeField] private GameObject _subMenuOtherSettings;

    private GameObject _activeMenu;

    private void OnEnable()
    {
        ShowMenu(_subMenuGameSettings);
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

    private void ShowMenu(GameObject menu)
    {
        _activeMenu?.SetActive(false);
        _activeMenu = menu;
        _activeMenu?.SetActive(true);
    }
}
