using UnityEngine.Events;

public class GameSceneEventManager
{
    public static readonly UnityEvent<bool> OnGamePause = new UnityEvent<bool>();

    public static void GamePause(bool isPause)
    {
        OnGamePause.Invoke(isPause);
    }
}
