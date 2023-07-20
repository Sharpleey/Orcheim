using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnitLevel, IAttacking, IDamageable, IUnitParameters, IInfluenceOfEffects, IUsesAbilities, IUsesAttackModifiers
{
    #region Serialize fields

    [Header("Конфиг с параметрами юнита")]
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

    public Dictionary<Type, Effect> ActiveEffects { get; protected set; }
    public Dictionary<Type, Ability> Abilities { get; protected set; }
    public Dictionary<Type, AttackModifier> AttackModifiers { get; protected set; }

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
            Debug.Log("Юнит конфиг не задан в инспекторе!");
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
        AttackModifiers = new Dictionary<Type, AttackModifier>();

        if (!_unitConfig)
        {
            Debug.Log("Юнит конфиг не задан в инспекторе!");
            return;
        }

        if (_unitConfig.OnCriticalAttack)
            SetAttackModifier(new CriticalAttack(increaseProcChancePerLevel: 10));

        if (_unitConfig.OnFlameAttack)
            SetAttackModifier(new FlameAttack(increaseProcChancePerLevel: 10, increaseDamageFlamePerLevel: 4, increaseDurationEffectPerLevel: 2));

        if (_unitConfig.OnSlowAttack)
            SetAttackModifier(new SlowAttack(increaseProcChancePerLevel: 10, increaseAttackSpeedPercentageDecrease: 10, increaseMovementSpeedPercentageDecrease: 10, increaseDurationEffectPerLevel: 3, durationEffect: 5));

        if (_unitConfig.OnPenetrationProjectile)
            SetAttackModifier(new PenetrationProjectile(decreasePenetrationDamageDecreasePerLevel: -10, maxLevelPenetrationDamageDecrease: 5));
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
    public void PerformAttack(Unit attackedUnit, int currentPenetration = 0, Collider hitBox = null)
    {
        float damage = Damage.Actual;
        bool isCriticalHit = false;

        if(AttackModifiers.TryGetValue(typeof(PenetrationProjectile), out AttackModifier modifierPenetrationProjectile))
        {
            PenetrationProjectile penetrationProjectile = (PenetrationProjectile)modifierPenetrationProjectile;
            if (penetrationProjectile.IsActive && currentPenetration != 0)
            {
                damage = penetrationProjectile.GetValueDamage(damage, currentPenetration);
            }
        }

        if (AttackModifiers.TryGetValue(typeof(CriticalAttack), out AttackModifier modifierAttackModifier))
        {
            CriticalAttack criticalAttack = (CriticalAttack)modifierAttackModifier;
            if (criticalAttack.IsActive && criticalAttack.IsProc)
            {
                isCriticalHit = true;
                damage *= criticalAttack.DamageMultiplier.Value / 100f;
            }
        }

        if (AttackModifiers.TryGetValue(typeof(FlameAttack), out AttackModifier modifierFlameAttack))
        {
            FlameAttack flameAttack = (FlameAttack)modifierFlameAttack;
            if (flameAttack.IsActive && flameAttack.IsProc)
            {
                attackedUnit.SetEffect(flameAttack.Effect);
            }
        }

        if (AttackModifiers.TryGetValue(typeof(SlowAttack), out AttackModifier modifierSlowAttack))
        {
            SlowAttack slowAttack = (SlowAttack)modifierSlowAttack;
            if (slowAttack.IsActive && slowAttack.IsProc)
            {
                attackedUnit.SetEffect(slowAttack.Effect);
            }
        }

        // Наносим урон юниту
        attackedUnit.TakeDamage(Mathf.Clamp(damage, 0, int.MaxValue), Damage.Type, Damage.IsArmorIgnore, isCriticalHit, hitBox);
    }

    public virtual void SetAttackModifier(AttackModifier newAttackModifier)
    {
        if (AttackModifiers.ContainsKey(newAttackModifier.GetType()))
            return;

        AttackModifiers.Add(newAttackModifier.GetType(), newAttackModifier);
    }

    public void RemoveAttackModifier<T>() where T : AttackModifier
    {
        if (AttackModifiers.ContainsKey(typeof(T)))
            AttackModifiers.Remove(typeof(T));
    }

    public virtual void SetEffect(Effect effect)
    {
        if (ActiveEffects.ContainsKey(effect.GetType()))
            return;

        // Копируем эффект
        Effect newEffect = effect.DeepCopy(this); // 14

        // Применяем эффект
        newEffect.Enable(); // 2

        // Если эффект иммет длительность, то запускаем корутину с таймером и с переодическим воздействием, если он есть
        if (newEffect.Duration != null)
            StartCoroutine(newEffect.CoroutineEffect); // 2700+

        // Добавляем эффект в словарь для хранения
        ActiveEffects.Add(newEffect.GetType(), newEffect); // 9
    }

    public virtual void RemoveEffect(Effect effect)
    {
        // Если данный тип эффекта содержится в словаре, то удаляем его
        if (ActiveEffects.ContainsKey(effect.GetType()))
        {
            // Удаляем изменения эффекта по окончанию эффекта или применяем, в зависимости, что эффект делает
            effect.Disable();

            // Удаляем эффект из словаря
            ActiveEffects.Remove(effect.GetType());
        }
    }

    #endregion Public methods

    #region Abstract methods

    public abstract void TakeDamage(float damage, DamageType damageType, bool isArmorIgnore = false, bool isCriticalHit = false, Collider hitBox = null);

    /// <summary>
    /// Метод инициализации конроллеров юнита
    /// </summary>
    protected abstract void InitControllers();

    /// <summary>
    /// Метод инициализации параметров конроллеров юнита
    /// </summary>
    protected abstract void InitControllersParameters();

    #endregion Abstract methods
}
