using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxArmor = 0;
    [SerializeField] private float _maxSpeed = 4f;

    [SerializeField] private GameObject _playerCharacterPrefab;

    public ManagerStatus Status { get; private set; }

    private GameObject[] _playerSpawnZones;

    private int _health;
    private int _armor;

    private int _kills;

    private GameObject _playerCharacter;
    private List<IModifier> _modifaers = new List<IModifier>();

    private void Awake()
    {
        SetDefaultParameters();

        Messenger<int>.AddListener(GlobalGameEvent.PLAYER_DAMAGED, TakeDamage);
        Messenger.AddListener(GlobalGameEvent.NEW_GAME_MODE_ORCCHEIM, SetDefaultParameters);
        Messenger.AddListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, FindPlayerSpawnZonesOnScene);
        Messenger.AddListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, PlayerRespawn);
        Messenger.AddListener(GlobalGameEvent.ENEMY_KILLED, UpdateCounterKills);
        Messenger.AddListener(GlobalGameEvent.GAME_OVER, SetDefaultParameters);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GlobalGameEvent.PLAYER_DAMAGED, TakeDamage);
        Messenger.RemoveListener(GlobalGameEvent.NEW_GAME_MODE_ORCCHEIM, SetDefaultParameters);
        Messenger.RemoveListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, FindPlayerSpawnZonesOnScene);
        Messenger.RemoveListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, PlayerRespawn);
        Messenger.RemoveListener(GlobalGameEvent.ENEMY_KILLED, UpdateCounterKills);
        Messenger.RemoveListener(GlobalGameEvent.GAME_OVER, SetDefaultParameters);
    }

    public void Startup()
    {
        Debug.Log("Player manager starting...");

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <=0)
        {
            Messenger.Broadcast(GlobalGameEvent.PLAYER_DEAD);
        }
    }

    private void SetDefaultParameters()
    {
        Debug.Log("Set default parameters for player");

        // Устанавливаем начальные значения характеристик (Здоровья и т.п.)
        _health = _maxHealth;
        _armor = _maxArmor;

        _kills = 0;

        // Обнуляем список улучшений
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
            _playerCharacter = Instantiate(_playerCharacterPrefab);

        _playerCharacter.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y + 1f, spawn.transform.position.z);
        _playerCharacter.transform.rotation = spawn.transform.rotation;
    }

    private void UpdateCounterKills()
    {
        _kills++;
    }
}
