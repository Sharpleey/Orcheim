using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnitLevel, IAttacking, IDamageable, IUsesAttackModifiers, IUnitParameters,IInfluenceOfEffects
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
    public CriticalAttack CriticalAttack { get; protected set; }
    public FlameAttack FlameAttack { get; protected set; }
    public SlowAttack SlowAttack { get; protected set; }
    public PenetrationProjectile PenetrationProjectile { get; protected set; }
    #endregion

    public Dictionary<Type, Effect> ActiveEffects { get; protected set; }

    #endregion Properties

    #region Mono
    protected virtual void Awake()
    {
        InitParameters();

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
        Level = _unitConfig.Level;

        Health = new Health(_unitConfig.DefaultHealth, _unitConfig.HealthIncreasePerLevel, _unitConfig.HealthMaxLevel);
        Armor = new Armor(_unitConfig.DefaultArmor, _unitConfig.ArmorIncreasePerLevel, _unitConfig.ArmorMaxLevel);
        Damage = new Damage(_unitConfig.DefaultDamage, _unitConfig.DamageIncreasePerLevel, maxLevel: _unitConfig.DamageMaxLevel);
        MovementSpeed = new MovementSpeed(_unitConfig.DefaultMovementSpeed, _unitConfig.MovementSpeedIncreasePerLevel, _unitConfig.MovementSpeedMaxLevel);
        AttackSpeed = new AttackSpeed(_unitConfig.DefaultAttackSpeed, _unitConfig.AttackSpeedIncreasePerLevel, _unitConfig.AttackSpeedMaxLevel);

        if (_unitConfig.OnCriticalAttack)
            CriticalAttack = new CriticalAttack();

        if (_unitConfig.OnFlameAttack)
            FlameAttack = new FlameAttack();

        if (_unitConfig.OnSlowAttack)
            SlowAttack = new SlowAttack();

        if (_unitConfig.OnPenetrationProjectile)
            PenetrationProjectile = new PenetrationProjectile();

        ActiveEffects = new Dictionary<Type, Effect>();
    }

    public void LevelUp(int levelUp = 1)
    {
        Level += levelUp;
    }

    public void SetLevel(int newLevel)
    {
        Level = newLevel;
    }
    public void PerformAttack(Unit attackedUnit, Collider hitBox = null)
    {
        Damage damage = Damage.Copy();

        if (CriticalAttack != null && CriticalAttack.IsProc())
        {
            damage.Actual = (int)(damage.Max * CriticalAttack.DamageMultiplier);
        }

        if (FlameAttack != null && FlameAttack.IsProc())
        {
            attackedUnit.SetEffect(FlameAttack.Effect);
        }

        if (SlowAttack != null && SlowAttack.IsProc())
        {
            attackedUnit.SetEffect(SlowAttack.Effect);
        }

        // ������� ���� �����
        attackedUnit.TakeDamage(damage, hitBox);
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
        if (newEffect.Duration > 0)
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

    #endregion Public methods

    #region Abstract methods

    public abstract void TakeDamage(Damage damage, Collider hitBox = null);

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
