using UnityEngine;

/// <summary>
/// Базовый абстрактный класс фабрики юнитов
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    public virtual T GetNewInstance(T obj, Vector3 position)
    {
        var newobj = Instantiate(obj, position, Quaternion.identity);

        return newobj;
    }
}
