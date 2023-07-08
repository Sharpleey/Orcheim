using Zenject;

public class EnemyUnitFactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<EnemyUnitFactory>()
            .AsSingle()
            .NonLazy();
    }
}