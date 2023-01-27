using UnityEngine;
using TMPro;

/// <summary>
/// ������ UI  ��������: ������� ����
/// </summary>
public class WaveCounter : MonoBehaviour
{
    [Header("����� ������ ��������")]
    [SerializeField] private TextMeshProUGUI _waveValueText;

    private void Awake()
    {
        WaveEventManager.OnWaveIsComing.AddListener(SetTextWaveCounter);
    }

    private void Start()
    {
        if (_waveValueText)
            _waveValueText.text = "";
    }

    /// <summary>
    /// ����� ������������ ����� ��� �������� ����
    /// </summary>
    /// <param name="wave">����� �����</param>
    private void SetTextWaveCounter(int wave)
    {
        if (_waveValueText)
            _waveValueText.text = wave.ToString();
    }
}
