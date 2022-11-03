using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxArmor = 0;
    [SerializeField] private float _maxSpeed = 3.5f;

    [Header("State Settings")]
    [SerializeField] private bool _isOnlyIdleState;
    [SerializeField] private bool _isStartPursuitState;

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
        set
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
            float speed = Random.Range(value - 0.4f, value + 0.4f);
            _maxSpeed = speed;
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

    public BoxCollider SummonTrigger { get; set; }

    public State CurrentState { get; private set; }
    public IdleState IdleState { get; private set; }
    public PursuitState PursuitState { get; private set; }
    public AttackIdleState AttackIdleState { get; private set; }
    public DieState DieState { get; private set; }

    /// <summary>
    /// ���� ����� ������ ��������� �����
    /// </summary>
    public bool IsStartPursuitState { get; set; }
    /// <summary>
    /// ��������� ��������� ����� - ������������� (�� ��������� - �����)
    /// </summary>
    public bool IsOnlyIdleState { get; private set; }

    public GameObject Weapon => WeaponController.UsedWeapon;
    #endregion Properties

    #region Private fields
    private int _health = 0;
    private int _armor = 0;

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
        MaxSpeed = _maxSpeed;

        Health = _maxHealth;
        Armor = _maxArmor;

        IsStartPursuitState = _isStartPursuitState;
        IsOnlyIdleState = _isOnlyIdleState;

        SummonTrigger = _summonTrigger;
    }

    private void Start()
    {
        // �������� ������������ ����������
        // ---------------------------------------------------------------
        HitBoxesController = GetComponent<HitBoxesController>();
        RagdollController = GetComponent<RagdollController>();
        WeaponController = GetComponent<WeaponController>();

        IconEffectsController = GetComponentInChildren<IconEffectsController>();

        BurningEffectController = GetComponentInChildren<BurningEffectController>();
        DieEffectController = GetComponentInChildren<DieEffectController>();
        HealthBarController = GetComponentInChildren<HealthBarController>();
        PopupDamageController = GetComponentInChildren<PopupDamageController>();

        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // ---------------------------------------------------------------
        IdleState = new IdleState(this);
        PursuitState = new PursuitState(this);
        AttackIdleState = new AttackIdleState(this);
        DieState = new DieState(this);

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
        // ������ ��������� ���������
        InitializeStartingState(IdleState);
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        CurrentState.Update();

        // TODO ����� ����� ������ �������� ���������� ��� ������ ��������� 
        if (_isBurning)
            Burning();

        if (_isSlow)
            Slowing();
    }
    private void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }

    /// <summary>
    /// ����� ������������� ������ ���������, ���������� CurrentState �������� startingState � ������� ��� ���� Enter. 
    /// ��� �������������� ������ ���������, � ������ ��� ������� �������� ���������.
    /// </summary>
    public void InitializeStartingState(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
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
    #endregion Private methods

    #region Public methods
    public virtual void TakeDamage(int damage, TypeDamage typeDamage)
    {
        if (Health > 0)
        {
            Health -= damage;

            // ���� ����� �������� �����, �������� ���������
            if (CurrentState == IdleState)
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
                ChangeState(PursuitState);
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
            if (CurrentState != DieState)
                ChangeState(DieState);
        }
    }
    public void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage)
    {
        // �������� �������� ����� � ������ ��������� � �� ��� ���� ����� ����
        damage = HitBoxesController.GetDamageValue(damage, hitCollider);
        TakeDamage(damage, typeDamage);
    }

    /// <summary>
    /// ����� ������������ �������� ����� �����������. �� �������� Exit ��� ������� CurrentState ����� ������� ��� ������ �� newState. � ����� �� �������� Enter ��� newState.
    /// </summary>
    /// <param name="newState">����� ���������, ������� ����� ����������</param>
    public void ChangeState(State newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
    /// <summary>
    /// ����� ��������� ����� �� ��������� �����
    /// </summary>
    /// <param name="damagePerSecond">�������� ����� � ��������</param>
    /// <param name="duration">������������ �������</param>
    /// <param name="typeDamage">��� ���������� �����</param>
    public void SetBurning(int damagePerSecond, int duration, TypeDamage typeDamage)
    {
        if (!_isBurning)
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
        if (!_isSlow)
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
