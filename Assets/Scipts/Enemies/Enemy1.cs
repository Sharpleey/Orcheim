using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Enemy1 : MonoBehaviour, IEnemy
{
    #region Serialize fields
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;
    [SerializeField] private float _speed = 3.5f;
    #endregion Serialize fields

    #region Properties
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
                navMeshAgent.speed = 0.1f;
                return;
            }
            navMeshAgent.speed = value;
            _speed = value;
        }
    }
    public float CurrentSpeed
    {
        get
        {
            return navMeshAgent.velocity.magnitude;
        }
    }
    #endregion Properties

    #region Private fields
    private bool _isBurning = false;
    private float _timerBurning = 0f;
    private int _durationBurning = 0;

    private HitBoxesController _hitBoxesController;
    private RagdollController _ragdollController;
    private HealthBarController _healthBarController;
    private PopupDamageController _popupDamageController;

    private DieEffectController _dieEffectController;
    private BurningEffectController _burningEffectController;

    private IconEffectsController _iconEffectsController;

    private StateMachineEnemy _stateMachineEnemy;


    #endregion Private fields

    #region Public fields
    [HideInInspector] public NavMeshAgent navMeshAgent;

    [HideInInspector] public Animator animator;

    public IdleState idleState;
    public PursuitState pursuitState;
    public AttackState attackState;
    #endregion Public fields

    #region Mono
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        MaxHealth = _maxHealth;
        Health = MaxHealth;
        Speed = _speed;
    }

    private void Start()
    {
        // Получаем контроллерыи компоненты
        // ---------------------------------------------------------------
        _hitBoxesController = GetComponent<HitBoxesController>();
        _ragdollController = GetComponent<RagdollController>();

        _iconEffectsController = GetComponentInChildren<IconEffectsController>();

        _burningEffectController = GetComponentInChildren<BurningEffectController>();
        _dieEffectController = GetComponentInChildren<DieEffectController>();
        _healthBarController = GetComponentInChildren<HealthBarController>();
        _popupDamageController = GetComponentInChildren<PopupDamageController>();

        _stateMachineEnemy = GetComponent<StateMachineEnemy>();

        animator = GetComponent<Animator>();
        // ---------------------------------------------------------------

        // Делаем компонент неактивным, чтобы не началась анимация
        if (_dieEffectController != null)
            _dieEffectController.enabled = false;

        if (_burningEffectController != null)
            _burningEffectController.enabled = false;

        // Устанавливаем максимальное и актуальное хп для полосы хп
        if (_healthBarController != null)
        {
            _healthBarController.SetMaxHealth(MaxHealth);
            _healthBarController.SetHealth(Health);
        }

        // Инициализируем состояния и задаем начальное состояние
        if (_stateMachineEnemy != null)
        {
            idleState = new IdleState(this, _stateMachineEnemy);
            pursuitState = new PursuitState(this, _stateMachineEnemy);
            attackState = new AttackState(this, _stateMachineEnemy);

            // Задаем начальное состояние
            _stateMachineEnemy.InitializeStartingState(idleState);
        }
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (_stateMachineEnemy != null)
            _stateMachineEnemy.CurrentState.Update();

        // TODO когда будет больше эффектов переделать под машину состояний 
        if (_isBurning)
        {
            if (_timerBurning < _durationBurning+1)
            {
                _timerBurning += Time.deltaTime;
            }
            else
            {
                _timerBurning = 0;
                _isBurning = false;
                _burningEffectController.enabled = false;

                if (_iconEffectsController != null)
                    _iconEffectsController.SetActiveIconBurning(false);
            }
        }
    }
    private void FixedUpdate()
    {
        if (_stateMachineEnemy != null)
            _stateMachineEnemy.CurrentState.FixedUpdate();
    }
    private IEnumerator Die()
    {
        _ragdollController.MakePhysical();

        if (_burningEffectController != null)
            _burningEffectController.enabled = false;

        if (_healthBarController != null)
            _healthBarController.SetActiveHealthBar(false);

        if (_iconEffectsController != null)
            _iconEffectsController.DeactivateAllIcons();

        if (navMeshAgent != null)
            navMeshAgent.enabled = false;

        yield return new WaitForSeconds(2);

        //if (_hitBoxesController)
        //    _hitBoxesController.OnLayersAllColliders();

        if (_dieEffectController != null)
            _dieEffectController.enabled = true;

        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }

    #endregion Private methods

    #region Public methods
    public void TakeDamage(int damage, TypeDamage typeDamage)
    {
        if (Health > 0)
        {
            Health -= damage;

            // Всплывающий дамаг
            if (_popupDamageController != null)
                _popupDamageController.ShowPopupDamage(damage, typeDamage);

            // Полоска хп
            if (_healthBarController != null)
            {
                _healthBarController.SetHealth(Health);
                _healthBarController.ShowHealthBar();
            }
        }

        if (Health <= 0)
            StartCoroutine(Die());
    }
    public void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage)
    {
        // Получаем значение урона с учетом попадания в ту или иную часть тела
        damage = _hitBoxesController.GetDamageValue(damage, hitCollider);
        TakeDamage(damage, typeDamage);
    }
    public void SetBurning(int damagePerSecond, int duration, TypeDamage typeDamage)
    {
        if (!_isBurning)
        {
            _durationBurning = duration;

            _burningEffectController.DamagePerSecond = damagePerSecond;
            _burningEffectController.TypeDamage = typeDamage;

            _isBurning = true;
            _burningEffectController.enabled = true;

            // Включаем иконку горения над противником
            if (_iconEffectsController != null)
                _iconEffectsController.SetActiveIconBurning(true);
        }
    }
    #endregion Public methods
}
