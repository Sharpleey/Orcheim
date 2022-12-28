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

    /// <summary>
    /// Здоровье игрока
    /// </summary>
    public Health Health { get; private set; }
    public Armor Armor { get; private set; }
    public Damage Damage { get; private set; }
    public MovementSpeed MovementSpeed { get; private set; }
    public AttackSpeed AttackSpeed { get; private set; }

    public ManagerStatus Status { get; private set; }



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
        InitParameters();
    }
    #endregion Mono

    #region Private methods

    private void InitParameters()
    {
        Health = new Health(150, 25);
        Armor = new Armor(0, 2);
        Damage = new Damage(25, 5, DamageType.Physical, false);
        MovementSpeed = new MovementSpeed(360, 10);
        AttackSpeed = new AttackSpeed(100, 10);
    }

    private void AddParametersToUpgradeList()
    {
        if (LootManager.Instance)
        {
            LootManager.Instance.UpgradeParameters.Add(Health);
            LootManager.Instance.UpgradeParameters.Add(Armor);
            LootManager.Instance.UpgradeParameters.Add(Damage);
            LootManager.Instance.UpgradeParameters.Add(MovementSpeed);
            LootManager.Instance.UpgradeParameters.Add(AttackSpeed);
        }
    }

    private void AddListeners()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        //GlobalGameEventManager.OnEnemyKilled.AddListener(UpdateCounterKills);
        GlobalGameEventManager.OnGameOver.AddListener(SetDefaultParameters);
    }

    /// <summary>
    /// Метод для принятия урона игроком
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage, DamageType damageType, bool isArmorIgnore)
    {
        int damageValue = damage;

        if (!isArmorIgnore)
        {
            // Значение уменьшения урона
            float increaseDamage = 1.0f - (Armor.Actual / (100.0f + Armor.Actual));

            // Уменьшенный урон за счет брони
            damageValue = (int)(damageValue * increaseDamage);
        }
       

        Health.Actual -= damageValue;

        PlayerEventManager.PlayerDamaged(damageValue);

        if (Health.Actual <= 0)
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
        Health.SetLevel(1);
        Armor.SetLevel(1);
        Damage.SetLevel(1);
        MovementSpeed.SetLevel(1);
        AttackSpeed.SetLevel(1);

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
        AddParametersToUpgradeList();

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
