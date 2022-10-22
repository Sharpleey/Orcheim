using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Убеждаемся, что различные диспетчеры существуют.
[RequireComponent(typeof(GameSceneManager))]

public class Managers : MonoBehaviour
{
    // Статические свойства, которыми остальной код пользуется для доступа к диспетчерам.

    // public static DataManager Data {get; private set;}
    // public static PlayerManager Player {get; private set;}
    // public static InventoryManager Inventory {get; private set;}
    public static GameSceneManager GameSceneManager { get; private set; }

    // Список диспетчеров, который просматривается в цикле во время стартовой последовательности.
    private List<IGameManager> _startSequence;

    void Awake() 
    {
        // Команда Unity для сохранения объекта между сценами.
        DontDestroyOnLoad(gameObject);

        // Data = GetComponent<DataManager>();
        // Player = GetComponent<PlayerManager>();
        // Inventory = GetComponent<InventoryManager>();
        GameSceneManager = GetComponent<GameSceneManager>();

        _startSequence = new List<IGameManager>();
        // _startSequence.Add(Player);
        // _startSequence.Add(Inventory);
        _startSequence.Add(GameSceneManager);
        // _startSequence.Add(Data);

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

        //Событие загрузки рассылается без параметров.
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }

}
