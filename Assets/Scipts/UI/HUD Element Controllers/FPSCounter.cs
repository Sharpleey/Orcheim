using UnityEngine;
using TMPro;

/// <summary>
/// Скрипт HUD элемента: Cчетчик кадров
/// </summary>
public class FPSCounter : MonoBehaviour
{
    [Header("Частота обновления счетчика")]
    [SerializeField, Range(0.1f, 1f)] private float _rateUpdateFps = 0.25f;

    [Header("Текст вывода значения")]
    [SerializeField] private TextMeshProUGUI _fpsValueText;

    private int _fps;
    private float _timer = 0;

    void Update()
    {
        _timer += Time.unscaledDeltaTime;

        if(_timer > _rateUpdateFps)
        {
            _fps = (int)(1f / Time.unscaledDeltaTime);

            _fpsValueText.text = _fps.ToString();

            _timer = 0;
        }
    }
}
