using UnityEngine;
using Zenject;

public class GameSceneManagerInstaller : MonoInstaller
{
    [SerializeField] private GameSceneManager _gameSceneManager;

    public override void InstallBindings()
    {
        Container
             .Bind<GameSceneManager>()
             .FromInstance(_gameSceneManager)
             .AsSingle()
             .NonLazy();

    }
}
