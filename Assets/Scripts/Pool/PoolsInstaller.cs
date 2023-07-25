using Zenject;

public class PoolsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindTracerEffectPool();
        BindPopupDamagePool();
        BindHitEffectPool();
        BindProjectileArrowPool();
    }

    private void BindTracerEffectPool()
    {
        TraccerEffectFactory factory = new TraccerEffectFactory(Container);
        Pool<TracerEffect> pool = new Pool<TracerEffect>(factory);

        Container.Bind<Pool<TracerEffect>>()
            .FromInstance(pool)
            .AsSingle()
            .NonLazy();
    }

    private void BindProjectileArrowPool()
    {
        ProjectileArrowFactory factory = new ProjectileArrowFactory(Container);
        Pool<ProjectileArrow> pool = new Pool<ProjectileArrow>(factory);

        Container.Bind<Pool<ProjectileArrow>>()
            .FromInstance(pool)
            .AsSingle()
            .NonLazy();
    }

    private void BindPopupDamagePool()
    {
        PopupDamageFactory factory = new PopupDamageFactory(Container);
        Pool<PopupDamage> pool = new Pool<PopupDamage>(factory);

        Container.Bind<Pool<PopupDamage>>()
            .FromInstance(pool)
            .AsSingle()
            .NonLazy();
    }

    private void BindHitEffectPool()
    {
        HitEffectFactory factory = new HitEffectFactory(Container);
        Pool<HitEffect> pool = new Pool<HitEffect>(factory);

        Container.Bind<Pool<HitEffect>>()
            .FromInstance(pool)
            .AsSingle()
            .NonLazy();
    }
}
