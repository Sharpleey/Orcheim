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
    /// Максимальное значение брони противника
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
    /// Максимальное значение скорости передвижения противника
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
            float speed = UnityEngine.Random.Range(value - 0.4f, value + 0.4f);
            _maxSpeed = speed;
        }
    }
    
    /// <summary>
    /// Актуальная скорость противника, скорость передвижения в NavMeshAgent
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
    
    /// <summary>
    /// Актуальная броня противника
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

    /// <summary>
    /// Текущее состояние врага
    /// </summary>
    public State CurrentState { get; private set; }

    /// <summary>
    /// Начальное состояние врага - преследование (По умолчанию - покоя)
    /// </summary>
    public bool IsStartPursuitState { get; set; }
    
    /// <summary>
    /// Враг имеет только состояние покоя
    /// </summary>
    public bool IsOnlyIdleState { get; private set; }

    public GameObject Weapon => WeaponController.UsedWeapon;
    #endregion Properties

    #region Private fields
    /// <summary>
    /// Словарь для хранения состояний
    /// </summary>
    private Dictionary<Type, State> _states;

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
        // Получаем контроллерыи компоненты
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

        // Делаем компонент неактивным, чтобы не началась анимация
        if (DieEffectController)
            DieEffectController.enabled = false;

        if (BurningEffectController)
            BurningEffectController.enabled = false;

        // Устанавливаем скорость для NavMeshAgent
        Speed = _maxSpeed;

        // Устанавливаем максимальное и актуальное хп для полосы хп
        HealthBarController?.SetMaxHealth(MaxHealth);
        HealthBarController?.SetHealth(Health);

        // Инициализируем состояния
        InitStates();
        // Задаем состояние поумолчанию
        SetStateByDefault();
    }
    
    private void Update()
    {
        CurrentState.Update();

        // TODO когда будет больше эффектов переделать под машину состояний 
        if (_isBurning)
            Burning();

        if (_isSlow)
            Slowing();
    }
    #endregion Mono

    #region Private methods
    /// <summary>
    /// Метод инициализирует состояния
    /// </summary>
    private void InitStates()
    {
        _states = new Dictionary<Type, State>();

        _states[typeof(IdleState)] = new IdleState(this);
        _states[typeof(PursuitState)] = new PursuitState(this);
        _states[typeof(AttackIdleState)] = new AttackIdleState(this);
        _states[typeof(DieState)] = new DieState(this);
    }

    /// <summary>
    /// Метод обрабатывает переходы между состояниями. Он вызывает Exit для старого CurrentState перед заменой его ссылки на newState. В конце он вызывает Enter для newState.
    /// </summary>
    /// <param name="newState">Новое состояние, которое хотим установить</param>
    private void SetState(State newState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = newState;
        CurrentState.Enter();
    }
    
    /// <summary>
    /// Метод вытаскивает нужное состояние из словаря
    /// </summary>
    /// <typeparam name="T">Тип состояния</typeparam>
    /// <returns></returns>
    private State GetState<T>() where T : State
    {
        var type = typeof(T);
        return _states[type];
    }
    
    /// <summary>
    /// Метод устанавливает первое состояние поумолчанию
    /// </summary>
    private void SetStateByDefault()
    {
        SetIdleState();
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
    
    /// <summary>
    /// Метод отвечает за эффект замедления, его длительность
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
    #endregion Private methods

    #region Public methods
    /// <summary>
    /// Устанавливает состояние покоя
    /// </summary>
    public void SetIdleState()
    {
        State state = GetState<IdleState>();
        SetState(state);
    }

    /// <summary>
    /// Устанавливает состояние преследования
    /// </summary>
    public void SetPursuitState()
    {
        State state = GetState<PursuitState>();
        SetState(state);
    }

    /// <summary>
    /// Устанавливает состояние атаки
    /// </summary>
    public void SetAttackIdleState()
    {
        State state = GetState<AttackIdleState>();
        SetState(state);
    }

    /// <summary>
    /// Устанавливаем состояние смерти
    /// </summary>
    public void SetDieState()
    {
        State state = GetState<DieState>();
        SetState(state);
    }
    
    /// <summary>
    /// Метод получения урона персонажем
    /// </summary>
    /// <param name="damage">Значение принимаемого урона</param>
    /// <param name="typeDamage">Тип урона</param>
    public virtual void TakeDamage(int damage, TypeDamage typeDamage)
    {
        if (Health > 0)
        {
            Health -= damage;

            // Если игрок атаковал врага, изменяем состояние
            if (CurrentState.GetType() == typeof(IdleState))
            {
                // Рассылаем событие, по сути являетс первым тригером для начала игры
                try
                {
                    Messenger.Broadcast(GlobalGameEvent.FIRST_TRIGGER_GAME);
                }
                catch
                {
                     
                }

                // Изменяем состояние на преследование
                SetPursuitState();
            }

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
            if (CurrentState.GetType() != typeof(DieState))
                SetDieState();
        }
    }
    
    /// <summary>
    /// Получение урона через определенный хитбокс персонажа
    /// </summary>
    /// <param name="damage">Значение получаемого урона</param>
    /// <param name="hitCollider">Хитбокс попадания</param>
    /// <param name="typeDamage">Тип урона</param>
    public void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage)
    {
        // Получаем значение урона с учетом попадания в ту или иную часть тела
        damage = HitBoxesController.GetDamageValue(damage, hitCollider);
        TakeDamage(damage, typeDamage);
    }

    /// <summary>
    /// Метод полжигает врага на некоторое время
    /// </summary>
    /// <param name="damagePerSecond">Значения урона в секунгду</param>
    /// <param name="duration">Длительность горения</param>
    /// <param name="typeDamage">Тип наносимого урона</param>
    public void SetBurning(int damagePerSecond, int duration, TypeDamage typeDamage)
    {
        if (!_isBurning && CurrentState.GetType() != typeof(DieState))
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
    
    /// <summary>
    /// Метод замедляет скорость передвижения противника на некоторое время
    /// </summary>
    /// <param name="slowdown">Значение замедления</param>
    /// <param name="duration">Длительность эффекьа замедления</param>
    public void SetSlowing(float slowdown, int duration)
    {
        if (!_isSlow && CurrentState.GetType() != typeof(DieState))
        {
            _durationSlow = duration;

            Speed = MaxSpeed * (1f - slowdown);

            _isSlow = true;

            // Включаем иконку замедления над противником
            if (IconEffectsController != null)
                IconEffectsController.SetActiveIconSlowdown(true);
        }
    }
    
    /// <summary>
    /// Уничтожает объект оружия врага и его самого
    /// </summary>
    public void DestroyEnemyObjects()
    {
        Destroy(Weapon);
        Destroy(gameObject);
    }
    #endregion Public methods
}
