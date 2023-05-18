using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��� ��������
/// </summary>
/// <typeparam name="T">��� �������� ����</typeparam>
public class Pool<T> where T : MonoBehaviour
{
    /// <summary>
    /// ������ �������� ����
    /// </summary>
    private T _prefab;

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
    private List<T> _pool;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">������ �������, ��� �������� ����� ������� ���</param>
    /// <param name="startSizePool">��������� ������ ����</param>
    /// <param name="container">���������, ������, � �������� ����� ������������ ��� ������� ���� �� ����� � ��������</param>
    public Pool(T prefab, int startSizePool, Transform container)
    {
        _prefab = prefab;
        _container = container;
        _startSize = startSizePool;

        CreatePool();
    }

    /// <summary>
    /// ����� �������� ����
    /// </summary>
    private void CreatePool()
    {
        _pool = new List<T>();

        for(int i=0; i<_startSize; i++)
        {
            T obj = GetInstance();

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_container);

            _pool.Add(obj);
        }
    }

    /// <summary>
    /// ����� ������� � ���������� ����� ������ ���� T
    /// </summary>
    /// <returns>������ ���� �</returns>
    private T GetInstance()
    {
        T newInstance = Object.Instantiate(_prefab, _container);
        return newInstance;
    }

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
            freeElement = GetInstance();

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
            T element = GetInstance();

            element.gameObject.SetActive(false);
            element.transform.SetParent(_container);

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
