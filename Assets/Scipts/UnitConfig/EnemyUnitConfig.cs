using UnityEngine;

[CreateAssetMenu(menuName = "UnitConfig/EnemyUnitConfig", fileName = "EnemyUnitConfig", order = 0)]
public class EnemyUnitConfig : UnitConfig
{
    #region Fields

    [Header("Награда за убиство в золоте")]
    [Tooltip("Начальная стоимость")]
    [SerializeField, Min(0)] private int _gold;
    [Tooltip("Прирост стоимости за уровень юнита")]
    [SerializeField, Min(0)] private int _increaseGold;

    [Header("Награда за убиство в опыте")]
    [Tooltip("Начальный опыт")]
    [SerializeField, Min(0)] private int _exp;
    [Tooltip("Прирост стоимости за уровень юнита")]
    [SerializeField, Min(0)] private int _increaseExp;

    [Header("Название и описание юнита врага")]
    [Tooltip("Название")]
    [SerializeField] private string _name;
    [Tooltip("Описание"), TextArea]
    [SerializeField] private string _description;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Золота за убийство
    /// </summary>
    public int Gold => _gold;
    /// <summary>
    /// Увеличение награды в виде золота за уровень юнита
    /// </summary>
    public int IncreaseGold => _increaseGold;

    /// <summary>
    /// Опыта за убийство
    /// </summary>
    public int Experience => _exp;
    /// <summary>
    /// Увеличение награды в виде опыта за уровень юнита
    /// </summary>
    public int IncreaseExperience => _increaseExp;
    #endregion Properties
}
