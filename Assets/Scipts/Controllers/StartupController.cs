using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameSceneManager _gameSceneManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private LootManager _lootManager;
    [SerializeField] private AudioManager _audioManager;

    // Список диспетчеров, который просматривается в цикле во время стартовой последовательности.
    private List<IGameManager> _startSequence;

    private void Awake()
    {
        _startSequence = new List<IGameManager>();

        if(_gameSceneManager)
            _startSequence.Add(_gameSceneManager);
        if (_playerManager)
            _startSequence.Add(_playerManager);
        if (_enemyManager)
            _startSequence.Add(_enemyManager);
        if (_waveManager)
            _startSequence.Add(_waveManager);
        if (_lootManager)
            _startSequence.Add(_lootManager);
        if (_audioManager)
            _startSequence.Add(_audioManager);

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

        _gameSceneManager.SwitchToScene(HashSceneNameString.MAIN_MENU);
    }
}
