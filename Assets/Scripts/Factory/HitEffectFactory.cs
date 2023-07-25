using UnityEngine;
using Zenject;

public class HitEffectFactory : BaseFactory<HitEffect>
{
    public HitEffectFactory(DiContainer diContainer) : base(diContainer)
    {
    }

    protected override void LoadResources()
    {
        _prefab = Resources.Load<HitEffect>(HashResourcesPath.VFX_HIT);
    }
}
