using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������� �������� �� ����������� ������ (��������) ��� �����������
/// </summary>
public class IconEffectsController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private GameObject _fireIcon;
    [SerializeField] private GameObject _electroIcon;
    [SerializeField] private GameObject _slowdownIcon;

    /// <summary>
    /// ���������� �� � ����� ��������
    /// </summary>
    [SerializeField] [Range(0.5f, 5f)] private float _offsetX = 3f;
    #endregion Serialize fields

    #region Private fields
    /// <summary>
    /// ������ �������� ������, ��������� ��� ����������� ������������ ������ ��� �����������
    /// </summary>
    private List<GameObject> _activeIcons;
    #endregion Private fields

    #region Mono
    void Start()
    {
        _fireIcon.SetActive(false);
        _electroIcon.SetActive(false);
        _slowdownIcon.SetActive(false);

        _activeIcons = new List<GameObject>();
    }
    #endregion Mono

    #region Private methods
    /// <summary>
    /// ����� � ������ �������� ������ ��������������� ������������ ������ �� ������ ���, ����� 
    /// � ����� ������ � ��� ����� ����������� ��� ������������ �� ������ ����������
    /// </summary>
    private void RecalculationLocationIcon()
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
    /// ����� ����������/������������ ������ �������
    /// </summary>
    /// <param name="active">������������ (true) / �������������� (false)</param>
    public void SetActiveIconBurning(bool active)
    {
        _fireIcon.SetActive(active);

        if (active)
            _activeIcons.Add(_fireIcon);
        else
            _activeIcons.Remove(_fireIcon);

        RecalculationLocationIcon();
    }
    /// <summary>
    /// ����� ����������/������������ ������ �������
    /// </summary>
    /// <param name="active">������������ (true) / �������������� (false)</param>
    public void SetActiveIconElectric(bool active)
    {
        _electroIcon.SetActive(true);
        if (active)
            _activeIcons.Add(_electroIcon);
        else
            _activeIcons.Remove(_electroIcon);

        RecalculationLocationIcon();
    }
    /// <summary>
    /// ����� ����������/������������ ������ ����������
    /// </summary>
    /// <param name="active">������������ (true) / �������������� (false)</param>
    public void SetActiveIconSlowdown(bool active)
    {
        _slowdownIcon.SetActive(true);

        if (active)
            _activeIcons.Add(_slowdownIcon);
        else
            _activeIcons.Remove(_slowdownIcon);

        RecalculationLocationIcon();
    }
    /// <summary>
    /// ������������ ��� ������
    /// </summary>
    public void DeactivateAllIcons()
    {
        foreach (var icon in _activeIcons)
            icon.SetActive(false);

        _activeIcons.Clear();
    }
    #endregion Public methods
}