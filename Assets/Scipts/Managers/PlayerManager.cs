using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менеджер отвечает за параметры и данные игрока. Возрождение игрока на сцене
/// </summary>
public class PlayerManager : PlayerUnit , IGameManager
{
    public static PlayerManager Instance { get; private set; }

    #region Serialize Fields

    [Header("Префаб игрока") , Space(10)]
    [SerializeField] private GameObject _playerCharacterPrefab;

    #endregion Serialize Fields

    #region Properties

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

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        AddListeners();

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    #endregion Mono

    #region Private methods

    private void AddListeners()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        //GlobalGameEventManager.OnEnemyKilled.AddListener(UpdateCounterKills);
        GlobalGameEventManager.OnGameOver.AddListener(SetDefaultParameters);
    }

    protected override void InitControllers()
    {
        throw new NotImplementedException(); //TODO
    }

    protected override void InitControllersParameters()
    {
        throw new NotImplementedException(); //TODO
    }

    /// <summary>
    /// Метод для установки стандартных параметров игрока
    /// </summary>
    private void SetDefaultParameters()
    {
        Debug.Log("Set default parameters for player");

        // Устанавливаем начальные значения характеристик (Здоровья и т.п.)
        Level = 1;

        Health.SetLevel(Level);
        Armor.SetLevel(Level);
        Damage.SetLevel(Level);
        MovementSpeed.SetLevel(Level);
        AttackSpeed.SetLevel(Level);

        Gold = 0;
        Experience = 0;

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
