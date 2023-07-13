using UnityEngine;
using Zenject;

public class LootManagerInstaller : MonoInstaller
{
    [SerializeField] private LootManager _lootManager;

    public override void InstallBindings()
    {
        Container.Bind<LootManager>()
            .FromInstance(_lootManager)
            .AsSingle()
            .NonLazy();
    }
}
