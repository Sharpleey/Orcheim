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
        PlayerUnit playerUnit = Container.InstantiatePrefabForComponent<PlayerUnit>(prefab, Vector3.zero, Quaternion.identity, null);

        Container
            .Bind<PlayerUnit>()
            .FromInstance(playerUnit)
            .AsSingle();
    }
}