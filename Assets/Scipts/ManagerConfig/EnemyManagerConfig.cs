using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���� ��� EnemyManager-a c ����������� ������ ������ � �.�.
/// </summary>
[CreateAssetMenu(menuName = "ManagerConfig/EnemyManagerConfig", fileName = "EnemyManagerConfig", order = 0)]
public class EnemyManagerConfig : ScriptableObject
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

    [Header("������ ��������� �����")]
    [Space(20)]
    [SerializeField] private EnemyUnit _prefabMainUnit;

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
    /// ������ ��������� ����� �����
    /// </summary>
    public EnemyUnit PrefabMainUnit => _prefabMainUnit;
    /// <summary>
    /// ������ ����� � �������� � ����������� ������ ������ ������
    /// </summary>
    public List<EnemySpawnConfig> EnemySpawnConfigs => _enemySpawnConfigs;

    #endregion Properties
}
