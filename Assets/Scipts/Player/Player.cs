using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialize fields
    [Header("Parameters")]
    [SerializeField] private int _avgDamage = 50;
    [SerializeField] private float _maxRunSpeed = 3.6f;
    [SerializeField] private float _maxSprintSpeed = 5f;
    [SerializeField] private int _maxAttackSpeed = 100;

    [Header("Weapons")]
    [SerializeField] private GameObject _meleeWeapon;
    [SerializeField] private GameObject _rangeWeapon;
    #endregion Serialize fields

    #region Properties

    /// <summary>
    /// Состояние спринта
    /// </summary>
    public bool IsSprinting => FirstPersonController.IsSprinting;

    public bool IsBlockSprint
    {
        set
        {
            FirstPersonController.IsBlockSprint = value;
        }
    }
    
    public bool IsBlockChangeWeapon { get; set; }

    public Camera Camera { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public FirstPersonController FirstPersonController { get; private set; }

    public Dictionary<Type, AttackModifaer> AttackModifaers { get; private set; }
    #endregion Properties

    #region Private fields

    private IWeapon _usedWeapon;
    private GameObject _usedWeaponGameObj;

    #endregion Private fields

    #region Mono
    private void Start()
    {
        InitAttackModifaers();

        Rigidbody = GetComponent<Rigidbody>();
        FirstPersonController = GetComponent<FirstPersonController>();

        FirstPersonController.walkSpeed = PlayerManager.Instance.MovementSpeed.Max/100f;
        FirstPersonController.sprintSpeed = PlayerManager.Instance.MovementSpeed.Max * (1f + 0.18f)/100f;

        Camera = GetComponentInChildren<Camera>();

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
    private void InitAttackModifaers()
    {
        AttackModifaers = new Dictionary<Type, AttackModifaer>();

        AttackModifaers[typeof(FlameAttack)] = new FlameAttack();
        AttackModifaers[typeof(SlowAttack)] = new SlowAttack();
        AttackModifaers[typeof(CriticalAttack)] = new CriticalAttack();
        AttackModifaers[typeof(PenetrationProjectile)] = new PenetrationProjectile();
    }

    public AttackModifaer GetAttackModifaer<T>() where T : AttackModifaer
    {
        Type type = typeof(T);

        if(AttackModifaers.ContainsKey(type))
            return AttackModifaers[type];

        return null;
    }

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
