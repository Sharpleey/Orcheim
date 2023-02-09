using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������� �������� �� ����������� ������ (��������) ��� �����������
/// </summary>
public class IconEffectsController : MonoBehaviour
{
    #region Serialize fields

    [SerializeField] private GameObject _flameIcon;
    [SerializeField] private GameObject _armorUpIcon;
    [SerializeField] private GameObject _slowdownIcon;

    /// <summary>
    /// ���������� �� � ����� ��������
    /// </summary>
    [Header("���������� �� � ����� ��������")]
    [SerializeField] [Range(0.5f, 5f)] private float _offsetX = 3f;

    #endregion Serialize fields

    #region Private fields

    /// <summary>
    /// ������ �������� ������, ��������� ��� ����������� ������������ ������ ��� �����������
    /// </summary>
    private List<GameObject> _activeIcons = new List<GameObject>();

    private Dictionary<Type, GameObject> _effectIcons = new Dictionary<Type, GameObject>();

    #endregion Private fields

    #region Mono

    private void Awake()
    {
        InitIcons();
    }

    private void Start()
    {
        DisableAllIcons();
    }

    #endregion Mono

    #region Private methods

    private void InitIcons()
    {
        _effectIcons.Add(typeof(Flame), _flameIcon);
        _effectIcons.Add(typeof(Slowdown), _slowdownIcon);
        _effectIcons.Add(typeof(ArmorUp), _armorUpIcon);
    }

    private void DisableAllIcons()
    {
        foreach (GameObject item in _effectIcons.Values)
        {
            item.SetActive(false);
        }
    }

    /// <summary>
    /// ����� � ������ �������� ������ ��������������� ������������ ������ �� ������ ���, ����� 
    /// � ����� ������ � ��� ����� ����������� ��� ������������ �� ������ ����������
    /// </summary>
    private void RecalculationLocationIcons()
    {
        float leftBound = (_offsetX * (_activeIcons.Count - 1)) / -2f;

        foreach (var icon in _activeIcons)
        {
            Vector3 newPosition = new Vector3(leftBound, 0, 0);
            icon.transform.localPosition = newPosition;

            leftBound += _offsetX;
        }
    }
    #endregion Private methods

    #region Public methods

    /// <summary>
    /// ����� ���������/����������� ������ ��������������� �������
    /// </summary>
    /// <typeparam name="T">������, ������ �������� ����� ������������</typeparam>
    /// <param name="active">���� ��������� ������</param>
    public void EnableIcon<T>(bool active) where T : Effect
    {
        GameObject icon;

        if (_effectIcons.TryGetValue(typeof(T), out icon))
        {
            icon.SetActive(active);

            if (active)
                _activeIcons.Add(icon);
            else
                _activeIcons.Remove(icon);

            RecalculationLocationIcons();
        }
    }

    /// <summary>
    /// ������������ ��� ������
    /// </summary>
    public void DisableAllActiveIcons()
    {
        foreach (var icon in _activeIcons)
            icon.SetActive(false);

        _activeIcons.Clear();
    }
    #endregion Public methods
}
