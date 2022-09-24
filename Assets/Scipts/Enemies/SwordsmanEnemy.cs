using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]
[RequireComponent(typeof(StateMachineSwordsman))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class SwordsmanEnemy : MonoBehaviour, IEnemy
{
    #region Serialize fields
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;
    [SerializeField] private float _speed = 3.5f;

    [Header("Weapon Visual Settings")]
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject[] _prefabWeapons;
    [SerializeField] private Transform _weaponPlace;
    #endregion Serialize fields

    #region Properties
    /// <summary>
    /// Максимальное значение здоровья противника
    /// </summary>
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if (value <= 0)
            {
                _maxHealth = 1;
                return;
            }
            _maxHealth = value;
        }
    }
    /// <summary>
    /// Текущее значение здоровья противника
    /// </summary>
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value < 0)
            {
                _health = 0;
                return;
            }
            if (value > _maxHealth)
            {
                _health = _maxHealth;
                return;
            }
            _health = value;
        }
    }
    /// <summary>
    /// Максимальное значение скорости передвижения противника
    /// </summary>
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (value < 0.1f)
            {
                _speed = 0.1f;
                return;
            }
            float speed = Random.Range(value - 0.4f, value + 0.4f);
            _speed = speed;
        }
    }
    /// <summary>
    /// Актуальная скорость противника, скорость передвижения в NavMeshAgent
    /// </summary>
    public float CurrentSpeed
    {
        get
        {
            return NavMeshAgent.velocity.magnitude;
        }
        set
        {
            if (value < 0.1f)
            {
                NavMeshAgent.speed = 0.1f;
                return;
            }
            NavMeshAgent.speed = value;
        }
    }
    
    public bool IsAttacked { get; private set; }

    public HitBoxesController HitBoxesController { get; private set; }
    public RagdollController RagdollController { get; private set; }
    public HealthBarController HealthBarController { get; private set; }
    public PopupDamageController PopupDamageController { get; private set; }

    public DieEffectController DieEffectController { get; private set; }
    public BurningEffectController BurningEffectController { get; private set; }

    public IconEffectsController IconEffectsController { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }

    public GameObject Weapon => _weapon;
    #endregion Properties

    #region Private fields
    private bool _isBurning = false;
    private float _timerBurning = 0f;
    private int _durationBurning = 0;

    private bool _isSlow = false;
    private float _timerSlow = 0f;
    private int _durationSlow = 0;

    private StateMachineSwordsman _stateMachine;
    #endregion Private fields

    #region Mono
    private void Awake()
    {

        MaxHealth = _maxHealth;
        Health = MaxHealth;
        Speed = _speed;
    }

    private void Start()
    {
        // Выбираем оружие из списка для визуальной разницы
        SetRandomWeaponModel();

        // Получаем контроллерыи компоненты
        // ---------------------------------------------------------------
        HitBoxesController = GetComponent<HitBoxesController>();
        RagdollController = GetComponent<RagdollController>();

        IconEffectsController = GetComponentInChildren<IconEffectsController>();

        BurningEffectController = GetComponentInChildren<BurningEffectController>();
        DieEffectController = GetComponentInChildren<DieEffectController>();
        HealthBarController = GetComponentInChildren<HealthBarController>();
        PopupDamageController = GetComponentInChildren<PopupDamageController>();

        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        _stateMachine = GetComponent<StateMachineSwordsman>();
        _stateMachine?.InitializeStates(this);
        // ---------------------------------------------------------------

        // Делаем компонент неактивным, чтобы не началась анимация
        if (DieEffectController != null)
            DieEffectController.enabled = false;

        if (BurningEffectController != null)
            BurningEffectController.enabled = false;

        // Устанавливаем скорость для NavMeshAgent
        CurrentSpeed = Speed;

        // Устанавливаем максимальное и актуальное хп для полосы хп
        HealthBarController?.SetMaxHealth(MaxHealth);
        HealthBarController?.SetHealth(Health);

        // Задаем начальное состояние
        _stateMachine?.InitializeStartingState(_stateMachine.IdleState);
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (_stateMachine != null)
            _stateMachine.CurrentState.Update();

        // TODO когда будет больше эффектов переделать под машину состояний 
        if (_isBurning)
            Burning();

        if (_isSlow)
            Slowing();
    }
    private void FixedUpdate()
    {
        if (_stateMachine != null)
            _stateMachine.CurrentState.FixedUpdate();
    }

    /// <summary>
    /// Метод устанавливает рандомную модель оружия, для визуального разнообразия
    /// </summary>
    private void SetRandomWeaponModel()
    {
        int id_weapon = Random.Range(0, _prefabWeapons.Length);
        _weapon = Instantiate(_prefabWeapons[id_weapon]);

        _weapon.transform.parent = _weaponPlace.transform;

        _weapon.transform.position = _weaponPlace.position;
        _weapon.transform.rotation = _weaponPlace.rotation;

    }

    /// <summary>
    /// Метод отвечает за эффект горения, его длительность и нанесения урона
    /// </summary>
    private void Burning()
    {
        if (_timerBurning < _durationBurning + 1)
        {
            _timerBurning += Time.deltaTime;
        }
        else
        {
            _timerBurning = 0;
            _isBurning = false;
            BurningEffectController.enabled = false;

            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconBurning(false);
        }
    }

    private void Slowing()
    {
        if (_timerSlow < _durationSlow + 1)
        {
            _timerSlow += Time.deltaTime;
        }
        else
        {
            _timerSlow = 0;

            CurrentSpeed = Speed;
            _isSlow = false;

            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconSlowdown(false);
        }
    }
    #endregion Private methods

    #region Public methods
    public void TakeDamage(int damage, TypeDamage typeDamage)
    {
        if (Health > 0)
        {
            Health -= damage;

            IsAttacked = true;

            // Всплывающий дамаг
            if (PopupDamageController != null)
                PopupDamageController.ShowPopupDamage(damage, typeDamage);

            // Полоска хп
            if (HealthBarController != null)
            {
                HealthBarController.SetHealth(Health);
                HealthBarController.ShowHealthBar();
            }
        }

        if (Health <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.DieState);
        }
    }
    public void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage)
    {
        // Получаем значение урона с учетом попадания в ту или иную часть тела
        damage = HitBoxesController.GetDamageValue(damage, hitCollider);
        TakeDamage(damage, typeDamage);
    }
    public void SetBurning(int damagePerSecond, int duration, TypeDamage typeDamage)
    {
        if (!_isBurning)
        {
            _durationBurning = duration;

            BurningEffectController.DamagePerSecond = damagePerSecond;
            BurningEffectController.TypeDamage = typeDamage;

            _isBurning = true;
            BurningEffectController.enabled = true;

            // Включаем иконку горения над противником
            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconBurning(true);
        }
    }
    public void SetSlowing(float slowdown, int duration)
    {
        if (!_isSlow)
        {
            _durationSlow = duration;

            CurrentSpeed = Speed * (1f - slowdown);

            _isSlow = true;

            // Включаем иконку замедления над противником
            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconSlowdown(true);
        }
    }
    #endregion Public methods
}