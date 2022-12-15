using UnityEngine;

public class LootManager : MonoBehaviour, IGameManager
{
    public static LootManager Instance { get; private set; }

    #region Serialize fields
    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    #endregion Properties

    #region Private fields

    private GameObject[] _chestSpawnZones;

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

    #endregion Mono

    #region Private methods

    private void AddListeners()
    {

        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        WaveEventManager.OnWaveIsOver.AddListener(ChestRespawn);
    }

    private void FindChestSpawnZones()
    {
        Debug.Log("Find chest spawn zones...");

        _chestSpawnZones = GameObject.FindGameObjectsWithTag("ChestSpawnZone");

        Debug.Log("Found: " + _chestSpawnZones.Length.ToString() + " zones");
    }

    private void ChestRespawn()
    {
        Debug.Log("Spawn chest");
    }

    #endregion Private methods

    #region Public methods

    public void Startup()
    {
        Debug.Log("Loot manager starting...");

        Status = ManagerStatus.Started;
    }

    #endregion Public methods

    #region Event handlers
    private void EventHandler_NewGame(GameMode gameMode)
    {

    }

    private void EventHandler_GameMapStarted()
    {
        FindChestSpawnZones();
        ChestRespawn();
    }
    #endregion
}
