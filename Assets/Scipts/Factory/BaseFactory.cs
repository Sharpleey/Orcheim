using UnityEngine;

/// <summary>
/// Базовый абстрактный класс фабрики юнитов
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    public virtual T GetNewInstance(T prefab, Vector3 position)
    {
        var newobj = Instantiate(prefab, position, Quaternion.identity);

        return newobj;
    }
}
