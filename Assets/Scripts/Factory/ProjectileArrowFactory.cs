using UnityEngine;
using Zenject;

public class ProjectileArrowFactory : BaseFactory<ProjectileArrow>
{
    public ProjectileArrowFactory(DiContainer diContainer) : base(diContainer)
    {
    }

    protected override void LoadResources()
    {
        _prefab = Resources.Load<ProjectileArrow>(HashResourcesPath.PROJECTILE_ARROW);
    }
}
