
/// <summary>
/// ������ HUD ��������, ������������� ���-�� ������ � ������
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
