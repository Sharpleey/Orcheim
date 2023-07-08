using UnityEngine;
using Zenject;

public class GameSceneManagerInstaller : MonoInstaller
{
    [SerializeField] private GameSceneManagerConfig _gameSceneManagerConfig;
    [SerializeField] private LoadingScreenController _loadingScreen;

    public override void InstallBindings()
    {
        GameSceneManager gameSceneManager = new GameSceneManager(_gameSceneManagerConfig, _loadingScreen);

        Container
             .Bind<GameSceneManager>()
             .FromInstance(gameSceneManager)
             .AsSingle()
             .NonLazy();

    }
}
