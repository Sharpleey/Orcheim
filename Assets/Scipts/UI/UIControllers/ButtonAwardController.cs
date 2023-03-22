using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonAwardController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Serialize fields
    [Header("Контроллер игрового меню")]
    [SerializeField] private GameMenuCanvasController _gameMenuCanvasController;

    [Header("Цвет для разных типов наград")]
    [SerializeField] private Color _colorAwardAttackModifaer;
    [SerializeField] private Color _colorAwardAttackModifierUpgrade;
    [SerializeField] private Color _colorAwardPlayerStatsUpgrade;
    [SerializeField] private Color _colorDefaultAward;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _textTypeNameAward;
    [SerializeField] private TextMeshProUGUI _textNameAward;
    [SerializeField] private TextMeshProUGUI _textDescriptionAward;
    [SerializeField] private TextMeshProUGUI _textLevelUpgrade;

    [Header("Настройка кнопки")]
    [SerializeField] private Color _colorNormal;
    [SerializeField] private Color _colorPointerEnter;
    [SerializeField, Range(1f, 1.1f)] private float _scaleXYPointerEnter;

    #endregion Serialize fields

    #region Private fields

    private Image _image;
    private RectTransform _rectTransform;

    private Award _award;

    private float _timer;
    private bool _isBlockClick;
    private Vector3 _vector3ScalePointerEnter;

    #endregion Private fields

    private void Awake()
    {
        _vector3ScalePointerEnter = new Vector3(_scaleXYPointerEnter, _scaleXYPointerEnter, 1f);

        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _isBlockClick = true;
        _timer = 0;
    }

    private void Update()
    {
        if(_isBlockClick)
            _timer += Time.unscaledDeltaTime;

        if (_timer >= 2)
            _isBlockClick = false;
    }

    #region Private methods

    private void SetColorAwardText(Color newColor)
    {
        _textTypeNameAward.color = newColor;
        _textLevelUpgrade.color = newColor;
    }

    #endregion Private methods

    #region Public methods

    public void SetAward(Award award)
    {
        _award = award;

        _textTypeNameAward.text = _award?.TypeName;
        _textNameAward.text = _award?.Name;
        _textLevelUpgrade.text = "";

        // TODO Переделать
        if (_textDescriptionAward)
        {
            if (_award is AwardAttackModifier awardAttackModifaer)
            {
                _textDescriptionAward.text = awardAttackModifaer?.AttackModifier.Description;
            }

            if (_award is AwardParameterUpgrade awardParameterUpgrade)
            {
                _textDescriptionAward.text = awardParameterUpgrade?.UpgratableParameter?.UpgradeDescription;
                _textLevelUpgrade.text = $"Уровень {awardParameterUpgrade?.UpgratableParameter?.Level + 1}";
            }

            switch (_award.Type)
            {
                case AwardType.AttackModifaer:
                    SetColorAwardText(_colorAwardAttackModifaer);
                    break;
                case AwardType.AttackModifierUpgrade:
                    SetColorAwardText(_colorAwardAttackModifierUpgrade);
                    break;
                case AwardType.PlayerStatUpgrade:
                    SetColorAwardText(_colorAwardPlayerStatsUpgrade);
                    break;
                default:
                    SetColorAwardText(_colorDefaultAward);
                    break;

            }
        }
    }

    /// <summary>
    /// TODO Переделать
    /// </summary>
    public void OnClickAward()
    {
        if(_award is AwardAttackModifier awardAttackModifaer)
        {
            // Добавляем модификатор атаки игроку
            PlayerManager.Instance?.PlayerUnit?.SetActiveAttackModifier(awardAttackModifaer.AttackModifier);

            // Удаляем модификатор атаки из пула наград
            LootManager.Instance?.RemoveAwardAttackModifier(awardAttackModifaer);
        }

        if(_award is AwardParameterUpgrade awardParameterUpgrade)
        {
            // Улучшаем параметр модификатора атаки
            awardParameterUpgrade.UpgratableParameter.LevelUp();

            if (awardParameterUpgrade.UpgratableParameter is Health)
                PlayerEventManager.PlayerHealthChanged();

            if (awardParameterUpgrade.UpgratableParameter is Armor)
                PlayerEventManager.PlayerArmorChanged();

            if (awardParameterUpgrade.UpgratableParameter is MovementSpeed)
                PlayerEventManager.PlayerMovementSpeedChanged();
        }


        GlobalGameEventManager.PauseGame(false);

        _gameMenuCanvasController?.ShowCanvas(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isBlockClick)
            return;

        OnClickAward();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _colorPointerEnter;
        _rectTransform.localScale = _vector3ScalePointerEnter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _colorNormal;
        _rectTransform.localScale = Vector3.one;
    }

    #endregion Public methods
}
