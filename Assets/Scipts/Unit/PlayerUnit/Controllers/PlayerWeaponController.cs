using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    #region Serialize fields

    [Header("Оружие ближнего боя")]
    [SerializeField] private GameObject _meleeWeapon;

    [Header("Оружие дальнего боя")]
    [SerializeField] private GameObject _rangeWeapon;

    [Header("Клавиши смены оружия")]
    [SerializeField] private KeyCode _keyCodeMeleeWeapon = KeyCode.Alpha1;
    [SerializeField] private KeyCode _keyCodeRangeWeapon = KeyCode.Alpha2;

    #endregion Serialize fields

    #region Properties

    /// <summary>
    /// Флаг блокировки смены орудия
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
    /// Метод смены оружия
    /// </summary>
    /// <param name="weapon">Объект оружия, которое хотим сделать активным</param>
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
