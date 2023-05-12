using UnityEngine;
using Zenject;

public class LocalInstaller : MonoInstaller
{
    [SerializeField] private Transform PlayerStartPoint;
    [SerializeField] private GameObject PlayerPrefab;

    public override void InstallBindings()
    {
        PlayerUnit playerUnit = Container.InstantiatePrefabForComponent<PlayerUnit>(PlayerPrefab, PlayerStartPoint.position, Quaternion.identity, null);

        Container.Bind<PlayerUnit>().FromInstance(playerUnit).AsSingle();
    }
}