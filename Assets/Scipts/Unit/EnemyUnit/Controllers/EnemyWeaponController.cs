using UnityEngine;

/// <summary>
/// ����� �������� �� ������, ������� ����� ������������ ��������. ��� ���������� �������� �� ������� � �������. 
/// �� ������, ������� ����� �������������� ������ ����� �����, ������� � ������ � ����������.
/// </summary>
public class EnemyWeaponController : MonoBehaviour
{
    /// <summary>
    /// ������������ ������, � �������� ������������� ������
    /// </summary>
    [SerializeField] private Transform _weaponPlace;

    /// <summary>
    /// ������������ ������. ����� ������ � ��������� ��� ���� �� ������ ���������� �� ������� � �������
    /// </summary>
    [SerializeField] private GameObject _usedWeapon;

    /// <summary>
    /// ������ ������, ������� ����� ������������ ������ ��� �����
    /// </summary>
    [SerializeField] private GameObject[] _weapons;

    private CapsuleCollider _usedWeaponTriggerCollider;
    private Rigidbody _rigidbodyUsedWeapon;

    private Vector3 _weaponPositionOnWeaponPlace;
    private Quaternion _weaponRotationOnWeaponPlace;

    private void Start()
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
        // �������� � ������������� ��������� ������
        int indexWeapon = Random.Range(0, _weapons.Length);
        _usedWeapon = _weapons[indexWeapon];

        // ������ �������� ������������ ������
        _usedWeapon.SetActive(true);

        // �������� ��������� ������ ���������� �� ��������� �����
        _usedWeaponTriggerCollider = _usedWeapon.GetComponentInChildren<CapsuleCollider>();

        // �������� Rigidbody ������������� ������ ��� ����������� �������������
        _rigidbodyUsedWeapon = _usedWeapon.GetComponent<Rigidbody>();

        // ������ ���������� ��������� ����������
        if (_usedWeaponTriggerCollider)
            _usedWeaponTriggerCollider.enabled = false;
    }

    /// <summary>
    /// ����� ������ �� ��������� ��� ������
    /// </summary>
    private void DisableWeapons()
    {
        foreach (GameObject weapon in _weapons)
        {
            weapon.SetActive(false);
        }
    }

    /// <summary>
    /// ����� ��� ������� �������� �����. ������������ ����� � ������������ ������� ����� �������� � ��������� 
    /// ����������� ��������� ������ �����
    /// </summary>
    private void EnableDealingDamage(ObjectState state)
    {
        _usedWeaponTriggerCollider.enabled = state == ObjectState.Enabled;
    }

    /// <summary>
    /// ����� ���������� ������ �� ������ ���������� � ������ ��� �������� ��� �������� ��������� ������ �� �����
    /// </summary>
    /// <param name="isMakePhysical">������� ��������</param>
    public void MakeWeaponPhysical(bool isMakePhysical)
    {
        if(isMakePhysical)
        {
            // ���������� ������� � ������� ������ �� ��� ������������ �� �����
            _weaponPositionOnWeaponPlace = _usedWeapon.transform.position;
            _weaponRotationOnWeaponPlace = _usedWeapon.transform.rotation;

            // ����������� ������ �� �����  
            _usedWeapon.transform.parent = null;

            // ������ ��� �� ���������
            _rigidbodyUsedWeapon.isKinematic = false;

            return;
        }

        // ��������� ������ �� �����
        _usedWeapon.transform.parent = _weaponPlace;
        _usedWeapon.transform.position = _weaponPositionOnWeaponPlace;
        _usedWeapon.transform.rotation = _weaponRotationOnWeaponPlace;
        _rigidbodyUsedWeapon.isKinematic = true;
    }
}
