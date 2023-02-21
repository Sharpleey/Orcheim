
/// <summary>
/// Скрипт HUD элемента: Счетчик оставшихся врагов
/// </summary>
public class EnemiesRemainingHUDElementController : HUDElementController
{
    protected override void AddListeners()
    {
        EnemyEventManager.OnUpdateCountEnemiesRemaining.AddListener(UpdatetValueText);
    }

    private void UpdatetValueText(int countEnemy)
    {
        SetValueText(countEnemy.ToString());
    }
}
