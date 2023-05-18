using UnityEngine;
using UnityEngine.UI;

public class LearnMenuController : MonoBehaviour
{
    #region Serialize fields

    [Header("Button")]
    [SerializeField] private Button _buttonEnemies;
    [SerializeField] private GameObject _subMenuEnemies;

    [Space(5)]
    [SerializeField] private Button _buttonAttackModifiers;
    [SerializeField] private GameObject _subMenuAttackModifiers;

    [Space(5)]
    [SerializeField] private Button _buttonEffects;
    [SerializeField] private GameObject _subMenuEffects;

    [Space(5)]
    [SerializeField] private Button _buttonWeapons;
    [SerializeField] private GameObject _subMenuWeapons;

    [Space(5)]
    [SerializeField] private Button _buttonOther;
    [SerializeField] private GameObject _subMenuOther;

    [Space(5)]
    [SerializeField] private Color _colorSelectable;
    [SerializeField] private Color _colorNormal;

    #endregion Serialize fields

    #region Private fields

    private MainMenuCanvasController _mainMenuCanvasController;

    private GameObject _activeSubMenu;
    private Button _activeButton;

    #endregion Private fields

    #region Mono

    private void OnEnable()
    {
        DeselectAllButton();
        DisableAllSubMenu();
        ShowMenu(_subMenuEnemies, _buttonEnemies);
    }

    private void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
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
        ColorBlock cb = _buttonEnemies.colors;
        cb.normalColor = _colorNormal;

        _buttonEnemies.colors = cb;
        _buttonAttackModifiers.colors = cb;
        _buttonEffects.colors = cb;
        _buttonWeapons.colors = cb;
        _buttonOther.colors = cb;
    }

    private void DisableAllSubMenu()
    {
        _subMenuEnemies.SetActive(false);
        _subMenuAttackModifiers.SetActive(false);
        _subMenuEffects.SetActive(false);
        _subMenuWeapons.SetActive(false);
        _subMenuOther.SetActive(false);
    }

    #endregion Private methods

    #region Button event handlers

    public void OnClickEnemies()
    {
        ShowMenu(_subMenuEnemies, _buttonEnemies);
    }

    public void OnClickAttackModifiers()
    {
        ShowMenu(_subMenuAttackModifiers, _buttonAttackModifiers);
    }

    public void OnClickEffects()
    {
        ShowMenu(_subMenuEffects, _buttonEffects);
    }

    public void OnClickWeapons()
    {
        ShowMenu(_subMenuWeapons, _buttonWeapons);
    }

    public void OnClickOther()
    {
        ShowMenu(_subMenuOther, _buttonOther);
    }

    public void OnClickBack()
    {
        _mainMenuCanvasController?.BackToMainMenu();
    }

    #endregion Button event handlers
}
