using UnityEngine;

/// <summary>
/// ����� �������� �� ��� �����, ������� ����� ������������ ��������. ��� ���������� �������� �� ������� � ������. 
/// ��� ��� �����, ������� ����� �������������� ������ ����� ������, ������� � ������ armorSets � ����������.
/// </summary>
public class ArmorSetController : MonoBehaviour
{
    /// <summary>
    /// ����������� ��� �����. ������ � ���������� � �������� � ������ Start
    /// </summary>
    [SerializeField] private GameObject _usedArmorSet;
    /// <summary>
    /// ������ �������� � ������, ������� ����� ����������� ������ ��� ������
    /// </summary>
    [SerializeField] private GameObject[] _armorSets;

    void Start()
    {
        // ������������ ��� ���� ����� �� ������, ���� ���� ��� ��������� ���� �������� � ������� Orc
        DisableArmorSets();

        if (!_usedArmorSet)
        {
            // ���������� �������� ������ � ����� �����
            SetRandomArmorSet();
        }
    }

    /// <summary>
    /// ����� ���������� ��������� ��� ����� �� ������� armorSets
    /// </summary>
    private void SetRandomArmorSet()
    {
        int indexArmorSet = Random.Range(0, _armorSets.Length);

        _usedArmorSet = _armorSets[indexArmorSet];

        _usedArmorSet.SetActive(true);
    }

    /// <summary>
    /// ����� ������������ ��� ���� �����
    /// </summary>
    private void DisableArmorSets()
    {
        foreach(GameObject armorSet in  _armorSets)
        {
            armorSet.SetActive(false);
        }
    }
}
