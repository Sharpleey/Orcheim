using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private int _maxHealth = 150;
    [SerializeField] private int _maxArmor = 0;
    [SerializeField] private float _maxSpeed = 3.5f;
    [SerializeField] private int _averageDamage = 25;

    [Header("State Settings")]
    [SerializeField] private DefaultState _defaultState; 

    [Header("Summon Trigger")]
    [SerializeField] private BoxCollider _summonTrigger;
    #endregion Serialize fields

    #region Properties
    /// <summary>
    /// ������������ �������� �������� ����������
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
    /// ������������ �������� ����� ����������
    /// </summary>
    public int MaxArmor
    {
        get
        {
            return _maxArmor;
        }
        private set
        {
            if (value < 0)
            {
                _maxArmor = 0;
                return;
            }
            _maxArmor = value;
        }
    }

    /// <summary>
    /// ������������ �������� �������� ������������ ����������
    /// </summary>
    public float MaxSpeed
    {
        get
        {
            return _maxSpeed;
        }
        set
        {
            if (value < 0.1f)
            {
                _maxSpeed = 0.1f;
                return;
            }
            _maxSpeed = value;
        }
    }

    /// <summary>
    /// �������� ������� ����. �������� ������ ��� �������� ������������ ��� � ����������
    /// </summary>
    public int AverageDamage
    {
        get
        {
            return _averageDamage;
        }
        private set
        {
            if (value < 0)
            {
                _averageDamage = 0;
                return;
            }
            _averageDamage = value;
        }
    }

    /// <summary>
    /// ������� (������������) ������� ����. ���������� ��� ���������� ������� ��������������
    /// </summary>
    public int CurrentAverageDamage
    {
        get
        {
            return _currentAverageDamage;
        }
        set
        {
            if (value < 0)
            {
                _currentAverageDamage = 0;
                return;
            }
            _currentAverageDamage = value;
        }
    }

    /// <summary>
    /// ���� � ������ ��������, ������ �������� ���������� ��� ��������� �����
    /// </summary>
    public int ActualDamage
    {
        get
        {
            int range = (int)(CurrentAverageDamage * GeneralParameter.OFFSET_DAMAGE_HEALING);
            return UnityEngine.Random.Range(CurrentAverageDamage - range, CurrentAverageDamage + range);
        }
    }

    /// <summary>
    /// ������� �������� �������� ����������
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
    /// ���������� ����� ����������
    /// </summary>
    public int Armor
    {
        get
        {
            return _armor;
        }
        set
        {
            if (value < 0)
            {
                _armor = 0;
                return;
            }
            if (value > _maxHealth)
            {
                _armor = _maxArmor;
                return;
            }
            _armor = value;
        }
    }

    /// <summary>
    /// ���������� �������� ����������, �������� ������������ � NavMeshAgent
    /// </summary>
    public float Speed
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

    public DefaultState DefaultState { get => _defaultState; set => _defaultState = value; }

    public HitBoxesController HitBoxesController { get; private set; }
    public RagdollController RagdollController { get; private set; }
    public HealthBarController HealthBarController { get; private set; }
    public PopupDamageController PopupDamageController { get; private set; }
    public WeaponController WeaponController { get; private set; }
    public DieEffectController DieEffectController { get; private set; }
    public BurningEffectController BurningEffectController { get; private set; }
    public IconEffectsController IconEffectsController { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }

    public BoxCollider SummonTrigger { get; private set; }

    /// <summary>
    /// ������� ��������� �����
    /// </summary>
    public State CurrentState { get; private protected set; }

    public GameObject Weapon => WeaponController.UsedWeapon;
    public CapsuleCollider WeaponTriggerCollider { get; private set; }

    #endregion Properties

    #region Private fields
    /// <summary>
    /// ������� ��� �������� ���������
    /// </summary>
    protected Dictionary<Type, State> _states;

    private int _health = 0;
    private int _armor = 0;

    private int _currentAverageDamage = 0;

    private bool _isBurning = false;
    private float _timerBurning = 0;
    private int _durationBurning = 0;

    private bool _isSlow = false;
    private float _timerSlow = 0;
    private int _durationSlow = 0;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        MaxHealth = _maxHealth;
        MaxArmor = _maxArmor;
        AverageDamage = _averageDamage;
        CurrentAverageDamage = _averageDamage;
        MaxSpeed = _maxSpeed;

        Health = _maxHealth;
        Armor = _maxArmor;

        DefaultState = _defaultState;

        SummonTrigger = _summonTrigger;
    }

    protected void Start()
    {
        // �������� ������������ ����������
        // ---------------------------------------------------------------
        HitBoxesController = GetComponent<HitBoxesController>();
        RagdollController = GetComponent<RagdollController>();
        WeaponController = GetComponent<WeaponController>();

        WeaponTriggerCollider = Weapon.GetComponentInChildren<CapsuleCollider>();

        IconEffectsController = GetComponentInChildren<IconEffectsController>();

        BurningEffectController = GetComponentInChildren<BurningEffectController>();
        DieEffectController = GetComponentInChildren<DieEffectController>();
        HealthBarController = GetComponentInChildren<HealthBarController>();
        PopupDamageController = GetComponentInChildren<PopupDamageController>();

        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        //NavMeshAgent.updatePosition = false;

        if (WeaponTriggerCollider)
            WeaponTriggerCollider.enabled = false;

        // ������ ��������� ����������, ����� �� �������� ��������
        if (DieEffectController)
            DieEffectController.enabled = false;

        if (BurningEffectController)
            BurningEffectController.enabled = false;

        // ������������� �������� ��� NavMeshAgent
        Speed = _maxSpeed;

        // ������������� ������������ � ���������� �� ��� ������ ��
        HealthBarController?.SetMaxHealth(MaxHealth);
        HealthBarController?.SetHealth(Health);
    }
    
    private void Update()
    {
        if(CurrentState != null)
            CurrentState.Update();

        // TODO ����� ����� ������ �������� ���������� ��� ������ ��������� 
        if (_isBurning)
            Burning();

        if (_isSlow)
            Slowing();
    }
    #endregion Mono

    #region Private methods
    /// <summary>
    /// ����� �������������� ���������
    /// </summary>
    protected void InitStates()
    {
        _states = new Dictionary<Type, State>();

        _states[typeof(IdleState)] = new IdleState(this);
        _states[typeof(DieState)] = new DieState(this);
    }

    /// <summary>
    /// ����� ������������� ������ ��������� �����������
    /// </summary>
    protected void SetStateByDefault()
    {
        if (DefaultState == DefaultState.Pursuit)
            SetState<PursuitState>();
        else
            SetState<IdleState>();
    }

    /// <summary>
    /// ����� ������������ �������� ����� �����������. �� �������� Exit ��� ������� CurrentState ����� ������� ��� ������ �� newState. � ����� �� �������� Enter ��� newState.
    /// </summary>
    /// <typeparam name="T">��� ������ ���������</typeparam>
    protected internal void SetState<T>() where T : State
    {
        State newState;

        try
        {
            newState = _states[typeof(T)];
        }
        catch
        {
            Debug.Log("��������� "+ typeof(T).ToString() + " � ������� ���� ����� ����������!");
            return;
        }

        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = newState;
        CurrentState.Enter();
    }

    /// <summary>
    /// ����� �������� �� ������ �������, ��� ������������ � ��������� �����
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
    
    /// <summary>
    /// ����� �������� �� ������ ����������, ��� ������������
    /// </summary>
    private void Slowing()
    {
        if (_timerSlow < _durationSlow + 1)
        {
            _timerSlow += Time.deltaTime;
        }
        else
        {
            _timerSlow = 0;

            Speed = MaxSpeed;
            _isSlow = false;

            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconSlowdown(false);
        }
    }

    /// <summary>
    /// ����� ��� ������� ������ �������� �����
    /// </summary>
    private void SetEnableWeaponTriggerCollider()
    {
        WeaponTriggerCollider.enabled = true;
    }

    /// <summary>
    /// ����� ��� ������� � �������� ��� ��������� ����� 
    /// </summary>
    private void DisableWeaponTriggerCollider()
    {
        WeaponTriggerCollider.enabled = false;
    }

    #endregion Private methods

    #region Public methods
    /// <summary>
    /// ����� ��������� ����� ����������
    /// </summary>
    /// <param name="damage">�������� ������������ �����</param>
    /// <param name="typeDamage">��� �����</param>
    public virtual void TakeDamage(int damage, TypeDamage typeDamage)
    {
        if (Health > 0)
        {
            Health -= damage;

            // ���� ����� �������� �����, �������� ���������
            if (CurrentState.GetType() == typeof(IdleState))
            {
                // ��������� �������, �� ���� ������� ������ �������� ��� ������ ����
                try
                {
                    Messenger.Broadcast(GlobalGameEvent.FIRST_TRIGGER_GAME);
                }
                catch
                {
                     
                }

                // �������� ��������� �� �������������
                SetState<PursuitState>();
            }

            // ����������� �����
            if (PopupDamageController != null)
                PopupDamageController.ShowPopupDamage(damage, typeDamage);

            // ������� ��
            if (HealthBarController != null)
            {
                HealthBarController.SetHealth(Health);
                HealthBarController.ShowHealthBar();
            }
        }

        if (Health <= 0)
        {
            if (CurrentState.GetType() != typeof(DieState))
                SetState<DieState>();
        }
    }
    
    /// <summary>
    /// ��������� ����� ����� ������������ ������� ���������
    /// </summary>
    /// <param name="damage">�������� ����������� �����</param>
    /// <param name="hitCollider">������� ���������</param>
    /// <param name="typeDamage">��� �����</param>
    public void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage)
    {
        // �������� �������� ����� � ������ ��������� � �� ��� ���� ����� ����
        damage = HitBoxesController.GetDamageValue(damage, hitCollider);
        TakeDamage(damage, typeDamage);
    }

    /// <summary>
    /// ����� ��������� ����� �� ��������� �����
    /// </summary>
    /// <param name="damagePerSecond">�������� ����� � ��������</param>
    /// <param name="duration">������������ �������</param>
    /// <param name="typeDamage">��� ���������� �����</param>
    public void SetBurning(int damagePerSecond, int duration, TypeDamage typeDamage)
    {
        if (!_isBurning && CurrentState.GetType() != typeof(DieState))
        {
            _durationBurning = duration;

            BurningEffectController.DamagePerSecond = damagePerSecond;
            BurningEffectController.TypeDamage = typeDamage;

            _isBurning = true;
            BurningEffectController.enabled = true;

            // �������� ������ ������� ��� �����������
            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconBurning(true);
        }
    }
    
    /// <summary>
    /// ����� ��������� �������� ������������ ���������� �� ��������� �����
    /// </summary>
    /// <param name="slowdown">�������� ����������</param>
    /// <param name="duration">������������ ������� ����������</param>
    public void SetSlowing(float slowdown, int duration)
    {
        if (!_isSlow && CurrentState.GetType() != typeof(DieState))
        {
            _durationSlow = duration;

            Speed = MaxSpeed * (1f - slowdown);

            _isSlow = true;

            // �������� ������ ���������� ��� �����������
            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconSlowdown(true);
        }
    }

    /// <summary>
    /// ���������� ������ ������ ����� � ��� ������
    /// </summary>
    public void DestroyEnemyObjects()
    {
        Destroy(Weapon);
        Destroy(gameObject);
    }
    #endregion Public methods
}
