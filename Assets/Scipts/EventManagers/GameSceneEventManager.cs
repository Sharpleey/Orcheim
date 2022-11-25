using UnityEngine.Events;

public class GameSceneEventManager
{
    public static readonly UnityEvent OnSceneLoadingStarted = new UnityEvent();

    public static readonly UnityEvent OnSceneStarded = new UnityEvent();

    public static readonly UnityEvent OnGameMapStarded = new UnityEvent();

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
