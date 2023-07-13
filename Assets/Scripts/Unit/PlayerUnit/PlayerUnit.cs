using UnityEngine;
using Zenject;

public abstract class PlayerUnit : Unit, IPlayerUnitParameters
{
    #region Properties

    public int Gold { get; protected set; }
    
    private int _experience;
    public int Experience 
    { 
        get => _experience; 
        protected set => _experience = Mathf.Clamp(value, 0, int.MaxValue);
    }
    public int ExperienceForNextLevel => ((Level + 1) - 1) * (((Level + 1) - 2) * GeneralParameter.EXP_COEFF + 200);

    #endregion Properties

    #region Private fields
    private LootManager _lootManager;
    #endregion

    #region Mono

    protected override void Awake()
    {
        base.Awake();

        AddParametersToPoolAwards();

        AddAttackModifiersToPoolAwards();

        AddListeners();
    }

    [Inject]
    private void Construct(LootManager lootManager)
    {
        _lootManager = lootManager;
    }

    #endregion Mono

    #region Private methods
    protected virtual void AddListeners()
    {
        GlobalGameEventManager.OnEnemyKilled.AddListener(EventHandler_EnemyKilled);
        WaveEventManager.OnWaveIsOver.AddListener(EventHandler_WaveIsOver);
    }

    private void AddParametersToPoolAwards()
    {
        _lootManager?.AddAwardPlayerStatUpgrade(Health.Name, Health);
        _lootManager?.AddAwardPlayerStatUpgrade(Armor.Name, Armor);
        _lootManager?.AddAwardPlayerStatUpgrade(Damage.Name, Damage);
        _lootManager?.AddAwardPlayerStatUpgrade(MovementSpeed.Name, MovementSpeed);
        _lootManager?.AddAwardPlayerStatUpgrade(AttackSpeed.Name, AttackSpeed);
    }

    private void AddAttackModifiersToPoolAwards()
    {
        if (!_unitConfig.OnCriticalAttack)
            _lootManager?.AddAwardAttackModifier(CriticalAttack);

        if (!_unitConfig.OnFlameAttack)
            _lootManager?.AddAwardAttackModifier(FlameAttack);

        if (!_unitConfig.OnSlowAttack)
            _lootManager?.AddAwardAttackModifier(SlowAttack);

        if (!_unitConfig.OnPenetrationProjectile)
            _lootManager?.AddAwardAttackModifier(PenetrationProjectile);
    }

    #endregion Private methods

    #region Public methods

    public override void InitParameters()
    {
        base.InitParameters();

        PlayerUnitConfig? playerUnitConfig = _unitConfig as PlayerUnitConfig;

        if (playerUnitConfig != null)
        {
            Gold = playerUnitConfig.Gold;
        }
    }

    public override void TakeDamage(float damage, DamageType damageType, bool isArmorIgnore = false, bool isCriticalHit = false, Collider hitBox = null)
    {
        if (Health.Actual > 0)
        {
            if (!isArmorIgnore)
            {
                // Значение уменьшения урона
                float increaseDamage = 1.0f - (Armor.Actual / (100.0f + Armor.Actual));

                // Уменьшенный урон за счет брони
                damage *= increaseDamage;
            }

            Health.Actual -= damage;

            PlayerEventManager.PlayerDamaged(damage);
            PlayerEventManager.PlayerHealthChanged();
        }

        if (Health.Actual <= 0)
        {
            PlayerEventManager.PlayerDead();
        }
    }

    public override void SetActiveAttackModifier(AttackModifier attackModifier)
    {
        base.SetActiveAttackModifier(attackModifier);

        // Добавление параметров модификатора в пул наград

        // TODO Переделать или перенести куда-то
        if (attackModifier is CriticalAttack criticalAttack)
        {
            _lootManager?.AddAwardAttackModifierUpgrade(criticalAttack.Name, criticalAttack.Chance);
            _lootManager?.AddAwardAttackModifierUpgrade(criticalAttack.Name, criticalAttack.DamageMultiplier);

            return;
        }

        if (attackModifier is FlameAttack flameAttack)
        {
            _lootManager?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Chance);
            _lootManager?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.DamagePerSecond);
            _lootManager?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.Duration);
            _lootManager?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.ArmorDecrease);

            return;
        }

        if (attackModifier is SlowAttack slowAttack)
        {
            _lootManager?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Chance);
            _lootManager?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.MovementSpeedPercentageDecrease);
            _lootManager?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.AttackSpeedPercentageDecrease);
            _lootManager?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.Duration);

            return;
        }

        if (attackModifier is PenetrationProjectile penetrationProjectile)
        {
            _lootManager?.AddAwardAttackModifierUpgrade(penetrationProjectile.Name, penetrationProjectile.MaxPenetrationCount);
            _lootManager?.AddAwardAttackModifierUpgrade(penetrationProjectile.Name, penetrationProjectile.PenetrationDamageDecrease);

            return;
        }
    }

    public override void LevelUp(int levelUp = 1)
    {
        base.LevelUp(levelUp);

        PlayerEventManager.PlayerLevelUp();
    }

    public void AddExperience(int newExp)
    {
        int delta = newExp - (ExperienceForNextLevel - Experience);

        if(delta >= 0)
        {
            Experience = ExperienceForNextLevel;

            LevelUp();

            AddExperience(delta);

        }

        Experience += newExp;

        PlayerEventManager.PlayerExperienceChanged();
    }

    public void AddGold(int gold)
    {
        Gold += gold;

        PlayerEventManager.PlayerGoldChanged();
    }

    #endregion Public methods

    #region Event Handlers

    private void EventHandler_EnemyKilled(EnemyUnit enemyUnit)
    {
        AddExperience((int)enemyUnit.CostInExp.Value);
        AddGold((int)enemyUnit.CostInGold.Value);
    }

    private void EventHandler_WaveIsOver(int wave)
    {
        Health.Actual = Health.Max;

        PlayerEventManager.PlayerHealthChanged();
    }

    #endregion Event Handlers
}
