
/// <summary>
/// Скрипт HUD элемента, отображающего кол-во золота у игрока
/// </summary>
public class GoldHUDElementController : HUDElementController
{
    protected override void AddListeners()
    {
        PlayerEventManager.OnPlayerGoldChanged.AddListener(UpdatetValueText);
    }

    protected override void UpdatetValueText()
    {
        SetValueText(_playerUnit?.Gold.ToString());
    }
}
