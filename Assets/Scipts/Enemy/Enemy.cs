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
    /// Здоровье персонажа противника
    /// </summary>
    public Health Health { get; private set; }

    /// <summary>
    /// Броня персонажа противника
    /// </summary>
    public Armor Armor { get; private set; }

    /// <summary>
    /// Урон персонажа противника
    /// </summary>
    public Damage Damage { get; private set; }

    /// <summary>
    /// Скорость передвижения персонажа противника
    /// </summary>
    public MovementSpeed MovementSpeed { get; private set; }

    /// <summary>
    /// Скорость атаки персонажа противника
    /// </summary>
    public AttackSpeed AttackSpeed { get; private set; }

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

    #endregion Properties

    #region Private fields

    /// <summary>
    /// Словарь для хранения состояний
    /// </summary>
    protected Dictionary<Type, EnemyState> _states;

    /// <summary>
    /// Словарь для хранения эффект висящих на персонаже противника
    /// </summary>
    protected Dictionary<Type, Effect> _activeEffects;

    #endregion Private fields

    #region Mono
    private void Awake()
    {
        InitParameters();


        //MaxSpeed = _maxMovementSpeed;


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
        NavMeshAgent.speed = MovementSpeed.ActualSpeed;

        // Устанавливаем максимальное и актуальное хп для полосы хп
        HealthBarController?.SetMaxHealth(Health.MaxHealth);
        HealthBarController?.SetHealth(Health.ActualHealth);
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

    private void InitParameters()
    {
        Health = new Health(80, 20);
        Armor = new Armor(5, 2);
        Damage = new Damage(25, 5, DamageType.Physical, false);
        MovementSpeed = new MovementSpeed(3.5f);
        AttackSpeed = new AttackSpeed(100);
    }

    /// <summary>
    /// Метод получения урона персонажем
    /// </summary>
    /// <param name="damage">Значение принимаемого урона</param>
    /// <param name="typeDamage">Тип урона</param>
    public void TakeDamage(int damage, DamageType damageType, bool isArmorIgnore, Collider hitbox = null)
    {
        if (Health.ActualHealth > 0)
        {
            if(hitbox)
            {
                // Изменяем значение урона в зависимости от попадаемого хитбокса
                damage = HitBoxesController.GetDamageValue(damage, hitbox);
            }

            if(!isArmorIgnore)
            {
                // Значение уменьшения урона
                float increaseDamage = 1.0f - (Armor.ActualArmor / (100.0f + Armor.ActualArmor));

                // Уменьшенный урон за счет брони
                damage = (int)(damage * increaseDamage);
            }

            Health.ActualHealth -= damage;

            // Если игрок атаковал врага, изменяем состояние
            if (CurrentState.GetType() == typeof(IdleState) || CurrentState.GetType() == typeof(PatrollingState))
            {

                WaveEventManager.StartingTrigger();
     
                // Изменяем состояние на преследование
                SetState<ChasingState>();
            }

            // Всплывающий дамаг
            if (PopupDamageController != null)
                PopupDamageController.ShowPopupDamage(damage, damageType);

            // Полоса здоровья
            if (HealthBarController != null)
            {
                HealthBarController.SetHealth(Health.ActualHealth);
                HealthBarController.ShowHealthBar();
            }

            // Звук
            if (AudioController)
            {
                AudioController.PlayRandomSoundWithProbability(EnemySoundType.Hit);
            }
        }

        if (Health.ActualHealth <= 0)
        {
            if (CurrentState.GetType() != typeof(DieState))
                SetState<DieState>();
        }
    }

    /// <summary>
    /// Метод устанавливает/накладывает эффект на персонажа
    /// </summary>
    /// <param name="effect">Эффект, который хотим установить</param>
    public void SetEffect(Effect effect)
    {
        // Если персонаж в состоянии смерти и эффект такого типа  уже установлен, то прерываем выполнение
        if (CurrentState.GetType() == typeof(DieState) || _activeEffects.ContainsKey(effect.GetType())) // 10
            return;

        // Копируем эффект
        Effect newEffect = effect.DeepCopy(this); // 14

        // Применяем эффект
        newEffect.Enable(); // 2

        // Если эффект иммет длительность, то запускаем корутину с таймером и с переодическим воздействием, если он есть
        if (newEffect.Duration > 0)
            StartCoroutine(newEffect.CoroutineEffect); // 2700+

        // Добавляем эффект в словарь для хранения
        _activeEffects.Add(newEffect.GetType(), newEffect); // 9
    }

    /// <summary>
    /// Метод снимает эффект установленный на персонаже
    /// </summary>
    /// <param name="effect">Эффект который хотим удалить, точнее тип эффекта</param>
    public void RemoveEffect(Effect effect)
    {
        // Если данный тип эффекта содержится в словаре, то удаляем его
        if (_activeEffects.ContainsKey(effect.GetType()))
        {
            // Удаляем изменения эффекта по окончанию эффекта или применяем, в зависимости, что эффект делает
            effect.Disable();

            // Удаляем эффект из словаря
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
