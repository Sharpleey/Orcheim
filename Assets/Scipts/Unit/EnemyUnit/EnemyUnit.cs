using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]
[RequireComponent(typeof(EnemyWeaponController))]
[RequireComponent(typeof(EnemyAudioController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyUnit : Unit, IEnemyUnitParameters, IStateMachine
{
    #region Serialize fields
    [Header("Начальнео состояние")]
    [SerializeField] private StartStateType _defaultState;

    #endregion Serialize fields

    #region Properties

    public float AttackDistance { get; private set; }
    public Parameter CostInGold { get; private set; }
    public Parameter CostInExp { get; private set; }


    #region State machine
    public StartStateType DefaultState { get => _defaultState; set => _defaultState = value; }
    public Dictionary<Type, EnemyState> States { get; private set; }
    public EnemyState CurrentState { get; private set; }
    #endregion State machine

    #region Controllers
    public HitBoxesController HitBoxesController { get; private set; }
    public RagdollController RagdollController { get; private set; }
    public HealthBarController HealthBarController { get; private set; }
    public PopupDamageController PopupDamageController { get; private set; }
    public EnemyWeaponController WeaponController { get; private set; }
    public EnemyAudioController AudioController { get; private set; }
    public DieEffectController DieEffectController { get; private set; } //TODO Сделать через одинт контроллер визуальных эффектов
    public BurningEffectController BurningEffectController { get; private set; } //TODO Сделать через одинт контроллер визуальных эффектов
    public IconEffectsController IconEffectsController { get; private set; }
    public SummonTrigger SummonTrigger { get; private set; }

    #endregion 

    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }

    #endregion Properties

    #region Mono

    protected override void Awake()
    {
        InitStates();

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        SetStateByDefault();
    }

    private void Update()
    {
        if (CurrentState != null)
            CurrentState?.Update();
    }

    #endregion Mono

    #region State machine

    public virtual void InitStates()
    {
        States = new Dictionary<Type, EnemyState>();

        States[typeof(IdleState)] = new IdleState(this);
        States[typeof(DieState)] = new DieState(this);
        States[typeof(PatrollingState)] = new PatrollingState(this);
        States[typeof(NeutralState)] = new NeutralState(this);
    }

    public void SetStateByDefault()
    {
        switch (DefaultState)
        {
            case StartStateType.Chasing:
                SetState<ChasingState>();
                break;
            case StartStateType.Patrolling:
                SetState<PatrollingState>();
                break;
            case StartStateType.Neutral:
                SetState<NeutralState>();
                break;
            default:
                SetState<IdleState>();
                break;
        }
    }

    public void SetState<T>() where T : EnemyState
    {
        EnemyState newState;

        if (!States.TryGetValue(typeof(T), out newState))
        {
            Debug.Log("Состояние " + typeof(T).ToString() + " у данного вида юнита отсутсвует!");
            return;
        }

        if (CurrentState != null)
            CurrentState?.Exit();

        CurrentState = newState;
        CurrentState?.Enter();
    }

    #endregion State machine

    #region Private methods

    protected override void InitControllers()
    {
        HitBoxesController = GetComponent<HitBoxesController>();
        RagdollController = GetComponent<RagdollController>();
        WeaponController = GetComponent<EnemyWeaponController>();
        AudioController = GetComponent<EnemyAudioController>();

        IconEffectsController = GetComponentInChildren<IconEffectsController>();

        BurningEffectController = GetComponentInChildren<BurningEffectController>();
        DieEffectController = GetComponentInChildren<DieEffectController>();
        HealthBarController = GetComponentInChildren<HealthBarController>();
        PopupDamageController = GetComponentInChildren<PopupDamageController>();

        SummonTrigger = GetComponentInChildren<SummonTrigger>();

        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void InitControllersParameters()
    {
        // Делаем компонент неактивным, чтобы не началась анимация
        if (DieEffectController)
            DieEffectController.enabled = false;

        if (BurningEffectController)
            BurningEffectController.enabled = false;

        if(NavMeshAgent)
        {
            // Устанавливаем скорость для NavMeshAgent
            NavMeshAgent.speed = MovementSpeed.Actual / 100f;
        }

        // Устанавливаем максимальное и актуальное хп для полосы хп
        HealthBarController?.SetDefaultParameters(Health.Max, Health.Actual);

        // Отключаем триггер
        SummonTrigger?.SetEnable(false);
    }

    #endregion Private methods

    #region Public methods

    public override void InitParameters()
    {
        base.InitParameters();

        DefaultState = _defaultState; //?? надо ли

        if(_unitConfig is EnemyUnitConfig enemyUnitConfig)
        {
            CostInGold = new Parameter(enemyUnitConfig.Gold, enemyUnitConfig.IncreaseGold);
            CostInExp = new Parameter(enemyUnitConfig.Experience, enemyUnitConfig.IncreaseExperience);

            AttackDistance = enemyUnitConfig.AttackDistance;
        }
    }

    public override void SetEffect(Effect effect)
    {
        // Если персонаж в состоянии смерти и эффект такого типа  уже установлен, то прерываем выполнение
        if (CurrentState.GetType() == typeof(DieState) || ActiveEffects.ContainsKey(effect.GetType())) // 10
            return;

        base.SetEffect(effect);
    }

    public override void TakeDamage(float damage, DamageType damageType, bool isArmorIgnore = false, bool isCriticalHit = false, Collider hitBox = null)
    {
        if (Health.Actual > 0)
        {
            if (hitBox)
            {
                // Изменяем значение урона в зависимости от попадаемого хитбокса
                damage = HitBoxesController.GetDamageValue(damage, hitBox);
            }

            if (!isArmorIgnore)
            {
                // Множитель урона прошедшего через броню
                float increaseDamage = 1.0f - (Armor.Actual / (100.0f + Armor.Actual));

                // Уменьшенный урон за счет брони
                damage *= increaseDamage;
            }

            Health.Actual -= damage;

            // Если игрок атаковал врага, изменяем состояние
            if (CurrentState.GetType() == typeof(IdleState) || CurrentState.GetType() == typeof(PatrollingState))
            {
                //SummonTrigger.SummonNearbyUnits(8);

                // Изменяем состояние на преследование
                SetState<ChasingState>();
            }

            // Всплывающий дамаг
            if (PopupDamageController != null)
                PopupDamageController.ShowPopupDamage(damage, isCriticalHit, damageType);

            // Полоса здоровья
            if (HealthBarController != null)
            {
                HealthBarController.SetHealth(Health.Actual, true);
                HealthBarController.ShowHealthBar();
            }

            // Звук
            if (AudioController)
            {
                AudioController.PlayRandomSoundWithProbability(EnemySoundType.Hit);
            }
        }

        if (Health.Actual <= 0)
        {

            if (CurrentState.GetType() != typeof(DieState))
            {
                SetState<DieState>();
            }
        }
    }

    public override void LevelUp(int levelUp = 1)
    {
        base.LevelUp(levelUp);
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);
    }

    /// <summary>
    /// Уничтожает объект врага
    /// </summary>
    public void DestroyUnit()
    {
        Destroy(gameObject); //TODO Через Pool objects
    }

    #endregion Public methods
}
