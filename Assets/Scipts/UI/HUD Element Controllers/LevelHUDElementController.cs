
/// <summary>
/// ������ HUD ��������: ������� ������
/// </summary>
public class LevelHUDElementController : HUDElementController
{
    protected override void AddListeners()
    {
        PlayerEventManager.OnPlayerLevelUp.AddListener(UpdatetValueText);
    }

    protected override void UpdatetValueText()
    {
            SetValueText(_playerUnit?.Level.ToString());
    }
}
