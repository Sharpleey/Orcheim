using UnityEngine.Events;

public class GameSceneEventManager
{
    public static readonly UnityEvent OnSceneLoadingStarted = new UnityEvent();

    public static readonly UnityEvent OnSceneStarded = new UnityEvent();

    public static readonly UnityEvent OnGameMapStarded = new UnityEvent();

    public static readonly UnityEvent<bool> OnGamePause = new UnityEvent<bool>();

    public static void GamePause(bool isPause)
    {
        OnGamePause.Invoke(isPause);
    }

    public static void SceneLoadingStarted()
    {
        OnSceneLoadingStarted.Invoke();
    }

    public static void SceneStarded()
    {
        OnSceneStarded.Invoke();
    }

    public static void GameMapStarted()
    {
        OnGameMapStarded.Invoke();
    }
}
