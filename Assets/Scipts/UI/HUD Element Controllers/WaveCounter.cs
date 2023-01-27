using UnityEngine;
using TMPro;

/// <summary>
/// Скрипт UI  элемента: Счетчик волн
/// </summary>
public class WaveCounter : MonoBehaviour
{
    [Header("Текст вывода значения")]
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
    /// Метод устанавлиает текст для счетчика волн
    /// </summary>
    /// <param name="wave">Номер волны</param>
    private void SetTextWaveCounter(int wave)
    {
        if (_waveValueText)
            _waveValueText.text = wave.ToString();
    }
}
