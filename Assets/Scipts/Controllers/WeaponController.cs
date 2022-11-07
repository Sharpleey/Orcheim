using UnityEngine;

/// <summary>
/// ����� �������� �� ������, ������� ����� ������������ ��������. ��� ���������� �������� �� ������� � �������. 
/// �� ������, ������� ����� �������������� ������ ����� �����, ������� � ������ � ����������.
/// </summary>
public class WeaponController : MonoBehaviour
{
    /// <summary>
    /// ������������ ������. ����� ������ � ��������� ��� ���� �� ������ ���������� �� ������� � �������
    /// </summary>
    [SerializeField] private GameObject _usedWeapon;
    /// <summary>
    /// ������ ������, ������� ����� ������������ ������ ��� �����
    /// </summary>
    [SerializeField] private GameObject[] _weapons;

    public GameObject UsedWeapon => _usedWeapon;

    private void Awake()
    {
        DisableWeapons();

        if (!_usedWeapon)
        {
            SetRandomWeapon();
        }
    }

    /// <summary>
    /// ����� ������������� ��������� ������ �� ������.
    /// ��� ������� � ������� ������ ��� ���� ����������� �� ����� � ������� ��������� (�����), �� �� �������.
    /// ����� ���������� ������������ ������.
    /// </summary>
    private void SetRandomWeapon()
    {
        int indexWeapon = Random.Range(0, _weapons.Length);

        _usedWeapon = _weapons[indexWeapon];

        _usedWeapon.SetActive(true);
    }

    private void DisableWeapons()
    {
        foreach (GameObject weapon in _weapons)
        {
            weapon.SetActive(false);
        }
    }
}
