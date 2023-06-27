
/// <summary>
/// Скрипт HUD элемента: Счетчик волн
/// </summary>
public class WaveCounterHUDElementController : HUDElementController
{
    protected override void AddListeners()
    {
        WaveEventManager.OnWaveIsComing.AddListener(UpdatetValueText);
    }

    private void UpdatetValueText(int wave)
    {
        SetValueText(wave.ToString());
    }
}
