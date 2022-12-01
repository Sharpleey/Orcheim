using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialize fields
    [Header("Parameters")]
    [SerializeField] private float _maxRunSpeed = 3.6f;
    [SerializeField] private float _maxSprintSpeed = 5f;
    [SerializeField] private int _maxAttackSpeed = 100;

    [Header("Weapons")]
    [SerializeField] private GameObject _meleeWeapon;
    [SerializeField] private GameObject _rangeWeapon;
    #endregion Serialize fields

    #region Properties
    /// <summary>
    /// ������������ �������� ���� ������
    /// </summary>
    public float MaxRunSpeed => _maxRunSpeed;
    public float MaxSprintSpeed => _maxSprintSpeed;
    public int MaxAttackSpeed { get => _maxAttackSpeed; private set =>  _maxAttackSpeed = value; }

    /// <summary>
    /// ������� ������������ �������� ���� ������. ������ �������� �������� ��� ������ ������
    /// </summary>
    public float CurrentMaxRunSpeed
    {
        get => _firstPersonController.walkSpeed;
        set
        {
            _firstPersonController.walkSpeed = value;
        }
    }

    /// <summary>
    /// ������� ������������ �������� ������� ������. ������ �������� �������� ��� ������ ������
    /// </summary>
    public float CurrentMaxSprintSpeed
    {
        get => _firstPersonController.sprintSpeed;
        set
        {
            _firstPersonController.sprintSpeed = value;
        }
    }

    public float CurrentMaxAttackSpeed
    {
        get => _currentMaxAttackSpeed;
        private set
        {
            _currentMaxAttackSpeed = value;
        }
    }

    /// <summary>
    /// ���������� �������� ������
    /// </summary>
    public float ActualSpeed => _rigidbody.velocity.magnitude;
    
    /// <summary>
    /// ��������� �������
    /// </summary>
    public bool IsSprinting => _firstPersonController.IsSprinting;

    public bool IsBlockSprint
    {
        set
        {
            _firstPersonController.IsBlockSprint = value;
        }
    }
    
    public bool IsBlockChangeWeapon { get; set; }

    public Camera Camera { get; private set; }
    #endregion Properties

    #region Private fields
    private float _currentMaxAttackSpeed;

    private IWeapon _usedWeapon;
    private GameObject _usedWeaponGameObj;

    private Rigidbody _rigidbody;

    /// <summary>
    /// ���������� ������������
    /// </summary>
    private FirstPersonController _firstPersonController;
    #endregion Private fields

    #region Mono
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _firstPersonController = GetComponent<FirstPersonController>();

        Camera = GetComponentInChildren<Camera>();

        CurrentMaxRunSpeed = _maxRunSpeed;
        CurrentMaxSprintSpeed = _maxSprintSpeed;
        CurrentMaxAttackSpeed = _maxAttackSpeed;

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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeWeapon(_meleeWeapon);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon(_rangeWeapon);
            }
        }

    }
    #endregion Mono

    #region Private methods
    private void ChangeWeapon(GameObject weapon)
    {
        if (weapon == _usedWeaponGameObj)
            return;

        _usedWeaponGameObj?.SetActive(false);
        _usedWeaponGameObj = weapon;
        _usedWeaponGameObj.SetActive(true);

        _usedWeapon = _usedWeaponGameObj.GetComponent<IWeapon>();
    }
    #endregion Private methods

    #region Public methods
    public void TakeDamage(int damage)
    {
        PlayerEventManager.PlayerDamaged(damage);
    }
    #endregion Private methods
}
