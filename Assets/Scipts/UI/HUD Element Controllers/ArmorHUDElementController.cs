
/// <summary>
/// ������ HUD ��������, ������������� ���-�� ����� � ������
/// </summary>
public class ArmorHUDElementController : HUDElementController
{
    protected override void AddListeners()
    {
        PlayerEventManager.OnPlayerArmorChanged.AddListener(UpdatetValueText);
    }
    protected override void UpdatetValueText()
    {
        SetValueText(_playerUnit?.Armor.Actual.ToString());
    }
}
