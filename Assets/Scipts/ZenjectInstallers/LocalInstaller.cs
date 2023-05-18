using UnityEngine;
using Zenject;

public class LocalInstaller : MonoInstaller
{
    [SerializeField] private Transform PlayerStartPoint;
    [SerializeField] private GameObject PlayerPrefab;

    public override void InstallBindings()
    {
        BindInstallerInterfaces();
        BindPlayer();
        BindEnemyUnitFactory();
    }

    private void BindEnemyUnitFactory()
    {
        Container
            .Bind<EnemyUnitFactory>()
            .To<EnemyUnitFactory>()
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
}