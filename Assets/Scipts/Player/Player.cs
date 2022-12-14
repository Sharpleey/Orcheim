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
    public int AvgDamage => _avgDamage;
    /// <summary>
    /// Максимальная скорость бега игрока
    /// </summary>
    public float MaxRunSpeed => _maxRunSpeed;
    public float MaxSprintSpeed => _maxSprintSpeed;
    public int MaxAttackSpeed 
    { 
        get => _maxAttackSpeed; 
        private set =>  _maxAttackSpeed = value; 
    }

    public int Damage
    {
        get
        {
            int range = (int)(AvgDamage * GeneralParameter.OFFSET_DAMAGE_HEALING);
            return UnityEngine.Random.Range(AvgDamage - range, AvgDamage + range);
        }
    }

    /// <summary>
    /// Текущая максимальная скорость бега игрока. Данный параметр изменяем под разные случаи
    /// </summary>
    public float CurrentMaxRunSpeed
    {
        get => _firstPersonController.walkSpeed;
        set => _firstPersonController.walkSpeed = value;
    }

    /// <summary>
    /// Текущая максимальная скорость спринта игрока. Данный параметр изменяем под разные случаи
    /// </summary>
    public float CurrentMaxSprintSpeed
    {
        get => _firstPersonController.sprintSpeed;
        set => _firstPersonController.sprintSpeed = value;
    }

    public float CurrentMaxAttackSpeed
    {
        get => _currentMaxAttackSpeed;
        private set => _currentMaxAttackSpeed = value;
    }

    /// <summary>
    /// Актуальная скорость игрока
    /// </summary>
    public float ActualSpeed => _rigidbody.velocity.magnitude;
    
    /// <summary>
    /// Состояние спринта
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

    public Dictionary<Type, AttackModifaer> AttackModifaers { get; private set; }
    #endregion Properties

    #region Private fields
    private float _currentMaxAttackSpeed;

    private IWeapon _usedWeapon;
    private GameObject _usedWeaponGameObj;

    private Rigidbody _rigidbody;

    /// <summary>
    /// Контроллер передвижения
    /// </summary>
    private FirstPersonController _firstPersonController;
    #endregion Private fields

    #region Mono
    private void Start()
    {
        InitAttackModifaers();

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
