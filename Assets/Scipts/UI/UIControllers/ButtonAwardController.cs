using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonAwardController : MonoBehaviour
{
    #region Serialize fields
    [Header("Контроллер игрового меню")]
    [SerializeField] private GameMenuCanvasController _gameMenuCanvasController;

    [Header("Цвет для разных типов наград")]
    [SerializeField] private Color _colorAwardAttackModifaer;
    [SerializeField] private Color _colorAwardAttackModifierUpgrade;
    [SerializeField] private Color _colorAwardPlayerStatsUpgrade;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _textTypeNameAward;
    [SerializeField] private TextMeshProUGUI _textNameAward;
    [SerializeField] private TextMeshProUGUI _textDescriptionAward;
    [SerializeField] private TextMeshProUGUI _textLevelUpgrade;

    #endregion Serialize fields

    #region Private fields

    private Award _award;

    #endregion Private fields

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
            if (_award is AwardAttackModifaer awardAttackModifaer)
            {
                _textDescriptionAward.text = awardAttackModifaer?.AttackModifier.Description;
                _textTypeNameAward.color = _colorAwardAttackModifaer;
            }

            if (_award is AwardAttackModifierUpgrade awardAttackModifierUpgrade)
            {
                _textDescriptionAward.text = awardAttackModifierUpgrade?.UpgratableParameter?.UpgradeDescription;
                _textLevelUpgrade.text = $"Уровень {awardAttackModifierUpgrade?.UpgratableParameter?.Level + 1}";

                _textTypeNameAward.color = _colorAwardAttackModifierUpgrade;
                _textLevelUpgrade.color = _colorAwardAttackModifierUpgrade;
            }

            if (_award is AwardPlayerStatsUpgrade awardPlayerStatsUpgrade)
            {
                _textDescriptionAward.text = awardPlayerStatsUpgrade?.UpgratableParameter?.UpgradeDescription;
                _textLevelUpgrade.text = $"Уровень {awardPlayerStatsUpgrade?.UpgratableParameter?.Level + 1}";

                _textTypeNameAward.color = _colorAwardPlayerStatsUpgrade;
                _textLevelUpgrade.color = _colorAwardPlayerStatsUpgrade;
            }
        }
    }

    /// <summary>
    /// TODO Переделать
    /// </summary>
    public void OnClickAward()
    {
        if(_award is AwardAttackModifaer awardAttackModifaer)
        {
            // Добавляем модификатор атаки игроку
            PlayerManager.Instance?.PlayerUnit?.SetActiveAttackModifier(awardAttackModifaer.AttackModifier);

            // Удаляем модификатор атаки из пула наград
            LootManager.Instance?.RemoveAwardAttackModifier(awardAttackModifaer);
        }

        if(_award is AwardAttackModifierUpgrade awardAttackModifierUpgrade)
        {
            // Улучшаем параметр модификатора атаки
            awardAttackModifierUpgrade.UpgratableParameter.LevelUp();
        }

        if (_award is AwardPlayerStatsUpgrade awardPlayerStatsUpgrade)
        {
            // Улучшаем параметр модификатора атаки
            awardPlayerStatsUpgrade.UpgratableParameter.LevelUp();
        }

        _gameMenuCanvasController?.Pause(false);
        _gameMenuCanvasController?.ShowCanvas(false);
    }

    #endregion Public methods
}
