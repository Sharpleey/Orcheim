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

    /// <summary>
    /// ������� ��� ��������� ��� ������������� ����������
    /// </summary>
    public static class Event
    {
        #region Events for manager
        /// <summary>
        /// ������� ��������� ����� �������
        /// </summary>
        public const string TAKE_DAMAGE = "PLAYER_TAKE_DAMAGE";
        #endregion

        #region Events broadcast by manager
        /// <summary>
        /// ������� ������ ������
        /// </summary>
        public const string PLAYER_DEAD = "PLAYER_DEAD";
        #endregion
    }

    private void Awake()
    {
        Messenger<int>.AddListener(Event.TAKE_DAMAGE, TakeDamage);
        Messenger.AddListener(GameEvent.NEW_GAME_MODE_ORCCHEIM, SetDefaultParameters);
        Messenger.AddListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, FindPlayerSpawnZonesOnScene);
        Messenger.AddListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, PlayerRespawn);
        Messenger.AddListener(SpawnEnemyManager.Event.ENEMY_KILLED, UpdateCounterKills);
        Messenger.AddListener(GameEvent.GAME_OVER, SetDefaultParameters);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(Event.TAKE_DAMAGE, TakeDamage);
        Messenger.RemoveListener(GameEvent.NEW_GAME_MODE_ORCCHEIM, SetDefaultParameters);
        Messenger.RemoveListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, FindPlayerSpawnZonesOnScene);
        Messenger.RemoveListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, PlayerRespawn);
        Messenger.RemoveListener(SpawnEnemyManager.Event.ENEMY_KILLED, UpdateCounterKills);
        Messenger.RemoveListener(GameEvent.GAME_OVER, SetDefaultParameters);
    }

    public void Startup()
    {
        Debug.Log("Player manager starting...");

        SetDefaultParameters();

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <=0)
        {
            Messenger.Broadcast(Event.PLAYER_DEAD);
        }
    }

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

    private void FindPlayerSpawnZonesOnScene()
    {
        Debug.Log("Find player spawn zones on scene...");

        _playerSpawnZones = GameObject.FindGameObjectsWithTag("EnemySpawnZone");

        Debug.Log("Found: " + _playerSpawnZones.Length.ToString() + " zones");
    }

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
}
