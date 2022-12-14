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
    [SerializeField, Min(100)] private int _maxHealth = 150;
    [SerializeField, Min(0)] private int _maxArmor = 0;
    [SerializeField, Min(1)] private float _maxMovementSpeed = 3.5f;
    [SerializeField, Min(1)] private int _averageDamage = 25;
    [SerializeField, Min(1)] private float _attackDistance = 2.5f;

    [Header("State Settings")]
    [SerializeField] private String _curNammeState;
    [SerializeField] private StartStateType  _defaultState; 

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
    /// Максимальное значение скорости передвижения противника
    /// </summary>
    public float MaxSpeed
    {
        get
        {
            return _maxMovementSpeed;
        }
        set
        {
            if (value < 0.1f)
            {
                _maxMovementSpeed = 0.1f;
                return;
            }
            _maxMovementSpeed = value;
        }
    }

    /// <summary>
    /// Исходный средний урон. Изменять только при прокачке модификатора или в инспекторе
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
    /// Дистанция атаки противника
    /// </summary>
    public float AttackDistance
    {
        get
        {
            return _attackDistance;
        }
        protected set
        {
            if (value < 1)
            {
                _attackDistance = 1;
                return;
            }
            _attackDistance = value;
        }
    }

    /// <summary>
    /// Текущий (Используемый) средний урон. Изменяется для уменьшении разными модификаторами
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
    /// Урон с учетом разброса, данное значение используем при нанесении урона
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
    /// Текущее значение здоровья противника
    /// </summary>
    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
           
            if (HealthBarController != null)
            {
                HealthBarController.SetHealth(_health);
                HealthBarController.ShowHealthBar();
            }
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
    /// Флаг для блокировки изменения состояния
    /// </summary>
    //public bool IsBlockChangeState { get; set; }

    /// <summary>
    /// Первое стартовое состояние
    /// </summary>
    public StartStateType  DefaultState { get => _defaultState; set => _defaultState = value; }

    public HitBoxesController HitBoxesController { get; private set; }
    public RagdollController RagdollController { get; private set; }
    public HealthBarController HealthBarController { get; private set; }
    public PopupDamageController PopupDamageController { get; private set; }
    public WeaponController WeaponController { get; private set; }
    public EnemyAudioController AudioController { get; private set; }
    public DieEffectController DieEffectController { get; private set; }
    public BurningEffectController BurningEffectController { get; private set; }
    public IconEffectsController IconEffectsController { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }

    public BoxCollider SummonTrigger { get; private set; }

    /// <summary>
    /// Текущее состояние врага
    /// </summary>
    public EnemyState CurrentState { get; private protected set; }

    //public List<Effect> ActiveEffects { get; private set; }

    #endregion Properties

    #region Private fields
    /// <summary>
    /// Словарь для хранения состояний
    /// </summary>
    protected Dictionary<Type, EnemyState> _states;
    protected Dictionary<Type, Effect> _activeEffects;


    private int _health = 0;
    private int _armor = 0;

    private int _currentAverageDamage = 0;

    private bool _isBurning = false;
    private float _timerBurning = 0;
    private int _durationBurning = 0;

    private bool _isSlow = false;
    private float _timerSlow = 0;
    private int _durationSlow = 0;

    //private Vector3 _position = Vector3.zero;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        MaxHealth = _maxHealth;
        MaxArmor = _maxArmor;
        AverageDamage = _averageDamage;
        CurrentAverageDamage = _averageDamage;
        MaxSpeed = _maxMovementSpeed;

        Health = _maxHealth;
        Armor = _maxArmor;

        DefaultState = _defaultState;

        SummonTrigger = _summonTrigger;
    }

    protected void Start()
    {
        _activeEffects = new Dictionary<Type, Effect>();
        // Получаем контроллерыи компоненты
        // ---------------------------------------------------------------
        HitBoxesController = GetComponent<HitBoxesController>();
        RagdollController = GetComponent<RagdollController>();
        WeaponController = GetComponent<WeaponController>();
        AudioController = GetComponent<EnemyAudioController>();

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
        Speed = _maxMovementSpeed;

        // Устанавливаем максимальное и актуальное хп для полосы хп
        HealthBarController?.SetMaxHealth(MaxHealth);
        HealthBarController?.SetHealth(Health);
    }
    
    private void Update()
    {
        if (CurrentState != null)
            CurrentState?.Update();
    }
    #endregion Mono

    #region Private methods
    /// <summary>
    /// Метод инициализирует состояния
    /// </summary>
    protected void InitStates()
    {
        _states = new Dictionary<Type, EnemyState>();

        _states[typeof(IdleState)] = new IdleState(this);
        _states[typeof(DieState)] = new DieState(this);
        _states[typeof(PatrollingState)] = new PatrollingState(this);
    }

    /// <summary>
    /// Метод устанавливает первое состояние поумолчанию
    /// </summary>
    protected void SetStateByDefault()
    {
        switch(DefaultState)
        {
            case StartStateType.Chasing:
                SetState<ChasingState>();
                break;
            case StartStateType.Patrolling:
                SetState<PatrollingState>();
                break;
            default:
                SetState<IdleState>();
                break;
        }
    }

    /// <summary>
    /// Метод обрабатывает переходы между состояниями. Он вызывает Exit для старого CurrentState перед заменой его ссылки на newState. В конце он вызывает Enter для newState.
    /// </summary>
    /// <typeparam name="T">Тип класса состояния</typeparam>
    protected internal void SetState<T>() where T : EnemyState
    {
        EnemyState newState;

        try
        {
            newState = _states[typeof(T)];
        }
        catch
        {
            Debug.Log("Состояние "+ typeof(T).ToString() + " у данного вида врага отсутсвует!");
            return;
        }

        if (CurrentState != null)
            CurrentState?.Exit();

        CurrentState = newState;
        CurrentState?.Enter();

        _curNammeState = CurrentState.GetType().ToString();
    }
    #endregion Private methods

    #region Public methods
    /// <summary>
    /// Метод получения урона персонажем
    /// </summary>
    /// <param name="damage">Значение принимаемого урона</param>
    /// <param name="typeDamage">Тип урона</param>
    public void TakeDamage(int damage, TypeDamage typeDamage)
    {
        if (Health > 0)
        {
            Health -= damage;

            // Если игрок атаковал врага, изменяем состояние
            if (CurrentState.GetType() == typeof(IdleState) || CurrentState.GetType() == typeof(PatrollingState))
            {

                WaveEventManager.StartingTrigger();
     
                // Изменяем состояние на преследование
                SetState<ChasingState>();
            }

            // Всплывающий дамаг
            if (PopupDamageController != null)
                PopupDamageController.ShowPopupDamage(damage, typeDamage);
        }

        // Звук
        if (AudioController && Health > 0)
        {
            AudioController.PlayRandomSoundWithProbability(EnemySoundType.Hit);
        }

        if (Health <= 0)
        {
            if (CurrentState.GetType() != typeof(DieState))
                SetState<DieState>();
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

    public void SetEffect(Effect effect)
    {
        if (CurrentState.GetType() == typeof(DieState) || _activeEffects.ContainsKey(effect.GetType())) // 10
            return;

        Effect newEffect = effect.DeepCopy(this); // 14

        newEffect.Enable(); // 2

        if (newEffect.Duration != 0)
            StartCoroutine(newEffect.CoroutineEffect); // 2700+

        _activeEffects.Add(newEffect.GetType(), newEffect); // 9
    }

    public void RemoveEffect(Effect effect)
    {
        if (_activeEffects.ContainsKey(effect.GetType()))
        {
            effect.Disable();

            _activeEffects.Remove(effect.GetType());
        }
    }

    /// <summary>
    /// Уничтожает объект врага
    /// </summary>
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    #endregion Public methods
}
