using UnityEngine;
using Zenject;

public class StartupInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("StartupInstaller");
    }
}
