using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonAwardController : MonoBehaviour, IPointerClickHandler
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

    #endregion Serialize fields

    #region Private fields

    private Award _award;

    #endregion Private fields

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
        Debug.Log("OnClickAward");

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
        }


        GlobalGameEventManager.PauseGame(false);

        _gameMenuCanvasController?.ShowCanvas(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickAward();
    }

    #endregion Public methods
}
