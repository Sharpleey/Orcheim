using UnityEngine;

[CreateAssetMenu(menuName = "EnemySpawnConfig/EnemySpawnConfig", fileName = "EnemySpawnConfig", order = 0)]
public class EnemySpawnConfig : ScriptableObject
{
    [Tooltip("Префаб юнита")]
    [SerializeField] private EnemyUnit _prefabUnit;

    [Tooltip("Первая волна появления")]
    [SerializeField, Range(1, 24)] private int _spawnWave = 1;

    [Tooltip("Прирост кол-ва юнитов")]
    [SerializeField, Range(0, 16)] private int _incrementCountEnemy = 2;

    [Tooltip("Волна, каждую которую просиходит прирост кол-ва юнитов")]
    [SerializeField, Range(1, 16)] private int _incrementWaveCountEnemy = 2;

    public EnemyUnit PrefabUnit => _prefabUnit;
    public int SpawnWave => _spawnWave;
    public int IncrementCountEnemy => _incrementCountEnemy;
    public int IncrementWaveCountEnemy => _incrementWaveCountEnemy;
}
