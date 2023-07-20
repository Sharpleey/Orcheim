using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// ������ ���� ��� UnitSpawner-a c ����������� ������ ������ � �.�.
/// </summary>
[CreateAssetMenu(menuName = "ManagerConfig/UnitSpawnerConfig", fileName = "UnitSpawnerConfig", order = 0)]
public class UnitSpawnerConfig : ScriptableObject
{
    #region Serialize fields

    [Header("��������� ���-�� ������ �� �����")]

    [Tooltip("������������� �������� ������ �� �����")]
    [SerializeField][Range(8, 80)] public int _defaultMaximumEnemiesOnScene = 16;

    [Tooltip("�������� �������� ������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 8)] int _incrementMaximumEnemiesOnScene = 2;

    [Tooltip("����� �����, ����� ������ ������� ���������� ���������� �������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 10)] int _incrementWaveMaximumEnemiesOnScene = 4;

    [Header("��������� ���-�� ������ �� �� ���� �����")]
    [Space(20)]

    [Tooltip("������������� �������� ������ �� �����")]
    [SerializeField][Range(16, 256)] int _defaultMaximumEnemiesOnWave = 32;

    [Tooltip("�������� �������� ������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 8)] int _incrementMaximumEnemiesOnWave = 4;

    [Tooltip("����� �����, ����� ������ ������� ���������� ���������� �������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 10)] int _incrementWaveMaximumEnemiesOnWave = 3;

    [Space(20)]
    [Tooltip("�������� ����� ������� ������")]
    [SerializeField][Range(0.0f, 2f)] float _delayBetweenSpawnEnemies = 0.5f;

    [FormerlySerializedAs("_typeMainEnemyUnit")]
    [Header("��� ��������� ����� �����")]
    [Space(20)]
    [SerializeField] private EnemyType typeMainMainEnemyUnit;

    [Header("������ ����� � �������� � ����������� ������ ������ ������")]
    [SerializeField] private List<EnemySpawnConfig> _enemySpawnConfigs;

    #endregion Serialize fields

    #region Properties

    /// <summary>
    /// ������������� �������� ������ �� �����
    /// </summary>
    public int DefaultMaximumEnemiesOnScene => _defaultMaximumEnemiesOnScene;
    /// <summary>
    /// �������� �������� ������������� �������� ������ �� �����
    /// </summary>
    public int IncrementMaximumEnemiesOnScene => _incrementMaximumEnemiesOnScene;
    /// <summary>
    /// ����� �����, ������ ������� ���������� ���������� �������������� �������� ������ �� �����
    /// </summary>
    public int IncrementWaveMaximumEnemiesOnScene => _incrementWaveMaximumEnemiesOnScene;

    /// <summary>
    /// ������������� �������� ������ �� �����
    /// </summary>
    public int DefaultMaximumEnemiesOnWave => _defaultMaximumEnemiesOnWave;
    /// <summary>
    /// �������� �������� ������������� �������� ������ �� �����
    /// </summary>
    public int IncrementMaximumEnemiesOnWave => _incrementMaximumEnemiesOnWave;
    /// <summary>
    /// ����� �����, ����� ������ ������� ���������� ���������� �������������� �������� ������ �� �����
    /// </summary>
    public int IncrementWaveMaximumEnemiesOnWave => _incrementWaveMaximumEnemiesOnWave;

    /// <summary>
    /// �������� ����� ����������� ������
    /// </summary>
    public float DelayBetweenSpawnEnemies => _delayBetweenSpawnEnemies;

    /// <summary>
    /// ��� ��������� ����� �����
    /// </summary>
    public EnemyType MainEnemyType => typeMainMainEnemyUnit;
    /// <summary>
    /// ������ ����� � �������� � ����������� ������ ������ ������
    /// </summary>
    public List<EnemySpawnConfig> EnemySpawnConfigs => _enemySpawnConfigs;

    #endregion Properties
}