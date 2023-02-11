using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnitLevel, IAttacking, IDamageable, IUsesAttackModifiers, IUnitParameters, IInfluenceOfEffects, IUsesAbilities
{
    #region Serialize fields

    [Header("������ � ����������� �����")]
    [SerializeField] protected UnitConfig _unitConfig;

    #endregion Serialize fields

    #region Properties

    private int _level;
    public int Level
    {
        get => _level;
        protected set => _level = Mathf.Clamp(value, 1, int.MaxValue);
    }

    #region Unit parameters
    public Health Health { get; protected set; }
    public Armor Armor { get; protected set; }
    public Damage Damage { get; protected set; }
    public MovementSpeed MovementSpeed { get; protected set; }
    public AttackSpeed AttackSpeed { get; protected set; }
    #endregion

    #region Attack modifaers
    public CriticalAttack CriticalAttack { get; protected set; } //TODO ��������� � �������
    public FlameAttack FlameAttack { get; protected set; } //TODO ��������� � �������
    public SlowAttack SlowAttack { get; protected set; } //TODO ��������� � �������
    public PenetrationProjectile PenetrationProjectile { get; protected set; } //TODO ��������� � �������

    public Dictionary<Type, AttackModifier> AttackModifaers { get; protected set; }
    #endregion

    public Dictionary<Type, Effect> ActiveEffects { get; protected set; }
    public Dictionary<Type, Ability> Abilities { get; protected set; }

    #endregion Properties

    #region Mono
    protected virtual void Awake()
    {
        InitParameters();

        InitAttackModifiers();

        InitAbilities();

        SetLevel(Level);
    }

    protected virtual void Start()
    {
        InitControllers();

        InitControllersParameters();
    }

    #endregion Mono

    #region Public methods

    public virtual void InitParameters()
    {
        if(!_unitConfig)
        {
            Debug.Log("���� ������ �� ����� � ����������!");
            return;
        }

        Level = _unitConfig.Level;

        Health = new Health(_unitConfig.DefaultHealth, _unitConfig.HealthIncreasePerLevel, _unitConfig.HealthMaxLevel);
        Armor = new Armor(_unitConfig.DefaultArmor, _unitConfig.ArmorIncreasePerLevel, _unitConfig.ArmorMaxLevel);
        Damage = new Damage(_unitConfig.DefaultDamage, _unitConfig.DamageIncreasePerLevel, maxLevel: _unitConfig.DamageMaxLevel);
        MovementSpeed = new MovementSpeed(_unitConfig.DefaultMovementSpeed, _unitConfig.MovementSpeedIncreasePerLevel, _unitConfig.MovementSpeedMaxLevel);
        AttackSpeed = new AttackSpeed(_unitConfig.DefaultAttackSpeed, _unitConfig.AttackSpeedIncreasePerLevel, _unitConfig.AttackSpeedMaxLevel);

        ActiveEffects = new Dictionary<Type, Effect>();
    }

    public virtual void InitAttackModifiers()
    {
        CriticalAttack = new CriticalAttack();
        FlameAttack = new FlameAttack();
        SlowAttack = new SlowAttack();
        PenetrationProjectile = new PenetrationProjectile();

        if (!_unitConfig)
        {
            Debug.Log("���� ������ �� ����� � ����������!");
            return;
        }

        if (_unitConfig.OnCriticalAttack)
            SetActiveAttackModifier(CriticalAttack);

        if (_unitConfig.OnFlameAttack)
            SetActiveAttackModifier(FlameAttack);

        if (_unitConfig.OnSlowAttack)
            SetActiveAttackModifier(SlowAttack);

        if (_unitConfig.OnPenetrationProjectile)
            SetActiveAttackModifier(PenetrationProjectile);
    }

    public virtual void InitAbilities()
    {
        Abilities = new Dictionary<Type, Ability>();
    }

    public virtual void LevelUp(int levelUp = 1)
    {
        Level += levelUp;

        SetLevel(Level);
    }

    public virtual void SetLevel(int newLevel)
    {
        Level = newLevel;
    }
    public void PerformAttack(Unit attackedUnit, Collider hitBox = null)
    {
        Damage damage = Damage.Copy();
        bool isCriticalHit = false;

        if (CriticalAttack.IsActive && CriticalAttack.IsProc)
        {
            isCriticalHit = true;
            damage.Actual = damage.Max * CriticalAttack.DamageMultiplier.Value;
        }

        if (FlameAttack.IsActive && FlameAttack.IsProc)
        {
            attackedUnit.SetEffect(FlameAttack.Effect);
        }

        if (SlowAttack.IsActive && SlowAttack.IsProc)
        {
            attackedUnit.SetEffect(SlowAttack.Effect);
        }

        // ������� ���� �����
        attackedUnit.TakeDamage(damage, isCriticalHit, hitBox);
    }

    public virtual void SetEffect(Effect effect)
    {
        if (ActiveEffects.ContainsKey(effect.GetType()))
            return;

        // �������� ������
        Effect newEffect = effect.DeepCopy(this); // 14

        // ��������� ������
        newEffect.Enable(); // 2

        // ���� ������ ����� ������������, �� ��������� �������� � �������� � � ������������� ������������, ���� �� ����
        if (newEffect.Duration != null)
            StartCoroutine(newEffect.CoroutineEffect); // 2700+

        // ��������� ������ � ������� ��� ��������
        ActiveEffects.Add(newEffect.GetType(), newEffect); // 9
    }

    public virtual void RemoveEffect(Effect effect)
    {
        // ���� ������ ��� ������� ���������� � �������, �� ������� ���
        if (ActiveEffects.ContainsKey(effect.GetType()))
        {
            // ������� ��������� ������� �� ��������� ������� ��� ���������, � �����������, ��� ������ ������
            effect.Disable();

            // ������� ������ �� �������
            ActiveEffects.Remove(effect.GetType());
        }
    }

    public virtual void SetActiveAttackModifier(AttackModifier attackModifier)
    {
        attackModifier.IsActive = true;
    }

    #endregion Public methods

    #region Abstract methods

    public abstract void TakeDamage(Damage damage, bool isCriticalHit = false, Collider hitBox = null);

    /// <summary>
    /// ����� ������������� ����������� �����
    /// </summary>
    protected abstract void InitControllers();

    /// <summary>
    /// ����� ������������� ���������� ����������� �����
    /// </summary>
    protected abstract void InitControllersParameters();

    #endregion Abstract methods
}
