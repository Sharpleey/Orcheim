using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Класс пул объектов
/// </summary>
/// <typeparam name="T">Тип объектов пула</typeparam>
public class Pool<T> where T : MonoBehaviour
{
    ///// <summary>
    ///// Префаб объектов пула
    ///// </summary>
    //private T _prefab;

    /// <summary>
    /// Контейнер, объект на сцене куда будут складыватся объекты пула
    /// </summary>
    private Transform _container;

    /// <summary>
    /// Начальный размер пула объектов
    /// </summary>
    private int _startSize;

    /// <summary>
    /// Список (Пул) объектов
    /// </summary>
    private List<T> _pool = new List<T>();

    private BaseFactory<T> _factory;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">Префаб объекта, для которого хотим создать пул</param>
    /// <param name="startSizePool">Начальный размер пула</param>
    /// <param name="container">Контейнер, объект, в которому будут присоеденены все объекты пула на сцене в иерархии</param>
    public Pool(BaseFactory<T> factory, int startSizePool = 0)
    {
        _startSize = startSizePool;
        _factory = factory;

        CreateContainer();
        FillPool();
    }

    /// <summary>
    /// Метод создания пула
    /// </summary>
    private void FillPool()
    {
        for(int i=0; i<_startSize; i++)
        {
            T obj = _factory.GetNewInstance(Vector3.zero, Quaternion.identity, false, _container);

            _pool.Add(obj);
        }
    }

    private void CreateContainer()
    {
        _container = new GameObject(name: $"{typeof(T)}_PoolContainer").transform;
    }

    /// <summary>
    /// Метод создает и возвращает новый объект типа T
    /// </summary>
    /// <returns>Объект типа Т</returns>
    //private T GetInstance()
    //{
    //    T newInstance = Object.Instantiate(_prefab, _container);

    //    return newInstance;
    //}

    /// <summary>
    /// Метод проверяет есть ли свободный объект в пуле и возвращает его в виде out и состояние bool если есть
    /// </summary>
    /// <param name="freeElement">(Out) Свободный объект пула</param>
    /// <returns>Есть ли свободный элемент или нет</returns>
    private bool HasFreeElement(out T freeElement)
    {
        foreach (var poolElement in _pool)
        {
            if (poolElement.gameObject.activeInHierarchy)
                continue;

            freeElement = poolElement;

            return true;
        }

        freeElement = null;
        return false;
    }

    /// <summary>
    /// Метод возращает свободный элемент из пула объектов
    /// </summary>
    /// <returns></returns>
    public T GetFreeElement()
    {
        T freeElement;

        if (HasFreeElement(out var element))
        {
            freeElement = element;
            _pool.Remove(element);
        }
        else
            freeElement = _factory.GetNewInstance(Vector3.zero, Quaternion.identity, false, _container);

        freeElement?.gameObject.SetActive(true);

        return freeElement;
    }

    /// <summary>
    /// Дозаполняет пул объектами до нужного кол-ва
    /// </summary>
    public void RefillPool(int size)
    {
        for (int i = _pool.Count; i < size; i++)
        {
            T element = _factory.GetNewInstance(Vector3.zero, Quaternion.identity, false, _container);

            _pool.Add(element);
        }
    }

    /// <summary>
    /// Возращает элемент в пул (В объект контейнер на сцене)
    /// </summary>
    /// <param name="element"></param>
    public void ReturnToContainerPool(T element)
    {
        element.gameObject.SetActive(false);
        element.transform.SetParent(_container);
        element.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        _pool.Add(element);
    }


}
