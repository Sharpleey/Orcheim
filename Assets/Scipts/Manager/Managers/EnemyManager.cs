using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (P.S. In progress)
/// �������� ������ �������� �� ����� ��������� ����� ������ �� �����. ������������ ���-�� ������ �� �����.
/// </summary>
public class EnemyManager : MonoBehaviour, IGameManager
{
    public static EnemyManager Instance { get; private set; }

    #region Serialize fields

    [Header("������ ���� � ����������� ���������")]
    [SerializeField] private EnemyManagerConfig _managerConfig;

    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

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

    /// <summary>
    /// ���� ����������� ������
    /// </summary>
    private GameObject[] _enemySpawnZones;

    private float _timer = 0;

    private bool _isSpawningEnemy;

    /// <summary>
    /// ������� ������
    /// </summary>
    private EnemyUnitFactory _enemyUnitFactory;

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
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        AddListeners();
    }

    private void Start()
    {
        _enemyUnitFactory = GetComponent<EnemyUnitFactory>();
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

    private void AddListeners()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarded);
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

    /// <summary>
    /// ����� ������� ���������� ���������� �� ���� � ��������� ����� ������
    /// </summary>
    private void SpawnEnemy()
    {
        // ���������� ����� �� ����� �� ����� �����������
        int numSpawnZone = Random.Range(0, _enemySpawnZones.Length);
        Vector3 spawnPosition = _enemySpawnZones[numSpawnZone].transform.position;

        // ��������� ��������� ��� ���������� �� ����
        int randomIndexEnemy = Random.Range(0, _wavePoolEnemies.Count);
        EnemyUnit enemyUnitPrefab = _wavePoolEnemies[randomIndexEnemy];
        _wavePoolEnemies.Remove(enemyUnitPrefab);

        // ������� ����� ������ ���������� �� ������ ������������ ���� �� ����
        EnemyUnit enemyUnit = _enemyUnitFactory.GetNewInstance(enemyUnitPrefab, spawnPosition);
        // ��������� ������ ���������� � ��� ����������� �� �����
        _enemiesOnScene.Add(enemyUnit);


    }

    /// <summary>
    /// ����� ������ �� ����� ���� ����������� ������
    /// </summary>
    private void FindEnemySpawnZonesOnScene()
    {
        Debug.Log("Find enemy spawn zones on scene...");

        _enemySpawnZones = GameObject.FindGameObjectsWithTag("EnemySpawnZone");

        Debug.Log("Found: " + _enemySpawnZones.Length.ToString() + " zones");
    }

    /// <summary>
    /// ����� ������ ������, ������� ��� ��������� �� ����� � ������ ����
    /// </summary>
    private void FindEnemiesOnScene()
    {
        Debug.Log("Find enemies on scene");

        GameObject[] emeniesOnScene = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i=0; i<emeniesOnScene.Length; i++)
        {
            _enemiesOnScene.Add(emeniesOnScene[i].GetComponent<EnemyUnit>());
        }

        Debug.Log($"Found: {_enemiesOnScene.Count} enemies");
    }

    private void StartSpawnEnemies()
    {
        Debug.Log("Starting spawning enemies");

        if (_enemySpawnZones == null)
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

                // ��������� �� � ���
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

    #region Public methods
    public void Startup()
    {
        Debug.Log("Spawn Enemy manager starting...");

        // any long-running startup tasks go here, and set status to 'Initializi ng' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    #endregion Public methods

    #region Event handlers
    private void EventHandler_GameMapStarded()
    {
        SetDefaultParameters();

        FindEnemySpawnZonesOnScene();

        FindEnemiesOnScene();
    }

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
