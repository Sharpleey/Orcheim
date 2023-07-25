using UnityEngine;
using Zenject;

public class TraccerEffectFactory : BaseFactory<TracerEffect>
{
    public TraccerEffectFactory(DiContainer diContainer) : base(diContainer)
    {
    }

    protected override void LoadResources()
    {
        _prefab = Resources.Load<TracerEffect>(HashResourcesPath.VFX_TRACER);
    }
}
