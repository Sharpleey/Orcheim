using UnityEngine;
using Zenject;

public class LocalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEnemyUnitFactory();
        BindPlayer();
    }

    private void BindEnemyUnitFactory()
    {
        Container
            .Bind<EnemyUnitFactory>()
            .To<EnemyUnitFactory>()
            .AsSingle();
    }

    private void BindPlayer()
    {
        var prefab = Resources.Load<PlayerUnit>(HashResourcesPath.PLAYER_PATH);
        Player player = Container.InstantiatePrefabForComponent<Player>(prefab, Vector3.zero, Quaternion.identity, null);

        Container
            .Bind<Player>()
            .FromInstance(player)
            .AsSingle();

        Container
            .Bind<PlayerUnit>()
            .FromInstance(player)
            .AsSingle();
    }
}