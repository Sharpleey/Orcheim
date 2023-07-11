using UnityEngine;
using Zenject;

public class WaveManagerInstaller : MonoInstaller
{
    [SerializeField] private WaveManager _waveManager;

    public override void InstallBindings()
    {
        Container.Bind<WaveManager>()
            .FromInstance(_waveManager)
            .AsSingle()
            .NonLazy();
    }
}
