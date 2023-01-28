using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Скрипт HUD элемента: Игровые оповещения
/// </summary>
public class GameNotification : MonoBehaviour
{
    [Header("Текст для вывода сообщения")]
    [SerializeField] private TextMeshProUGUI _gameNotificationText;

    private void Awake()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_OnGameMapStarded);
        WaveEventManager.OnPreparingForWave.AddListener(EventHandler_PreparingForWave);
        WaveEventManager.OnWaveIsComing.AddListener(EventHandler_WaveIsComing);
        WaveEventManager.OnWaveIsOver.AddListener(EventHandler_WaveIsOver);
    }

    private void Start()
    {
        if (_gameNotificationText)
            _gameNotificationText.text = "";
    }

    /// <summary>
    /// Вывод оповещения с задержкой на экран игрока
    /// </summary>
    /// <param name="text">Текст оповещения</param>
    /// <param name="delay">Задержка в секундах</param>
    /// <returns></returns>
    private IEnumerator SetTextGameNotificationWithDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_gameNotificationText)
            _gameNotificationText.text = text;
    }

    #region Event handlers
    private void EventHandler_OnGameMapStarded()
    {
        StartCoroutine(SetTextGameNotificationWithDelay(HashGameNotificationString.CLEAR_VILLAGE, 5));
    }

    private void EventHandler_PreparingForWave(int wave)
    {
        StartCoroutine(SetTextGameNotificationWithDelay(HashGameNotificationString.PREPARING_FOR_WAVE, 0));
    }

    private void EventHandler_WaveIsComing(int wave)
    {
        StartCoroutine(SetTextGameNotificationWithDelay(HashGameNotificationString.WAVE_IS_COMING, 0));
    }

    private void EventHandler_WaveIsOver()
    {
        StartCoroutine(SetTextGameNotificationWithDelay(HashGameNotificationString.WAVE_IS_OVER, 0));
    }

    #endregion Event handlers
}
