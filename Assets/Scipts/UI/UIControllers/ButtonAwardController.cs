using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonAwardController : MonoBehaviour
{
    #region Serialize fields
    [Header("���������� �������� ����")]
    [SerializeField] private GameMenuCanvasController _gameMenuCanvasController;

    [Header("���� ��� ������ ����� ������")]
    [SerializeField] private Color _colorAwardAttackModifaer;
    [SerializeField] private Color _colorAwardAttackModifierUpgrade;
    [SerializeField] private Color _colorAwardPlayerStatsUpgrade;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _textTypeNameAward;
    [SerializeField] private TextMeshProUGUI _textNameAward;
    [SerializeField] private TextMeshProUGUI _textDescriptionAward;
    [SerializeField] private TextMeshProUGUI _textLevelUpgrade;

    #endregion Serialize fields

    #region Properties
    #endregion Properties

    #region Private fields

    private Award _award;

    #endregion Private fields

    #region Mono

    private void OnEnable()
    {
        _award = LootManager.Instance?.GetRandomAward();

        _textTypeNameAward.text = _award?.TypeName;
        _textNameAward.text = _award?.Name;
        _textLevelUpgrade.text = "";

        // TODO ����������
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
                _textLevelUpgrade.text = $"������� {awardAttackModifierUpgrade?.UpgratableParameter?.Level + 1}";

                _textTypeNameAward.color = _colorAwardAttackModifierUpgrade;
                _textLevelUpgrade.color = _colorAwardAttackModifierUpgrade;
            }

            if (_award is AwardPlayerStatsUpgrade awardPlayerStatsUpgrade)
            {
                _textDescriptionAward.text = awardPlayerStatsUpgrade?.UpgratableParameter?.UpgradeDescription;
                _textLevelUpgrade.text = $"������� {awardPlayerStatsUpgrade?.UpgratableParameter?.Level + 1}";

                _textTypeNameAward.color = _colorAwardPlayerStatsUpgrade;
                _textLevelUpgrade.color = _colorAwardPlayerStatsUpgrade;
            }
        }

         
    }
    #endregion Mono

    #region Private methods
    #endregion Private methods

    #region Public methods

    /// <summary>
    /// TODO ����������
    /// </summary>
    public void OnClickAward()
    {
        if(_award is AwardAttackModifaer awardAttackModifaer)
        {
            // ��������� ����������� ����� ������
            PlayerManager.Instance?.PlayerUnit?.SetActiveAttackModifier(awardAttackModifaer.AttackModifier);

            // ������� ����������� ����� �� ���� ������
            LootManager.Instance?.RemoveAwardAttackModifier(awardAttackModifaer);
        }

        if(_award is AwardAttackModifierUpgrade awardAttackModifierUpgrade)
        {
            // �������� �������� ������������ �����
            awardAttackModifierUpgrade.UpgratableParameter.Upgrade();
        }

        if (_award is AwardPlayerStatsUpgrade awardPlayerStatsUpgrade)
        {
            // �������� �������� ������������ �����
            awardPlayerStatsUpgrade.UpgratableParameter.Upgrade();
        }

        _gameMenuCanvasController?.Pause(false);
        _gameMenuCanvasController?.ShowCanvas(false);
    }

    #endregion Public methods
}
