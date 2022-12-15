using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менеджер отвечает за параметры и данные игрока. Возрождение игрока на сцене
/// </summary>
public class PlayerManager : MonoBehaviour, IGameManager
{
    public static PlayerManager Instance { get; private set; }

    #region Serialize Fields

    [Header("Player parameters")]
    [SerializeField] [Min(150)] private int _maxHealth = 150;
    [SerializeField] [Min(0)] private int _maxArmor = 0;
    [SerializeField] [Min(2)] private float _maxSpeed = 4f;

    [Space(10)]
    [SerializeField] private GameObject _playerCharacterPrefab;

    #endregion Serialize Fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    /// <summary>
    /// Максимальное значение здоровья игркока
    /// </summary>
    public int MaxHealth
    {
        get => _maxHealth;
        set => Mathf.Clamp(value, 1, int.MaxValue);
    }

    /// <summary>
    /// Максимальное значение брони игрока
    /// </summary>
    public int MaxArmor
    {
        get => _maxArmor;
        private set => Mathf.Clamp(value, 0, int.MaxValue);
    }

    /// <summary>
    /// Максимальное значение скорости передвижения игрока
    /// </summary>
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => Mathf.Clamp(value, 0.1f, 32f);
    }

    /// <summary>
    /// Текущее значение здоровья игрока
    /// </summary>
    public int Health
    {
        get => _health;
        set => Mathf.Clamp(value, 0, _maxHealth);
    }

    private int _health;

    /// <summary>
    /// Актуальная броня игрока
    /// </summary>
    public int Armor
    {
        get => _armor;
        set => Mathf.Clamp(value, 0, _maxArmor);
    }

    private int _armor;

    /// <summary>
    /// Актуальная скорость игрока, скорость передвижения в PlayerCharacterController
    /// </summary>
    public float Speed
    {
        get => _speed;
        set => Mathf.Clamp(value, 0.1f, _maxSpeed);
    }

    private float _speed;

    #endregion Properties

    #region Private fields

    /// <summary>
    /// Зоны возрождения игрока на сцене
    /// </summary>
    private GameObject[] _playerSpawnZones;

    private GameObject _playerCharacter;
    private PlayerCharacterController _playerCharacterController;

    /// <summary>
    /// Список модификаторов атака игшрока
    /// </summary>
    private List<IModifier> _modifaers = new List<IModifier>();

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
        Startup();
    }

    #endregion Mono

    #region Private methods

    private void AddListeners()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        PlayerEventManager.OnPlayerDamaged.AddListener(TakeDamage);
        //GlobalGameEventManager.OnEnemyKilled.AddListener(UpdateCounterKills);
        GlobalGameEventManager.OnGameOver.AddListener(SetDefaultParameters);
    }

    /// <summary>
    /// Метод для принятия урона игроком
    /// </summary>
    /// <param name="damage"></param>
    private void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <=0)
        {
            PlayerEventManager.PlayerDead();
        }
    }

    /// <summary>
    /// Метод для установки стандартных параметров игрока
    /// </summary>
    private void SetDefaultParameters()
    {
        Debug.Log("Set default parameters for player");

        // Устанавливаем начальные значения характеристик (Здоровья и т.п.)
        _health = _maxHealth;
        _armor = _maxArmor;

        // Обнуляем список улучшений
        _modifaers = _modifaers = new List<IModifier>();
    }

    /// <summary>
    /// Метод поиска зон спавна игрока
    /// </summary>
    private void FindPlayerSpawnZonesOnScene()
    {
        Debug.Log("Find player spawn zones on scene...");

        _playerSpawnZones = GameObject.FindGameObjectsWithTag("PlayerSpawnZone");

        Debug.Log("Found: " + _playerSpawnZones.Length.ToString() + " zones");
    }

    /// <summary>
    /// Метод для возрождения игрока на случайном спавне
    /// </summary>
    private void PlayerRespawn()
    {
        Debug.Log("Player respawn");

        if (_playerSpawnZones == null || _playerSpawnZones.Length == 0)
        {
            Debug.Log("Player spawn zones not found on scene!");
            return;
        }

        int numSpawn = Random.Range(0, _playerSpawnZones.Length);
        GameObject spawn = _playerSpawnZones[numSpawn];

        _playerCharacter = GameObject.FindGameObjectWithTag("Player");

        if (_playerCharacter == null)
        {
            _playerCharacter = Instantiate(_playerCharacterPrefab);
            _playerCharacterController = GetComponent<PlayerCharacterController>();
        }

        _playerCharacter.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y + 1f, spawn.transform.position.z);
        _playerCharacter.transform.rotation = spawn.transform.rotation;
        //_playerCharacterController.Speed
    }

    #endregion Private methods

    #region Public methods

    public void Startup()
    {
        Debug.Log("Player manager starting...");

        SetDefaultParameters();

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    #endregion Public methods

    #region EventHandlers
    private void EventHandler_NewGame(GameMode gameMode)
    {
        SetDefaultParameters();
    }

    private void EventHandler_GameMapStarted()
    {
        FindPlayerSpawnZonesOnScene();
        PlayerRespawn();
    }
    #endregion
}
