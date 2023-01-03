using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менеджер отвечает за параметры и данные игрока. Возрождение игрока на сцене
/// </summary>
public class PlayerManager : MonoBehaviour, IGameManager, IPlayerUnitParameters, IUsesAttackModifiers
{
    public static PlayerManager Instance { get; private set; }

    #region Serialize Fields

    [Header("Конфиг с параметрами")]
    [SerializeField] PlayerUnitConfig _playerUnitConfig;

    [Header("Префаб игрока") , Space(10)]
    [SerializeField] private GameObject _playerCharacterPrefab;

    #endregion Serialize Fields

    #region Properties

    private int _level;
    public int Level
    {
        get => _level;
        protected set => _level = Mathf.Clamp(value, 1, int.MaxValue);
    }

    public Health Health { get; private set; }
    public Armor Armor { get; private set; }
    public Damage Damage { get; private set; }
    public MovementSpeed MovementSpeed { get; private set; }
    public AttackSpeed AttackSpeed { get; private set; }
    public int Gold { get; private set; }
    public int Experience { get; private set; }

    public CriticalAttack CriticalAttack { get; private set; }
    public FlameAttack FlameAttack { get; private set; }
    public SlowAttack SlowAttack { get; private set; }
    public PenetrationProjectile PenetrationProjectile { get; private set; }

    public ManagerStatus Status { get; private set; }



    #endregion Properties

    #region Private fields

    /// <summary>
    /// Зоны возрождения игрока на сцене
    /// </summary>
    private GameObject[] _playerSpawnZones;

    private GameObject _playerCharacter;
    private PlayerCharacterController _playerCharacterController;

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
    private void AddParametersToUpgradeList()
    {
        //if (LootManager.Instance)
        //{
        //    LootManager.Instance.UpgradeParameters.Add(Health);
        //    LootManager.Instance.UpgradeParameters.Add(Armor);
        //    LootManager.Instance.UpgradeParameters.Add(Damage);
        //    LootManager.Instance.UpgradeParameters.Add(MovementSpeed);
        //    LootManager.Instance.UpgradeParameters.Add(AttackSpeed);
        //}
    }

    private void AddListeners()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        //GlobalGameEventManager.OnEnemyKilled.AddListener(UpdateCounterKills);
        GlobalGameEventManager.OnGameOver.AddListener(SetDefaultParameters);
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

        CriticalAttack = null;
        FlameAttack = null;
        SlowAttack = null;
        PenetrationProjectile = null;
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

        int numSpawn = UnityEngine.Random.Range(0, _playerSpawnZones.Length);
        GameObject spawn = _playerSpawnZones[numSpawn];

        _playerCharacter = GameObject.FindGameObjectWithTag("Player");

        if (_playerCharacter == null)
        {
            _playerCharacter = Instantiate(_playerCharacterPrefab);
            _playerCharacterController = GetComponent<PlayerCharacterController>();
        }

        _playerCharacter.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y + 1f, spawn.transform.position.z);
        _playerCharacter.transform.rotation = spawn.transform.rotation;
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

    public void InitParameters()
    {
        if(!_playerUnitConfig)
        {
            Debug.Log("Конфиг игрока не задан в инспекторе!");
            return;
        }

        Level = _playerUnitConfig.Level;

        Health = new Health(_playerUnitConfig.DefaultHealth, _playerUnitConfig.HealthIncreasePerLevel, _playerUnitConfig.HealthMaxLevel);
        Armor = new Armor(_playerUnitConfig.DefaultArmor, _playerUnitConfig.ArmorIncreasePerLevel, _playerUnitConfig.ArmorMaxLevel);
        Damage = new Damage(_playerUnitConfig.DefaultDamage, _playerUnitConfig.DamageIncreasePerLevel, maxLevel: _playerUnitConfig.DamageMaxLevel);
        MovementSpeed = new MovementSpeed(_playerUnitConfig.DefaultMovementSpeed, _playerUnitConfig.MovementSpeedIncreasePerLevel, _playerUnitConfig.MovementSpeedMaxLevel);
        AttackSpeed = new AttackSpeed(_playerUnitConfig.DefaultAttackSpeed, _playerUnitConfig.AttackSpeedIncreasePerLevel, _playerUnitConfig.AttackSpeedMaxLevel);
        Gold = _playerUnitConfig.Gold;
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
