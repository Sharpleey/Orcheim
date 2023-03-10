using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private Button _buttonGameSettings;
    [SerializeField] private GameObject _subMenuGameSettings;

    [Space(5)]
    [SerializeField] private Button _buttonControlsSettings;
    [SerializeField] private GameObject _subMenuControlsSettings;

    [Space(5)]
    [SerializeField] private Button _buttonAudioSettings;
    [SerializeField] private GameObject _subMenuAudioSettings;

    [Space(5)]
    [SerializeField] private Button _buttonVideoSettings;
    [SerializeField] private GameObject _subMenuVideoSettings;

    [Space(5)]
    [SerializeField] private Button _buttonOtherSettings;
    [SerializeField] private GameObject _subMenuOtherSettings;

    [Header("Button")]
    [SerializeField] private Color _colorSelectable;
    [SerializeField] private Color _colorNormal;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer _generalAudioMixer;

    private GameObject _activeSubMenu;
    private Button _activeButton;

    private MainMenuCanvasController _mainMenuCanvasController;
    private void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }

    private void OnEnable()
    {
        DeselectAllButton();
        DisableAllSubMenu();
        ShowMenu(_subMenuAudioSettings, _buttonAudioSettings);
    }

    private void ShowMenu(GameObject menu, Button button)
    {
        _activeSubMenu?.SetActive(false);
        _activeSubMenu = menu;
        _activeSubMenu?.SetActive(true);

        ColorBlock cb;

        if (_activeButton)
        {
            cb = _activeButton.colors;
            cb.normalColor = _colorNormal;
            _activeButton.colors = cb;
        }

        _activeButton = button;

        cb = _activeButton.colors;
        cb.normalColor = _colorSelectable;
        _activeButton.colors = cb;
    }

    private void DeselectAllButton()
    {
        ColorBlock cb = _buttonGameSettings.colors;
        cb.normalColor = _colorNormal;

        _buttonGameSettings.colors = cb;
        _buttonControlsSettings.colors = cb;
        _buttonAudioSettings.colors = cb;
        _buttonVideoSettings.colors = cb;
        _buttonOtherSettings.colors = cb;
    }

    private void DisableAllSubMenu()
    {
        _subMenuGameSettings.SetActive(false);
        _subMenuControlsSettings.SetActive(false);
        _subMenuAudioSettings.SetActive(false);
        _subMenuVideoSettings.SetActive(false);
        _subMenuOtherSettings.SetActive(false);
    }

    #region OnClick

    public void OnClickGameSettings()
    {
        ShowMenu(_subMenuGameSettings, _buttonGameSettings);
    }

    public void OnClickControlsSettings()
    {
        ShowMenu(_subMenuControlsSettings, _buttonControlsSettings);
    }

    public void OnClickAudioSettings()
    {
        ShowMenu(_subMenuAudioSettings, _buttonAudioSettings);
    }

    public void OnClickVideoSettings()
    {
        ShowMenu(_subMenuVideoSettings, _buttonVideoSettings);
    }

    public void OnClickOtherSettings()
    {
        ShowMenu(_subMenuOtherSettings, _buttonOtherSettings);
    }

    public void OnClickBack()
    {
        _mainMenuCanvasController?.BackToMainMenu();
    }

    #endregion OnClick

    #region Volume Sliders

    public void SetGeneralVolume(float volume)
    {
        _generalAudioMixer.SetFloat("GeneralVolume", volume);
    }

    public void SetMainMenuMusicVolume(float volume)
    {
        _generalAudioMixer.SetFloat("MainMenuMusicVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        _generalAudioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetAmbientVolume(float volume)
    {
        _generalAudioMixer.SetFloat("AmbientVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        _generalAudioMixer.SetFloat("SFXVolume", volume);
    }
    public void SetNPCVolume(float volume)
    {
        _generalAudioMixer.SetFloat("NPCVolume", volume);
    }

    #endregion Volume Sliders
}
