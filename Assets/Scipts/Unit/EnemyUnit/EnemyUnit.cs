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
    [Header("��������� ���������")]
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
    public DieEffectController DieEffectController { get; private set; } //TODO ������� ����� ����� ���������� ���������� ��������
    public BurningEffectController BurningEffectController { get; private set; } //TODO ������� ����� ����� ���������� ���������� ��������
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
            Debug.Log("��������� " + typeof(T).ToString() + " � ������� ���� ����� ����������!");
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
        // ������ ��������� ����������, ����� �� �������� ��������
        if (DieEffectController)
            DieEffectController.enabled = false;

        if (BurningEffectController)
            BurningEffectController.enabled = false;

        if(NavMeshAgent)
        {
            // ������������� �������� ��� NavMeshAgent
            NavMeshAgent.speed = MovementSpeed.Actual / 100f;
        }

        // ������������� ������������ � ���������� �� ��� ������ ��
        HealthBarController?.SetDefaultParameters(Health.Max, Health.Actual);

        // ��������� �������
        SummonTrigger?.SetEnable(false);
    }

    #endregion Private methods

    #region Public methods

    public override void InitParameters()
    {
        base.InitParameters();

        DefaultState = _defaultState; //?? ���� ��

        if(_unitConfig is EnemyUnitConfig enemyUnitConfig)
        {
            CostInGold = new Parameter(enemyUnitConfig.Gold, enemyUnitConfig.IncreaseGold);
            CostInExp = new Parameter(enemyUnitConfig.Experience, enemyUnitConfig.IncreaseExperience);

            AttackDistance = enemyUnitConfig.AttackDistance;
        }
    }

    public override void SetEffect(Effect effect)
    {
        // ���� �������� � ��������� ������ � ������ ������ ����  ��� ����������, �� ��������� ����������
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
                // �������� �������� ����� � ����������� �� ����������� ��������
                damage = HitBoxesController.GetDamageValue(damage, hitBox);
            }

            if (!isArmorIgnore)
            {
                // ��������� ����� ���������� ����� �����
                float increaseDamage = 1.0f - (Armor.Actual / (100.0f + Armor.Actual));

                // ����������� ���� �� ���� �����
                damage *= increaseDamage;
            }

            Health.Actual -= damage;

            // ���� ����� �������� �����, �������� ���������
            if (CurrentState.GetType() == typeof(IdleState) || CurrentState.GetType() == typeof(PatrollingState))
            {
                //SummonTrigger.SummonNearbyUnits(8);

                // �������� ��������� �� �������������
                SetState<ChasingState>();
            }

            // ����������� �����
            if (PopupDamageController != null)
                PopupDamageController.ShowPopupDamage(damage, isCriticalHit, damageType);

            // ������ ��������
            if (HealthBarController != null)
            {
                HealthBarController.SetHealth(Health.Actual, true);
                HealthBarController.ShowHealthBar();
            }

            // ����
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
    /// ���������� ������ �����
    /// </summary>
    public void DestroyUnit()
    {
        Destroy(gameObject); //TODO ����� Pool objects
    }

    #endregion Public methods
}
