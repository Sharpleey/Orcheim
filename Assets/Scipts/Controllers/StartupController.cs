using UnityEngine;

public class StartupController : MonoBehaviour
{
    //[SerializeField] private Slider _progressBar;

    void Awake()
    {
        //Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }

    void OnDestroy()
    {
        //Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }
    //private void OnManagersProgress(int numReady, int numModules)
    //{
    //    float progress = (float)numReady / numModules;

    //    if (_progressBar)
    //        _progressBar.value = progress; // Обновляем ползунок данными о процессе загрузки.
    //}
    private void OnManagersStarted()
    {
        Messenger<string>.Broadcast(GameSceneManagerEvent.SWITCH_TO_SCENE, Scene.MAIN_MENU);
    }
}
