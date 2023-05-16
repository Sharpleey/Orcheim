using Scipts.Factory;
using UnityEngine;
using Zenject;

public class LocalInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private Transform PlayerStartPoint;
    [SerializeField] private GameObject PlayerPrefab;
    
    /*[Space(5)]
    [SerializeField] private EnemySpawnMarker[] _enemySpawnMarkers;*/
    
    public override void InstallBindings()
    {
        BindInstallerInterfaces();
        BindPlayer();
        BindEnemyFactory();
    }

    private void BindEnemyFactory()
    {
        Container
            .Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();
    }

    private void BindInstallerInterfaces()
    {
        Container
            .BindInterfacesTo<LocalInstaller>()
            .FromInstance(this)
            .AsSingle();
    }

    private void BindPlayer()
    {
        PlayerUnit playerUnit = Container.InstantiatePrefabForComponent<PlayerUnit>(PlayerPrefab, PlayerStartPoint.position, Quaternion.identity, null); //TODO

        Container
            .Bind<PlayerUnit>()
            .FromInstance(playerUnit)
            .AsSingle();
    }

    public void Initialize()
    {
        /*IEnemyFactory enemyFactory = Container.Resolve<IEnemyFactory>();
        enemyFactory.Load();

        foreach (EnemySpawnMarker enemySpawnMarker in _enemySpawnMarkers)
        {
            enemyFactory.Create(enemySpawnMarker.EnemyType, enemySpawnMarker.StartStateType, enemySpawnMarker.SpawnPosition);
        }*/
    }
}