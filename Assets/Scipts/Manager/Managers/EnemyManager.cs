using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (P.S. In progress)
/// Менеджер врагов отвечает за спавн различных типов врагов на сцене. Контролирует кол-во врагов на сцене.
/// </summary>
public class EnemyManager : MonoBehaviour, IGameManager
{
    public static EnemyManager Instance { get; private set; }

    #region Serialize fields

    [Header("Конфиг файл с настройками менеджера")]
    [SerializeField] private EnemyManagerConfig _managerConfig;

    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    /// <summary>
    /// Кол-во оставшихся врагов на волне
    /// </summary>
    public int CountEnemiesRemaining
    {
        get => CountEnemyOnScene + CountEnemyOnWavePool;
    }

    /// <summary>
    /// Кол-во противников на сцене
    /// </summary>
    public int CountEnemyOnScene
    {
        get => _enemiesOnScene.Count;
    }

    /// <summary>
    /// Кол-во врагов на волне
    /// </summary>
    public int CountEnemyOnWavePool
    {
        get => _wavePoolEnemies.Count;
    }
    
    /// <summary>
    /// Текущее значение максимального вол-ва врагов на сцене
    /// </summary>
    public int CurrentMaximumEnemiesOnScene
    {
        get => _currentMaximumEnemiesOnScene;
        private set => _currentMaximumEnemiesOnScene = Mathf.Clamp(value, 0, 4096);
    }
    private int _currentMaximumEnemiesOnScene;

    /// <summary>
    /// Текущее значение максимального вол-ва врагов на волне
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
    /// Зоны возрождения врагов
    /// </summary>
    private GameObject[] _enemySpawnZones;

    private float _timer = 0;

    private bool _isSpawningEnemy;

    /// <summary>
    /// Фабрика юнитов
    /// </summary>
    private EnemyUnitFactory _enemyUnitFactory;

    /// <summary>
    /// Пул (список) врагов волны
    /// </summary>
    private List<EnemyUnit> _wavePoolEnemies;
    /// <summary>
    /// Пул (список) врагов на сцене
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

            // Спавним врагов с задержкой
            if (_timer > _managerConfig.DelayBetweenSpawnEnemies)
            {
                // Спавним противника, если (кол-во врагов на сцене меньше лимита) и если (пулл врагов не пустой)
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
    /// Метод спавнит случайного противника из пула в случайной точке спавна
    /// </summary>
    private void SpawnEnemy()
    {
        // Возрождаем врага на одной из точек возрождения
        int numSpawnZone = Random.Range(0, _enemySpawnZones.Length);
        Vector3 spawnPosition = _enemySpawnZones[numSpawnZone].transform.position;

        // Извлекаем случайный тип противника из пула
        int randomIndexEnemy = Random.Range(0, _wavePoolEnemies.Count);
        EnemyUnit enemyUnitPrefab = _wavePoolEnemies[randomIndexEnemy];
        _wavePoolEnemies.Remove(enemyUnitPrefab);

        // Создаем новый объект противника на основе извлеченного типа из пула
        EnemyUnit enemyUnit = _enemyUnitFactory.GetNewInstance(enemyUnitPrefab, spawnPosition);
        // Добавляем нового противника в пул противников на сцене
        _enemiesOnScene.Add(enemyUnit);


    }

    /// <summary>
    /// Метод поиска на сцене мест возрождения врагов
    /// </summary>
    private void FindEnemySpawnZonesOnScene()
    {
        Debug.Log("Find enemy spawn zones on scene...");

        _enemySpawnZones = GameObject.FindGameObjectsWithTag("EnemySpawnZone");

        Debug.Log("Found: " + _enemySpawnZones.Length.ToString() + " zones");
    }

    /// <summary>
    /// Метод поиска врагов, которые уже находятся на сцене в начале игры
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
    /// Метод поределяет и возвращает значение максимального кол-ва врагов на сцене в зависимости от номера волны
    /// </summary>
    /// <param name="wave">Номер волны</param>
    /// <returns>Значение максимального кол-ва врагов на сцене</returns>
    private void UpdateCurrentMaximumEnemiesOnScene(int wave)
    {
        CurrentMaximumEnemiesOnScene = _managerConfig.DefaultMaximumEnemiesOnScene + (wave / _managerConfig.IncrementWaveMaximumEnemiesOnScene) * _managerConfig.IncrementMaximumEnemiesOnScene;
    }

    
    private void UpdateCurrentMaximumEnemiesOnWave(int wave)
    {
        CurrentMaximumEnemiesOnWave = _managerConfig.DefaultMaximumEnemiesOnWave + (wave / _managerConfig.IncrementWaveMaximumEnemiesOnWave) * _managerConfig.IncrementMaximumEnemiesOnWave;
    }

    /// <summary>
    /// Метод подготовливает пул противников, которые будут спавнится на конкретной волне
    /// </summary>
    private void FillPoolEnemies(int wave)
    {
        Debug.Log("Fill pool enemies...");

        // Кол-во особых противников в пуле на данной волне
        int countSpecialUnits = 0;

        foreach (EnemySpawnConfig enemySpawnConfig in _managerConfig.EnemySpawnConfigs)
        {
            // Если данная волна, это первая волна появления данного типа врага
            if(wave >= enemySpawnConfig.SpawnWave)
            {
                // Определяем максимальное кол-во противников данного типа на текущей волне
                int maxCountEnemyOnCurrentWave = (wave / enemySpawnConfig.IncrementWaveCountEnemy) * enemySpawnConfig.IncrementCountEnemy;
                // Рандомим актульное их число, на данной волне
                int countEnemyOnCurrentWave = Random.Range(0, maxCountEnemyOnCurrentWave);

                // Добавляем из в пул
                for (int i = 0; i < countEnemyOnCurrentWave; i++)
                    _wavePoolEnemies.Add(enemySpawnConfig.PrefabUnit);

                countSpecialUnits += countEnemyOnCurrentWave;
            }
        }

        // Дозаполяем пул обычным типов врага
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
        // Обновляем максимальное кол-во врагов на сцене на данной волне
        UpdateCurrentMaximumEnemiesOnScene(wave);

        // Обновляем максимальное кол-во врагов на данной волне
        UpdateCurrentMaximumEnemiesOnWave(wave);

        FillPoolEnemies(wave);
    }

    private void EventHandler_WaveIsComing(int wave)
    {
        // Врагам на сцене меняем состояние на преследование игрока
        foreach (EnemyUnit enemy in _enemiesOnScene)
            enemy.SetState<ChasingState>();

        // Запускаем спавн врагов из пула
        StartSpawnEnemies();

        // Рассылаем события обновления кол-ва оставшихся врагов
        EnemyEventManager.UpdateCountEnemiesRemaining(CountEnemiesRemaining);
    }

    private void EventHandler_OnEnemyKilled(EnemyUnit enemyUnit)
    {
        // Удаляем противника из пула врагов на сцене
        _enemiesOnScene.Remove(enemyUnit);

        // Рассылаем события обновления кол-ва оставшихся врагов
        EnemyEventManager.UpdateCountEnemiesRemaining(CountEnemiesRemaining);

        // Если кол-во оставшихся врагов
        if (CountEnemiesRemaining == 0)
        {
            // Останавливаем спавн врагов
            StopSpawnEnemies();

            // Рассылаем событие о том, что врагм на волне закончились
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
