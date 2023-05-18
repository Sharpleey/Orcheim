using UnityEngine;
using Zenject;

/// <summary>
/// Базовый абстрактный класс фабрики юнитов
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseFactory<T> where T : MonoBehaviour
{
    private readonly DiContainer _diContainer;
    
    [Inject]
    public BaseFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    public virtual T GetNewInstance(T prefab, Vector3 position, Quaternion rotation, bool isActive = true, Transform parent = null)
    {
        var instance = _diContainer.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        instance.gameObject.SetActive(isActive);
        return instance;
    }
}
