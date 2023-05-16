using System;
using System.Collections;
using System.Collections.Generic;
using Scipts.Factory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{
    #region Serialize fields

    [Header("������ ���� � ����������� ��������")]
    [SerializeField] private EnemyManagerConfig _managerConfig;
    
    [Header("����� ��������� ��������� ������ �� �����")]
    [SerializeField] private EnemySpawnMarker[] _enemySpawnMarkers;
    
    [Header("���� ������ ������ �� ����� ����")]
    [SerializeField] private EnemyWaveSpawnZone[] _enemyWaveSpawnZones;
    
    [Header("����� ������ ������")]
    [SerializeField] private Transform _playerSpawnPoint;

    #endregion Serialize fields
    
    #region Properties
    
    /// <summary>
    /// ���-�� ���������� ������ �� �����
    /// </summary>
    public int CountEnemiesRemaining
    {
        get => CountEnemyOnScene + CountEnemyOnWavePool;
    }

    /// <summary>
    /// ���-�� ����������� �� �����
    /// </summary>
    public int CountEnemyOnScene
    {
        get => _enemiesOnScene.Count;
    }

    /// <summary>
    /// ���-�� ������ �� �����
    /// </summary>
    public int CountEnemyOnWavePool
    {
        get => _wavePoolEnemies.Count;
    }
    
    /// <summary>
    /// ������� �������� ������������� ���-�� ������ �� �����
    /// </summary>
    public int CurrentMaximumEnemiesOnScene
    {
        get => _currentMaximumEnemiesOnScene;
        private set => _currentMaximumEnemiesOnScene = Mathf.Clamp(value, 0, 4096);
    }
    private int _currentMaximumEnemiesOnScene;

    /// <summary>
    /// ������� �������� ������������� ���-�� ������ �� �����
    /// </summary>
    public int CurrentMaximumEnemiesOnWave
    {
        get => _currentMaximumEnemiesOnWave;
        private set => _currentMaximumEnemiesOnWave = Mathf.Clamp(value, 0, 4096);
    }
    private int _currentMaximumEnemiesOnWave;

    #endregion Properties
    
    #region Private fields

    private float _timer = 0;
    private bool _isSpawningEnemy;
    
    /// <summary>
    /// ������� ������
    /// </summary>
    private IEnemyFactory _enemyFactory;

    /// <summary>
    /// ��� (������) ������ �����
    /// </summary>
    private List<EnemyUnit> _wavePoolEnemies;
    /// <summary>
    /// ��� (������) ������ �� �����
    /// </summary>
    private List<EnemyUnit> _enemiesOnScene;
    
    #endregion Private fields

    #region Mono

    private void Awake()
    {
        AddListeners();
    }

    private void Start()
    {
        SetDefaultParameters();
        SpawnEnemyOnMarkers();
    }

    private void Update()
    {
        if (_isSpawningEnemy)
        {
            _timer += Time.deltaTime;

            // ������� ������ � ���������
            if (_timer > _managerConfig.DelayBetweenSpawnEnemies)
            {
                // ������� ����������, ���� (���-�� ������ �� ����� ������ ������) � ���� (���� ������ �� ������)
                if (CountEnemyOnScene < CurrentMaximumEnemiesOnScene && CountEnemyOnWavePool != 0)
                    SpawnEnemy();

                _timer = 0;
            }
        }
    }

    #endregion Mono
    
    #region Private methods

    [Inject]
    private void Construct(IEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }
    
    private void AddListeners()
    {
        WaveEventManager.OnPreparingForWave.AddListener(EventHandler_PreparingForWave);
        WaveEventManager.OnWaveIsComing.AddListener(EventHandler_WaveIsComing);
        GlobalGameEventManager.OnEnemyKilled.AddListener(EventHandler_OnEnemyKilled);
        GlobalGameEventManager.OnGameOver.AddListener(EventHandler_GameOver);
    }

    private void SetDefaultParameters()
    {
        CurrentMaximumEnemiesOnScene = 0;
        CurrentMaximumEnemiesOnWave = 0;

        _wavePoolEnemies = new List<EnemyUnit>();
        _enemiesOnScene = new List<EnemyUnit>();
    }

    private void SpawnEnemyOnMarkers()
    {
        foreach (EnemySpawnMarker enemySpawnMarker in _enemySpawnMarkers)
        {
            EnemyUnit enemyUnit = _enemyFactory.Create(enemySpawnMarker.EnemyType, enemySpawnMarker.StartStateType, enemySpawnMarker.transform);
            _enemiesOnScene.Add(enemyUnit);
        }
    }
    
    /// <summary>
    /// ����� ������� ���������� ���������� �� ���� � ��������� ����� ������
    /// </summary>
    private void SpawnEnemy()
    {
        // ���������� ����� �� ����� �� ����� �����������
        int numSpawnZone = Random.Range(0, _enemyWaveSpawnZones.Length);
        Transform spawnTransform = _enemyWaveSpawnZones[numSpawnZone].transform;

        // ��������� ��������� ��� ���������� �� ����
        int randomIndexEnemy = Random.Range(0, _wavePoolEnemies.Count);
        EnemyUnit enemyUnitPrefab = _wavePoolEnemies[randomIndexEnemy];
        _wavePoolEnemies.Remove(enemyUnitPrefab);

        // ������� ����� ������ ���������� �� ������ ������������ ���� �� ����
        EnemyUnit enemyUnit = _enemyFactory.GetNewInstance(enemyUnitPrefab, StartStateType.Chasing, spawnTransform);
        // ��������� ������ ���������� � ��� ����������� �� �����
        _enemiesOnScene.Add(enemyUnit);
    }

    private void StartSpawnEnemies()
    {
        Debug.Log("Starting spawning enemies");

        if (_enemyWaveSpawnZones == null)
        {
            Debug.Log("Enemy spawn zones not found on scene!");
            return;
        }

        _isSpawningEnemy = true;
    }

    private void StopSpawnEnemies()
    {
        _isSpawningEnemy = false;
    }

    /// <summary>
    /// ����� ���������� � ���������� �������� ������������� ���-�� ������ �� ����� � ����������� �� ������ �����
    /// </summary>
    /// <param name="wave">����� �����</param>
    /// <returns>�������� ������������� ���-�� ������ �� �����</returns>
    private void UpdateCurrentMaximumEnemiesOnScene(int wave)
    {
        CurrentMaximumEnemiesOnScene = _managerConfig.DefaultMaximumEnemiesOnScene + (wave / _managerConfig.IncrementWaveMaximumEnemiesOnScene) * _managerConfig.IncrementMaximumEnemiesOnScene;
    }
    
    private void UpdateCurrentMaximumEnemiesOnWave(int wave)
    {
        CurrentMaximumEnemiesOnWave = _managerConfig.DefaultMaximumEnemiesOnWave + (wave / _managerConfig.IncrementWaveMaximumEnemiesOnWave) * _managerConfig.IncrementMaximumEnemiesOnWave;
    }

    /// <summary>
    /// ����� �������������� ��� �����������, ������� ����� ��������� �� ���������� �����
    /// </summary>
    private void FillPoolEnemies(int wave)
    {
        Debug.Log("Fill pool enemies...");

        // ���-�� ������ ����������� � ���� �� ������ �����
        int countSpecialUnits = 0;

        foreach (EnemySpawnConfig enemySpawnConfig in _managerConfig.EnemySpawnConfigs)
        {
            // ���� ������ �����, ��� ������ ����� ��������� ������� ���� �����
            if(wave >= enemySpawnConfig.SpawnWave)
            {
                // ���������� ������������ ���-�� ����������� ������� ���� �� ������� �����
                int maxCountEnemyOnCurrentWave = (wave / enemySpawnConfig.IncrementWaveCountEnemy) * enemySpawnConfig.IncrementCountEnemy;
                // �������� ��������� �� �����, �� ������ �����
                int countEnemyOnCurrentWave = Random.Range(0, maxCountEnemyOnCurrentWave);

                // ��������� � ���
                for (int i = 0; i < countEnemyOnCurrentWave; i++)
                    _wavePoolEnemies.Add(enemySpawnConfig.PrefabUnit);

                countSpecialUnits += countEnemyOnCurrentWave;
            }
        }

        // ���������� ��� ������� ����� �����
        for (int i = 0; i < CurrentMaximumEnemiesOnWave - countSpecialUnits; i++)
            _wavePoolEnemies.Add(_managerConfig.PrefabMainUnit);

        Debug.Log("Fill pool enemies (DONE)");
    }

    #endregion Private methods
    
    #region Event handlers

    private void EventHandler_PreparingForWave(int wave)
    {
        // ��������� ������������ ���-�� ������ �� ����� �� ������ �����
        UpdateCurrentMaximumEnemiesOnScene(wave);

        // ��������� ������������ ���-�� ������ �� ������ �����
        UpdateCurrentMaximumEnemiesOnWave(wave);

        FillPoolEnemies(wave);
    }

    private void EventHandler_WaveIsComing(int wave)
    {
        // ������ �� ����� ������ ��������� �� ������������� ������
        foreach (EnemyUnit enemy in _enemiesOnScene)
            enemy.SetState<ChasingState>();

        // ��������� ����� ������ �� ����
        StartSpawnEnemies();

        // ��������� ������� ���������� ���-�� ���������� ������
        EnemyEventManager.UpdateCountEnemiesRemaining(CountEnemiesRemaining);
    }

    private void EventHandler_OnEnemyKilled(EnemyUnit enemyUnit)
    {
        // ������� ���������� �� ���� ������ �� �����
        _enemiesOnScene.Remove(enemyUnit);

        // ��������� ������� ���������� ���-�� ���������� ������
        EnemyEventManager.UpdateCountEnemiesRemaining(CountEnemiesRemaining);

        // ���� ���-�� ���������� ������
        if (CountEnemiesRemaining == 0)
        {
            // ������������� ����� ������
            StopSpawnEnemies();

            // ��������� ������� � ���, ��� ����� �� ����� �����������
            EnemyEventManager.EnemiesOver();
        }
    }

    private void EventHandler_GameOver()
    {
        StopSpawnEnemies();
        SetDefaultParameters();
    }
    #endregion
}
