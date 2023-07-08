using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [Header("Точка спавна игрока")]
    [SerializeField] private Transform _playerSpawnPoint;

    public override void InstallBindings()
    {
        var prefab = Resources.Load<PlayerUnit>(HashResourcesPath.PLAYER_PATH);
        Player player = Container.InstantiatePrefabForComponent<Player>(prefab, _playerSpawnPoint.position, _playerSpawnPoint.rotation, null);

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