using UnityEngine;
using TMPro;

/// <summary>
/// Скрипт HUD элемента: Счетчик волн
/// </summary>
public class WaveCounterHUDElementController : MonoBehaviour
{
    [Header("Текст вывода значения")]
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
    /// Метод устанавлиает значение для счетчика волн, выводя его в виде текста
    /// </summary>
    /// <param name="wave">Номер волны</param>
    private void SetValueWaveCounter(int wave)
    {
        if (_waveValueText)
            _waveValueText.text = wave.ToString();
    }
}
