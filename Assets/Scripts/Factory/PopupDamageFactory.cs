using UnityEngine;
using Zenject;

public class PopupDamageFactory : BaseFactory<PopupDamage>
{
    public PopupDamageFactory(DiContainer diContainer) : base(diContainer)
    {
    }

    protected override void LoadResources()
    {
        _prefab = Resources.Load<PopupDamage>(HashResourcesPath.UI_POPUP_DAMAGE);
    }
}
