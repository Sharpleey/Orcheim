using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Конфиг файл для EnemyManager-a c настройками спавна юнитов и т.п.
/// </summary>
[CreateAssetMenu(menuName = "ManagerConfig/EnemyManagerConfig", fileName = "EnemyManagerConfig", order = 0)]
public class EnemyManagerConfig : ScriptableObject
{
    #region Serialize fields

    [Header("Настройки кол-ва врагов на сцене")]

    [Tooltip("Максиммальное значение врагов на сцене")]
    [SerializeField][Range(8, 80)] public int _defaultMaximumEnemiesOnScene = 16;

    [Tooltip("Значение прироста маскимального значения врагов на сцене")]
    [SerializeField][Range(1, 8)] int _incrementMaximumEnemiesOnScene = 2;

    [Tooltip("Номер волны, после каждой которой происходит приращение максиммального значения врагов на сцене")]
    [SerializeField][Range(1, 10)] int _incrementWaveMaximumEnemiesOnScene = 4;

    [Header("Настройки кол-ва врагов на за одну волну")]
    [Space(20)]

    [Tooltip("Максиммальное значение врагов за волну")]
    [SerializeField][Range(16, 256)] int _defaultMaximumEnemiesOnWave = 32;

    [Tooltip("Значение прироста маскимального значения врагов за волну")]
    [SerializeField][Range(1, 8)] int _incrementMaximumEnemiesOnWave = 4;

    [Tooltip("Номер волны, после каждой которой происходит приращение максиммального значения врагов за волну")]
    [SerializeField][Range(1, 10)] int _incrementWaveMaximumEnemiesOnWave = 3;

    [Header("Префаб основного юнита")]
    [Space(20)]
    [SerializeField] private EnemyUnit _prefabMainUnit;

    [Header("Конфиг файлы с префабом и настройками спавна особых юнитов")]
    [SerializeField] private List<EnemySpawnConfig> _enemySpawnConfigs;

    #endregion Serialize fields

    #region Properties

    /// <summary>
    /// Максиммальное значение врагов на сцене
    /// </summary>
    public int DefaultMaximumEnemiesOnScene => _defaultMaximumEnemiesOnScene;
    /// <summary>
    /// Значение прироста маскимального значения врагов на сцене
    /// </summary>
    public int IncrementMaximumEnemiesOnScene => _incrementMaximumEnemiesOnScene;
    /// <summary>
    /// Номер волны, каждую которую происходит приращение максиммального значения врагов на сцене
    /// </summary>
    public int IncrementWaveMaximumEnemiesOnScene => _incrementWaveMaximumEnemiesOnScene;

    /// <summary>
    /// Максиммальное значение врагов за волну
    /// </summary>
    public int DefaultMaximumEnemiesOnWave => _defaultMaximumEnemiesOnWave;
    /// <summary>
    /// Значение прироста маскимального значения врагов за волну
    /// </summary>
    public int IncrementMaximumEnemiesOnWave => _incrementMaximumEnemiesOnWave;
    /// <summary>
    /// Номер волны, после каждой которой происходит приращение максиммального значения врагов за волну
    /// </summary>
    public int IncrementWaveMaximumEnemiesOnWave => _incrementWaveMaximumEnemiesOnWave;

    /// <summary>
    /// Префаб основного юнита врага
    /// </summary>
    public EnemyUnit PrefabMainUnit => _prefabMainUnit;
    /// <summary>
    /// Конфиг файлы с префабом и настройками спавна особых юнитов
    /// </summary>
    public List<EnemySpawnConfig> EnemySpawnConfigs => _enemySpawnConfigs;

    #endregion Properties
}
