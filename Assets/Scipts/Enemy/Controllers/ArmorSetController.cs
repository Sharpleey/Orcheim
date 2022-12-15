using UnityEngine;

/// <summary>
/// Класс отвечает за сет брони, который будет использовать персонаж. Оно выбирается рандомно из массмва с броней. 
/// Тот сет брони, который будет использоваться данным типом врагов, заносим в массив armorSets в инспекторе.
/// </summary>
public class ArmorSetController : MonoBehaviour
{
    /// <summary>
    /// Используемы сет брони. Задаем в инспекторе и случайно в методе Start
    /// </summary>
    [SerializeField] private GameObject _usedArmorSet;
    /// <summary>
    /// Массив объектов с броней, котороу будет использвать данный тип врагов
    /// </summary>
    [SerializeField] private GameObject[] _armorSets;

    void Start()
    {
        // Диактевируем все сеты брони на случай, если один или несколько были активный в префабе Orc
        DisableArmorSets();

        if (!_usedArmorSet)
        {
            // Активируем случаный объект с сетом брони
            SetRandomArmorSet();
        }
    }

    /// <summary>
    /// Метод активирует случайный сет брони из массива armorSets
    /// </summary>
    private void SetRandomArmorSet()
    {
        int indexArmorSet = Random.Range(0, _armorSets.Length);

        _usedArmorSet = _armorSets[indexArmorSet];

        _usedArmorSet.SetActive(true);
    }

    /// <summary>
    /// Метод деактивирует все сеты брони
    /// </summary>
    private void DisableArmorSets()
    {
        foreach(GameObject armorSet in  _armorSets)
        {
            armorSet.SetActive(false);
        }
    }
}
