using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// ����� ��� ��������
/// </summary>
/// <typeparam name="T">��� �������� ����</typeparam>
public class Pool<T> where T : MonoBehaviour
{
    ///// <summary>
    ///// ������ �������� ����
    ///// </summary>
    //private T _prefab;

    /// <summary>
    /// ���������, ������ �� ����� ���� ����� ����������� ������� ����
    /// </summary>
    private Transform _container;

    /// <summary>
    /// ��������� ������ ���� ��������
    /// </summary>
    private int _startSize;

    /// <summary>
    /// ������ (���) ��������
    /// </summary>
    private List<T> _pool = new List<T>();

    private BaseFactory<T> _factory;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">������ �������, ��� �������� ����� ������� ���</param>
    /// <param name="startSizePool">��������� ������ ����</param>
    /// <param name="container">���������, ������, � �������� ����� ������������ ��� ������� ���� �� ����� � ��������</param>
    public Pool(BaseFactory<T> factory, int startSizePool = 0)
    {
        _startSize = startSizePool;
        _factory = factory;

        CreateContainer();
        FillPool();
    }

    /// <summary>
    /// ����� �������� ����
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
    /// ����� ������� � ���������� ����� ������ ���� T
    /// </summary>
    /// <returns>������ ���� �</returns>
    //private T GetInstance()
    //{
    //    T newInstance = Object.Instantiate(_prefab, _container);

    //    return newInstance;
    //}

    /// <summary>
    /// ����� ��������� ���� �� ��������� ������ � ���� � ���������� ��� � ���� out � ��������� bool ���� ����
    /// </summary>
    /// <param name="freeElement">(Out) ��������� ������ ����</param>
    /// <returns>���� �� ��������� ������� ��� ���</returns>
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
    /// ����� ��������� ��������� ������� �� ���� ��������
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
    /// ����������� ��� ��������� �� ������� ���-��
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
    /// ��������� ������� � ��� (� ������ ��������� �� �����)
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
