using TMPro;
using UnityEngine;

/// <summary>
/// Скрипт UI  элемента: Счетчик оставшихся врагов
/// </summary>
public class EnemiesRemainingCounter : MonoBehaviour
{
    [Header("Текст вывода значения")]
    [SerializeField] private TextMeshProUGUI _enemiesRemainingValueText;

    private void Awake()
    {
        SpawnEnemyEventManager.OnEnemiesRemaining.AddListener(SetValueEnemiesRemainingCounter); //TODO Поменять событие
    }

    private void Start()
    {
        if (_enemiesRemainingValueText)
            _enemiesRemainingValueText.text = "";
    }

    /// <summary>
    /// Метод устанавлиает значение для счетчика оставшихся врагов, выводя его в виде текста
    /// </summary>
    /// <param name="wave">Номер волны</param>
    private void SetValueEnemiesRemainingCounter(int enemiesRemaining)
    {
        if (_enemiesRemainingValueText)
            _enemiesRemainingValueText.text = enemiesRemaining.ToString();
    }
}
