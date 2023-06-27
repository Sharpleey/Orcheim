using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    #region Serialize fields

    [Header("Button")]
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

    [Space(5)]
    [SerializeField] private Color _colorSelectable;
    [SerializeField] private Color _colorNormal;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer _generalAudioMixer;
    [Space(5)]
    [SerializeField] private Slider _sliderGeneralVolume;
    [SerializeField] private Slider _sliderMainMenuMusicVolume;
    [SerializeField] private Slider _sliderMusicVolume;
    [SerializeField] private Slider _sliderAmbientVolume;
    [SerializeField] private Slider _sliderSFXVolume;
    [SerializeField] private Slider _sliderNPCVolume;

    [Header("Video Settings")]
    [SerializeField] private TMP_Dropdown _dropdownScreenResolution;
    [SerializeField] private TMP_Dropdown _dropdownGraphicQuality;
    [Space(5)]
    [SerializeField] private Toggle _toggleFullscreen;

    #endregion Serialize fields

    #region Private fields

    private MainMenuCanvasController _mainMenuCanvasController;

    private GameObject _activeSubMenu;
    private Button _activeButton;

    private Resolution[] _resolutions;

    #endregion Private fields

    #region Mono

    private void Awake()
    {
        InitParametersDropdownScreenResolution();
        InitParametersToggleFullscreen();
        InitParametersDropdownGraphicQuality();
        InitParametersAudioSliders();
    }

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

    #endregion Mono

    #region Private methods

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

    private void InitParametersDropdownScreenResolution()
    {
        int currentResolutionIndex = 0;
        List<string> optionsDropdown = new List<string>();

        _resolutions = Screen.resolutions;

        _dropdownScreenResolution.ClearOptions();

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = $"{_resolutions[i].width}x{_resolutions[i].height}";

            optionsDropdown.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _dropdownScreenResolution.AddOptions(optionsDropdown);
        _dropdownScreenResolution.value = currentResolutionIndex;
        _dropdownScreenResolution.RefreshShownValue();
    }

    private void InitParametersDropdownGraphicQuality()
    {
        int currentQualityIndex = QualitySettings.GetQualityLevel();

        _dropdownGraphicQuality.value = currentQualityIndex;
    }

    private void InitParametersToggleFullscreen()
    {
        _toggleFullscreen.isOn = Screen.fullScreen;
    }

    private void InitParametersAudioSliders()
    {
        if (_generalAudioMixer.GetFloat("GeneralVolume", out float valueGeneralVolume))
            _sliderGeneralVolume.value = valueGeneralVolume;

        if (_generalAudioMixer.GetFloat("MainMenuMusicVolume", out float valueMainMenuMusicVolume))
            _sliderMainMenuMusicVolume.value = valueMainMenuMusicVolume;

        if (_generalAudioMixer.GetFloat("MusicVolume", out float valueMusicVolume))
            _sliderMusicVolume.value = valueMusicVolume;

        if (_generalAudioMixer.GetFloat("AmbientVolume", out float valueAmbientVolume))
            _sliderAmbientVolume.value = valueAmbientVolume;

        if (_generalAudioMixer.GetFloat("SFXVolume", out float valueSFXVolume))
            _sliderSFXVolume.value = valueSFXVolume;

        if (_generalAudioMixer.GetFloat("NPCVolume", out float valueNPCVolume))
            _sliderNPCVolume.value = valueNPCVolume;

    }

    #endregion Private methods

    #region Button event handlers

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

    #endregion Button event handlers

    #region Audio handlers

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

    #endregion Audio handlers

    #region Video handlers
    public void SetGraphicQuality(int indexQuality)
    {
        QualitySettings.SetQualityLevel(indexQuality);
    }

    public void SetFullscreen(bool isFullsceen)
    {
        Screen.fullScreen = isFullsceen;
    }

    public void SetScreenResolution(int screenResolutionIndex)
    {
        Resolution resolution = _resolutions[screenResolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion Video handlers
}
