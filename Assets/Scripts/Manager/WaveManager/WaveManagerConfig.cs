using UnityEngine;

[CreateAssetMenu(menuName = "ManagerConfig/WaveManagerConfig", fileName = "WaveManagerConfig", order = 0)]
public class WaveManagerConfig : ScriptableObject
{
    #region Serialize fields

    [Min(1)]
    [SerializeField] private int _startWave = 1;

    [Space(10)]
    [SerializeField] private int _delayToFirstBroadcastPreparingForWave = 30;
    [SerializeField] private int _delayToBroadcastWaveIsComing = 10;
    [SerializeField] private int _delayToBroadcastPreparingForWave = 5;

    #endregion Serialize fields

    public int StartWave => _startWave;
    public int DelayToFirstBroadcastPreparingForWave => _delayToBroadcastPreparingForWave;
    public int DelayToBroadcastWaveIsComing => _delayToBroadcastWaveIsComing;
    public int DelayToBroadcastPreparingForWave => _delayToBroadcastPreparingForWave;
}
