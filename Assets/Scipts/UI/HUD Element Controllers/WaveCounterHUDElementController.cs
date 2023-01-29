using UnityEngine;
using TMPro;

/// <summary>
/// ������ HUD ��������: ������� ����
/// </summary>
public class WaveCounterHUDElementController : MonoBehaviour
{
    [Header("����� ������ ��������")]
    [SerializeField] private TextMeshProUGUI _waveValueText;

    private void Awake()
    {
        WaveEventManager.OnWaveIsComing.AddListener(SetValueWaveCounter);
    }

    private void Start()
    {
        if (_waveValueText)
            _waveValueText.text = "";
    }

    /// <summary>
    /// ����� ������������ �������� ��� �������� ����, ������ ��� � ���� ������
    /// </summary>
    /// <param name="wave">����� �����</param>
    private void SetValueWaveCounter(int wave)
    {
        if (_waveValueText)
            _waveValueText.text = wave.ToString();
    }
}
