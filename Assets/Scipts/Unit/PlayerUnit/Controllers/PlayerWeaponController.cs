using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    #region Serialize fields

    [Header("������ �������� ���")]
    [SerializeField] private GameObject _meleeWeapon;

    [Header("������ �������� ���")]
    [SerializeField] private GameObject _rangeWeapon;

    [Header("������� ����� ������")]
    [SerializeField] private KeyCode _keyCodeMeleeWeapon = KeyCode.Alpha1;
    [SerializeField] private KeyCode _keyCodeRangeWeapon = KeyCode.Alpha2;

    #endregion Serialize fields

    #region Properties

    /// <summary>
    /// ���� ���������� ����� ������
    /// </summary>
    public bool IsBlockChangeWeapon { get; set; }

    #endregion Properties

    #region Private fields

    private GameObject _usedWeaponGameObj;

    #endregion Private fields

    #region Mono
    private void Start()
    {
        _meleeWeapon?.SetActive(false);
        _rangeWeapon?.SetActive(false);

        if (_rangeWeapon)
            ChangeWeapon(_rangeWeapon);
        else
            ChangeWeapon(_meleeWeapon);

    }

    private void Update()
    {
        if (!IsBlockChangeWeapon)
        {
            if (Input.GetKeyDown(_keyCodeMeleeWeapon))
            {
                PlayerEventManager.PlayerChooseMeleeWeapon();

                ChangeWeapon(_meleeWeapon);
            }

            if (Input.GetKeyDown(_keyCodeRangeWeapon))
            {
                PlayerEventManager.PlayerChooseRangeWeapon();

                ChangeWeapon(_rangeWeapon);
            }
        }

    }
    #endregion Mono

    #region Private methods

    /// <summary>
    /// ����� ����� ������
    /// </summary>
    /// <param name="weapon">������ ������, ������� ����� ������� ��������</param>
    private void ChangeWeapon(GameObject weapon)
    {
        if (weapon == _usedWeaponGameObj)
            return;

        _usedWeaponGameObj?.SetActive(false);
        _usedWeaponGameObj = weapon;
        _usedWeaponGameObj.SetActive(true);
    }
    #endregion Private methods
}
