using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    private void Start()
    {
        Messenger.AddListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, FindChestSpawnZones);
        Messenger.AddListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, ChestRespawn);
        Messenger.AddListener(GlobalGameEvent.WAVE_IS_OVER, ChestRespawn);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, FindChestSpawnZones);
        Messenger.RemoveListener(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, ChestRespawn);
        Messenger.RemoveListener(GlobalGameEvent.WAVE_IS_OVER, ChestRespawn);
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
}
