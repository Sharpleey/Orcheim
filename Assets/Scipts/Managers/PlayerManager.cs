using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
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
    /// ������������ �������� �������� �������
    /// </summary>
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if (value <= 0)
            {
                _maxHealth = 1;
                return;
            }
            _maxHealth = value;
        }
    }

    /// <summary>
    /// ������������ �������� ����� ������
    /// </summary>
    public int MaxArmor
    {
        get
        {
            return _maxArmor;
        }
        private set
        {
            if (value < 0)
            {
                _maxArmor = 0;
                return;
            }
            _maxArmor = value;
        }
    }

    /// <summary>
    /// ������������ �������� �������� ������������ ������
    /// </summary>
    public float MaxSpeed
    {
        get
        {
            return _maxSpeed;
        }
        set
        {
            if (value < 0.1f)
            {
                _maxSpeed = 0.1f;
                return;
            }
            float speed = UnityEngine.Random.Range(value - 0.4f, value + 0.4f);
            _maxSpeed = speed;
        }
    }

    /// <summary>
    /// ������� �������� �������� ������
    /// </summary>
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value < 0)
            {
                _health = 0;
                return;
            }
            if (value > _maxHealth)
            {
                _health = _maxHealth;
                return;
            }
            _health = value;
        }
    }

    /// <summary>
    /// ���������� ����� ������
    /// </summary>
    public int Armor
    {
        get
        {
            return _armor;
        }
        set
        {
            if (value < 0)
            {
                _armor = 0;
                return;
            }
            if (value > _maxHealth)
            {
                _armor = _maxArmor;
                return;
            }
            _armor = value;
        }
    }

    /// <summary>
    /// ���������� �������� ������, �������� ������������ � PlayerCharacterController
    /// </summary>
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (value < 0.1f)
            {
                _speed = 0.1f;
                return;
            }
            _speed = value;
        }
    }

    #endregion Properties

    private GameObject[] _playerSpawnZones;

    private int _health;
    private int _armor;
    private float _speed;

    private int _kills;

    private GameObject _playerCharacter;
    private PlayerCharacterController _playerCharacterController;

    private List<IModifier> _modifaers = new List<IModifier>();

    private void Awake()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        PlayerEventManager.OnPlayerDamaged.AddListener(TakeDamage);
        GlobalGameEventManager.OnEnemyKilled.AddListener(UpdateCounterKills);
        GlobalGameEventManager.OnGameOver.AddListener(SetDefaultParameters);
    }

    public void Startup()
    {
        Debug.Log("Player manager starting...");

        SetDefaultParameters();

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    /// <summary>
    /// ����� ��� �������� ����� �������
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
    /// ����� ��� ��������� ����������� ���������� ������
    /// </summary>
    private void SetDefaultParameters()
    {
        Debug.Log("Set default parameters for player");

        // ������������� ��������� �������� ������������� (�������� � �.�.)
        _health = _maxHealth;
        _armor = _maxArmor;

        _kills = 0;

        // �������� ������ ���������
        _modifaers = _modifaers = new List<IModifier>();
    }

    /// <summary>
    /// ����� ������ ��� ������ ������
    /// </summary>
    private void FindPlayerSpawnZonesOnScene()
    {
        Debug.Log("Find player spawn zones on scene...");

        _playerSpawnZones = GameObject.FindGameObjectsWithTag("EnemySpawnZone");

        Debug.Log("Found: " + _playerSpawnZones.Length.ToString() + " zones");
    }

    /// <summary>
    /// ����� ��� ����������� ������ �� ��������� ������
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

    private void UpdateCounterKills()
    {
        _kills++;
    }

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
