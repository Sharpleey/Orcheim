using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonAwardController : MonoBehaviour
{
    #region Serialize fields
    [Header("Контроллер игрового меню")]
    [SerializeField] private GameMenuCanvasController _gameMenuCanvasController;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _textTypeNameAward;
    [SerializeField] private TextMeshProUGUI _textNameAward;
    [SerializeField] private TextMeshProUGUI _textDescriptionAward;

    #endregion Serialize fields

    #region Properties
    #endregion Properties

    #region Private fields

    private Award _award;

    #endregion Private fields

    #region Mono

    private void OnEnable()
    {
        if (LootManager.Instance)
        {
            _award = LootManager.Instance.GetRandomAward();

            if (_textTypeNameAward)
                _textTypeNameAward.text = _award.TypeName;
            if (_textNameAward)
                _textNameAward.text = _award.Name;
            if (_textDescriptionAward)
                _textDescriptionAward.text = _award.Description;
        }
        else
        {
            if (_textDescriptionAward)
            {
                _textDescriptionAward.text = "LootManager is not found!";
            }
        }
    }
    #endregion Mono

    #region Private methods
    #endregion Private methods

    #region Public methods

    public void OnClickAward()
    {
        if(!PlayerManager.Instance)
        {
            Debug.Log("PlayerManager is not found!");

            if(_gameMenuCanvasController)
            {
                _gameMenuCanvasController.Pause(false);
                _gameMenuCanvasController.ShowCanvas(false);
            }
            return;
        }

        AwardAttackModifaer? awardAttackModifaer = _award as AwardAttackModifaer;

        if(awardAttackModifaer != null)
        {
            // Добавляем модификатор атаки игроку
            PlayerManager.Instance.PlayerUnit.SetActiveAttackModifier(awardAttackModifaer.AttackModifier);

            // Удаляем модификатор атаки из пула наград
            LootManager.Instance.RemoveAwardAttackModifier(awardAttackModifaer);

            if (_gameMenuCanvasController)
            {
                _gameMenuCanvasController.Pause(false);
                _gameMenuCanvasController.ShowCanvas(false);
            }

            return;
        }

        AwardAttackModifierUpgrade? awardAttackModifierUpgrade = _award as AwardAttackModifierUpgrade;

        if(awardAttackModifierUpgrade != null)
        {
            // Улучшаем параметр модификатора атаки
            awardAttackModifierUpgrade.UpgratableParameter.Upgrade();

            if (_gameMenuCanvasController)
            {
                _gameMenuCanvasController.Pause(false);
                _gameMenuCanvasController.ShowCanvas(false);
            }

            return;
        }

        AwardPlayerStatsUpgrade? awardPlayerStatsUpgrade = _award as AwardPlayerStatsUpgrade;

        if (awardPlayerStatsUpgrade != null)
        {
            // Улучшаем параметр модификатора атаки
            awardPlayerStatsUpgrade.UpgratableParameter.Upgrade();

            if (_gameMenuCanvasController)
            {
                _gameMenuCanvasController.Pause(false);
                _gameMenuCanvasController.ShowCanvas(false);
            }

            return;
        }
    }

    #endregion Public methods
}
