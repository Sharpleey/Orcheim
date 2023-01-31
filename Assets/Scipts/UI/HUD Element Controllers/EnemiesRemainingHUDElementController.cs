
/// <summary>
/// ������ HUD ��������: ������� ���������� ������
/// </summary>
public class EnemiesRemainingHUDElementController : HUDElementController
{
    protected override void AddListeners()
    {
        SpawnEnemyEventManager.OnEnemiesRemaining.AddListener(UpdatetValueText);
    }

    private void UpdatetValueText(int countEnemy)
    {
        SetValueText(countEnemy.ToString());
    }
}
