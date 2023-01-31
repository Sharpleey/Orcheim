
/// <summary>
/// Скрипт HUD элемента: Опыт игрока
/// </summary>
public class ExperienceHUDElementController : HUDElementController
{
    protected override void AddListeners()
    {
        PlayerEventManager.OnPlayerExperienceChanged.AddListener(UpdatetValueText);
    }

    protected override void UpdatetValueText()
    {
        string valueText = $"{_playerUnit?.Experience} / {_playerUnit?.ExperienceForNextLevel}";
        SetValueText(valueText);
    }
}
