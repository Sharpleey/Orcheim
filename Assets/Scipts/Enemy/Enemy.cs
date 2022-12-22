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
    /// �������� ��������� ����������
    /// </summary>
    public Health Health { get; private set; }

    /// <summary>
    /// ����� ��������� ����������
    /// </summary>
    public Armor Armor { get; private set; }

    /// <summary>
    /// ���� ��������� ����������
    /// </summary>
    public Damage Damage { get; private set; }

    /// <summary>
    /// �������� ������������ ��������� ����������
    /// </summary>
    public MovementSpeed MovementSpeed { get; private set; }

    /// <summary>
    /// �������� ����� ��������� ����������
    /// </summary>
    public AttackSpeed AttackSpeed { get; private set; }

    /// <summary>
    /// ��������� ����� ����������
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
    /// ������ ��������� ���������
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
    /// ������� ��������� �����
    /// </summary>
    public EnemyState CurrentState { get; private protected set; }

    #endregion Properties

    #region Private fields

    /// <summary>
    /// ������� ��� �������� ���������
    /// </summary>
    protected Dictionary<Type, EnemyState> _states;

    /// <summary>
    /// ������� ��� �������� ������ ������� �� ��������� ����������
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
        // �������� ������������ ����������
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

        // ������ ��������� ����������, ����� �� �������� ��������
        if (DieEffectController)
            DieEffectController.enabled = false;

        if (BurningEffectController)
            BurningEffectController.enabled = false;

        // ������������� �������� ��� NavMeshAgent
        NavMeshAgent.speed = MovementSpeed.ActualSpeed;

        // ������������� ������������ � ���������� �� ��� ������ ��
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
    /// ����� �������������� ���������
    /// </summary>
    protected void InitStates()
    {
        _states = new Dictionary<Type, EnemyState>();

        _states[typeof(IdleState)] = new IdleState(this);
        _states[typeof(DieState)] = new DieState(this);
        _states[typeof(PatrollingState)] = new PatrollingState(this);
    }

    /// <summary>
    /// ����� ������������� ������ ��������� �����������
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
    /// ����� ������������ �������� ����� �����������. �� �������� Exit ��� ������� CurrentState ����� ������� ��� ������ �� newState. � ����� �� �������� Enter ��� newState.
    /// </summary>
    /// <typeparam name="T">��� ������ ���������</typeparam>
    protected internal void SetState<T>() where T : EnemyState
    {
        EnemyState newState;

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
    /// ����� ��������� ����� ����������
    /// </summary>
    /// <param name="damage">�������� ������������ �����</param>
    /// <param name="typeDamage">��� �����</param>
    public void TakeDamage(int damage, DamageType damageType, bool isArmorIgnore, Collider hitbox = null)
    {
        if (Health.ActualHealth > 0)
        {
            if(hitbox)
            {
                // �������� �������� ����� � ����������� �� ����������� ��������
                damage = HitBoxesController.GetDamageValue(damage, hitbox);
            }

            if(!isArmorIgnore)
            {
                // �������� ���������� �����
                float increaseDamage = 1.0f - (Armor.ActualArmor / (100.0f + Armor.ActualArmor));

                // ����������� ���� �� ���� �����
                damage = (int)(damage * increaseDamage);
            }

            Health.ActualHealth -= damage;

            // ���� ����� �������� �����, �������� ���������
            if (CurrentState.GetType() == typeof(IdleState) || CurrentState.GetType() == typeof(PatrollingState))
            {

                WaveEventManager.StartingTrigger();
     
                // �������� ��������� �� �������������
                SetState<ChasingState>();
            }

            // ����������� �����
            if (PopupDamageController != null)
                PopupDamageController.ShowPopupDamage(damage, damageType);

            // ������ ��������
            if (HealthBarController != null)
            {
                HealthBarController.SetHealth(Health.ActualHealth);
                HealthBarController.ShowHealthBar();
            }

            // ����
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
    /// ����� �������������/����������� ������ �� ���������
    /// </summary>
    /// <param name="effect">������, ������� ����� ����������</param>
    public void SetEffect(Effect effect)
    {
        // ���� �������� � ��������� ������ � ������ ������ ����  ��� ����������, �� ��������� ����������
        if (CurrentState.GetType() == typeof(DieState) || _activeEffects.ContainsKey(effect.GetType())) // 10
            return;

        // �������� ������
        Effect newEffect = effect.DeepCopy(this); // 14

        // ��������� ������
        newEffect.Enable(); // 2

        // ���� ������ ����� ������������, �� ��������� �������� � �������� � � ������������� ������������, ���� �� ����
        if (newEffect.Duration > 0)
            StartCoroutine(newEffect.CoroutineEffect); // 2700+

        // ��������� ������ � ������� ��� ��������
        _activeEffects.Add(newEffect.GetType(), newEffect); // 9
    }

    /// <summary>
    /// ����� ������� ������ ������������� �� ���������
    /// </summary>
    /// <param name="effect">������ ������� ����� �������, ������ ��� �������</param>
    public void RemoveEffect(Effect effect)
    {
        // ���� ������ ��� ������� ���������� � �������, �� ������� ���
        if (_activeEffects.ContainsKey(effect.GetType()))
        {
            // ������� ��������� ������� �� ��������� ������� ��� ���������, � �����������, ��� ������ ������
            effect.Disable();

            // ������� ������ �� �������
            _activeEffects.Remove(effect.GetType());
        }
    }

    /// <summary>
    /// ���������� ������ �����
    /// </summary>
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    #endregion Public methods
}
