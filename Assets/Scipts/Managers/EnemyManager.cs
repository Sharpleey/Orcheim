using UnityEngine;

/// <summary>
/// (P.S. In progress)
/// �������� ������ �������� �� ����� ��������� ����� ������ �� �����. ������������ ���-�� ������ �� �����.
/// </summary>
public class EnemyManager : MonoBehaviour, IGameManager
{
    public static EnemyManager Instance { get; private set; }

    #region Serialize fields

    /// <summary>
    /// ������������� �������� ������ �� �����
    /// </summary>
    [Header("������������� �������� ������ �� �����")]
    [SerializeField][Range(8, 80)] int _maximumEnemiesOnScene = 16;

    /// <summary>
    /// �������� �������� ������������� �������� ������ �� �����
    /// </summary>
    [Header("�������� �������� ������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 8)] int _incrementMaximumEnemiesOnScene = 2;

    /// <summary>
    /// ����� �����, ����� ������ ������� ���������� ���������� �������������� �������� ������ �� �����
    /// </summary>
    [Header("����� �����, ����� ������ ������� ���������� ���������� �������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 10)] int _incrementWaveMaximumEnemiesOnScene = 4;

    /// <summary>
    /// ������������� �������� ������ �� �����
    /// </summary>
    [Header("������������� �������� ������ �� �����")]
    [SerializeField][Range(16, 256)] int _maximumEnemiesOnWave = 32;

    /// <summary>
    /// �������� �������� ������������� �������� ������ �� �����
    /// </summary>
    [Header("�������� �������� ������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 8)] int _incrementMaximumEnemiesOnWave = 4;

    /// <summary>
    /// ����� �����, ����� ������ ������� ���������� ���������� �������������� �������� ������ �� �����
    /// </summary>
    [Header("����� �����, ����� ������ ������� ���������� ���������� �������������� �������� ������ �� �����")]
    [SerializeField][Range(1, 10)] int _incrementWaveMaximumEnemiesOnWave = 3;

    [SerializeField] private GameObject _prefabEnemy;

    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    /// <summary>
    /// ���-�� ���������� ������ �� �����
    /// </summary>
    public int EnemiesRemaining
    {
        get => _countEnemyOnScene + _countEnemyOnWavePool;
    }

    public int CountEnemyOnScene
    {
        get => _countEnemyOnScene;
        set => _countEnemyOnScene = Mathf.Clamp(value, 0, 128);
    }

    public int CountEnemyOnWavePool
    {
        get => _countEnemyOnWavePool;
        set => _countEnemyOnWavePool = Mathf.Clamp(value, 0, 4096);
    }

    #endregion Properties

    #region Private fields

    /// <summary>
    /// ���� ����������� ������
    /// </summary>
    private GameObject[] _enemySpawnZones;

    /// <summary>
    /// ���-�� ����������� �� �����
    /// </summary>
    private int _countEnemyOnScene;

    /// <summary>
    /// ���-�� ������ �� �����
    /// </summary>
    private int _countEnemyOnWavePool;

    private float _timer = 0;
    private bool _isSpawningEnemy;

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

    private void Update()
    {
        if (_isSpawningEnemy)
        {
            _timer += Time.deltaTime;

            // ������� ������ � ���������
            if (_timer > 0.8f)
            {
                // ������� ����������, ���� (���-�� ������ �� ����� ������ ������) � ���� (���� ������ �� ������)
                if (CountEnemyOnScene < _maximumEnemiesOnScene && CountEnemyOnWavePool != 0)
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
        GlobalGameEventManager.OnEnemyKilled.AddListener(CheckEnemiesRemaining);
        GlobalGameEventManager.OnGameOver.AddListener(EventHandler_GameOver);
    }

    private void SetDefaultParameters()
    {
        CountEnemyOnScene = 0;
        CountEnemyOnWavePool = 0;
    }

    private void SpawnEnemy()
    {
        // �������� ����� �� ���� ������
        CountEnemyOnWavePool -= 1;

        // ��������� ��� � ������ ������ �� �����
        CountEnemyOnScene += 1;

        // ���������� ����� �� ����� �� ����� �����������
        int numSpawnZone = Random.Range(0, _enemySpawnZones.Length);
        GameObject spawn = _enemySpawnZones[numSpawnZone];

        GameObject enemyOrc = Instantiate(_prefabEnemy, spawn.transform.position, Quaternion.identity);

        // ������ ��������� ����� �� �������������
        EnemyUnit enemyUnit = enemyOrc.GetComponent<EnemyUnit>();
        enemyUnit.DefaultState = StartStateType.Chasing;

        Debug.Log("Spawn enemy " + enemyUnit.GetType());
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

        _countEnemyOnScene = GameObject.FindGameObjectsWithTag("Enemy").Length;

        Debug.Log("Found: " + _countEnemyOnScene.ToString() + " enemies");
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
    /// ����� ���������� ������������ ���-�� ������ �� ����� � ����������� �� ������ �����
    /// </summary>
    /// <param name="wave">����� �����</param>
    private void UpdateValueMaximumEnemiesOnScene(int wave)
    {
        if (wave % _incrementWaveMaximumEnemiesOnScene == 0)
        {
            _maximumEnemiesOnScene += _incrementMaximumEnemiesOnScene;
        }
    }
    
    /// <summary>
    /// ����� ���������� ������������ ���-�� ������ �� ����� � ����������� �� ������ �����
    /// </summary>
    /// <param name="wave">����� �����</param>
    private void UpdateValueMaximumEnemiesOnWave(int wave)
    {
        if (wave % _incrementWaveMaximumEnemiesOnWave == 0)
        {
            _maximumEnemiesOnWave += _incrementMaximumEnemiesOnWave;
        }
    }

    /// <summary>
    /// ����� �������������� ��� �����������, ������� ����� ��������� �� ���������� �����
    /// </summary>
    private void FillPoolEnemies()
    {
        // TODO ����������� ����� pool objects
        // TODO ����������� � ������� ������ ������

        Debug.Log("Fill pool enemies");

        _countEnemyOnWavePool = _maximumEnemiesOnWave;

        //// ���� �������� ��� ����� ����� ������
        //for (int i = 0; i < _maximumEnemiesOnWave; i++)
        //{
        //    GameObject enemy = Instantiate(_prefabEnemy);
        //    enemy.SetActive(false);

        //    _enemyOnWavePool.Enqueue(enemy);
        //}
    }

    private void CheckEnemiesRemaining()
    {
        CountEnemyOnScene -= 1;

        SpawnEnemyEventManager.EnemiesRemaining(EnemiesRemaining);

        if (EnemiesRemaining == 0)
        {
            StopSpawnEnemies();

            WaveEventManager.WaveIsOver();
        }
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
        UpdateValueMaximumEnemiesOnScene(wave);
        UpdateValueMaximumEnemiesOnWave(wave);

        FillPoolEnemies();
    }

    private void EventHandler_WaveIsComing(int wave)
    {
        StartSpawnEnemies();

        SpawnEnemyEventManager.EnemiesRemaining(EnemiesRemaining);
    }

    private void EventHandler_GameOver()
    {
        StopSpawnEnemies();
        SetDefaultParameters();
    }
    #endregion
}
