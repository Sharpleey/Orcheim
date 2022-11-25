using UnityEngine;

public class LootManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    private GameObject[] _chestSpawnZones;

    public void Startup()
    {
        Debug.Log("Loot manager starting...");

        Status = ManagerStatus.Started;
    }
    
    private void Awake()
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
