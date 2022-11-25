using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Убеждаемся, что различные диспетчеры существуют.
[RequireComponent(typeof(GameSceneManager))]
[RequireComponent(typeof(WaveManager))]
[RequireComponent(typeof(SpawnEnemyManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(LootManager))]
[RequireComponent(typeof(AudioManager))]


public class Managers : MonoBehaviour
{
    // Статические свойства, которыми остальной код пользуется для доступа к диспетчерам.
   
    public static GameSceneManager GameSceneManager { get; private set; }
    public static WaveManager WaveManager { get; private set; }
    public static SpawnEnemyManager SpawnEnemyManager { get; private set; }
    public static PlayerManager PlayerManager { get; private set; }
    public static LootManager LootManager { get; private set; }
    public static AudioManager AudioManager { get; private set; }

    // Список диспетчеров, который просматривается в цикле во время стартовой последовательности.
    private List<IGameManager> _startSequence;

    void Awake() 
    {
        // Команда Unity для сохранения объекта между сценами.
        DontDestroyOnLoad(gameObject);

        GameSceneManager = GetComponent<GameSceneManager>();
        WaveManager = GetComponent<WaveManager>();
        SpawnEnemyManager = GetComponent<SpawnEnemyManager>();
        PlayerManager = GetComponent<PlayerManager>();
        LootManager = GetComponent<LootManager>();
        AudioManager = GetComponent<AudioManager>();


        _startSequence = new List<IGameManager>();
        _startSequence.Add(GameSceneManager);
        _startSequence.Add(WaveManager);
        _startSequence.Add(SpawnEnemyManager);
        _startSequence.Add(PlayerManager);
        _startSequence.Add(LootManager);
        _startSequence.Add(AudioManager);

        StartCoroutine(StartupManagers()); 
    }

    private IEnumerator StartupManagers() 
    {   
        foreach (IGameManager manager in _startSequence) 
        {
            manager.Startup();
        }
        yield return null;
        
        int numModules = _startSequence.Count;
        int numReady = 0;

        // Продолжаем цикл, пока не начнут работать все диспетчеры.
        while (numReady < numModules) 
        { 
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence) 
            {
                if (manager.Status == ManagerStatus.Started) 
                    numReady++;
            }

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);

                // Событие загрузки рассылается вместе с относящимися к нему данными.
                //Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
            
            yield return new WaitForSeconds(0);

        }
        
        Debug.Log("All managers started up");

        GameSceneManager.SwitchToScene(SceneName.MAIN_MENU);
    }

}
