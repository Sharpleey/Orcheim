using UnityEngine;
using Zenject;

public class StartupController : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;

    [Inject]
    private void Construct(GameSceneManager gameSceneManager)
    {
        _gameSceneManager = gameSceneManager;
    }

    private void Start()
    {
        _gameSceneManager.SwitchToScene(HashSceneNameString.MAIN_MENU);
    }

    //[Header("Managers")]
    //[SerializeField] private GameSceneManager _gameSceneManager;
    //[SerializeField] private WaveManager _waveManager;
    //[SerializeField] private LootManager _lootManager;
    //[SerializeField] private AudioManager _audioManager;
    //[SerializeField] private PoolManager _poolManager;

    //// Список диспетчеров, который просматривается в цикле во время стартовой последовательности.
    //private List<IGameManager> _startSequence;

    //private void Awake()
    //{
    //    _startSequence = new List<IGameManager>();

    //    if(_gameSceneManager)
    //        _startSequence.Add(_gameSceneManager);
    //    if (_waveManager)
    //        _startSequence.Add(_waveManager);
    //    if (_lootManager)
    //        _startSequence.Add(_lootManager);
    //    if (_audioManager)
    //        _startSequence.Add(_audioManager);
    //    if (_audioManager)
    //        _startSequence.Add(_poolManager);

    //    StartCoroutine(StartupManagers());
    //}

    //private IEnumerator StartupManagers()
    //{
    //    foreach (IGameManager manager in _startSequence)
    //    {
    //        manager.Startup();
    //    }
    //    yield return null;

    //    int numModules = _startSequence.Count;
    //    int numReady = 0;

    //    // Продолжаем цикл, пока не начнут работать все диспетчеры.
    //    while (numReady < numModules)
    //    {
    //        int lastReady = numReady;
    //        numReady = 0;

    //        foreach (IGameManager manager in _startSequence)
    //        {
    //            if (manager.Status == ManagerStatus.Started)
    //                numReady++;
    //        }

    //        if (numReady > lastReady)
    //        {
    //            Debug.Log("Progress: " + numReady + "/" + numModules);
    //        }

    //        yield return new WaitForSeconds(0);

    //    }

    //    Debug.Log("All managers started up");

    //    _gameSceneManager.SwitchToScene(HashSceneNameString.MAIN_MENU);
    //}
}
