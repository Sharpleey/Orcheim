using UnityEngine;

public abstract class PlayerUnit : Unit, IPlayerUnitParameters
{
    #region Properties

    public int Gold { get; protected set; }
    public int Experience { get; protected set; }

    #endregion Properties

    #region Mono

    protected override void Awake()
    {
        base.Awake();

        AddParametersToPoolAwards();

        AddAttackModifiersToPoolAwards();
    }

    #endregion Mono

    #region Private methods
    private void AddListeners()
    {
        GlobalGameEventManager.OnEnemyKilled.AddListener(EventHandler_EnemyKilled);
    }

    private void AddParametersToPoolAwards()
    {
        LootManager.Instance?.AddAwardPlayerStatUpgrade(Health.Name, Health);
        LootManager.Instance?.AddAwardPlayerStatUpgrade(Armor.Name, Armor);
        LootManager.Instance?.AddAwardPlayerStatUpgrade(Damage.Name, Damage);
        LootManager.Instance?.AddAwardPlayerStatUpgrade(MovementSpeed.Name, MovementSpeed);
        LootManager.Instance?.AddAwardPlayerStatUpgrade(AttackSpeed.Name, AttackSpeed);
    }

    private void AddAttackModifiersToPoolAwards()
    {
        if (!_unitConfig.OnCriticalAttack)
            LootManager.Instance?.AddAwardAttackModifier(CriticalAttack);

        if (!_unitConfig.OnFlameAttack)
            LootManager.Instance?.AddAwardAttackModifier(FlameAttack);

        if (!_unitConfig.OnSlowAttack)
            LootManager.Instance?.AddAwardAttackModifier(SlowAttack);

        if (!_unitConfig.OnPenetrationProjectile)
            LootManager.Instance?.AddAwardAttackModifier(PenetrationProjectile);
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

    public override void TakeDamage(Damage damage, Collider hitBox = null)
    {
        if (Health.Actual > 0)
        {
            int damageValue = damage.Actual;

            if (!damage.IsArmorIgnore)
            {
                // Значение уменьшения урона
                float increaseDamage = 1.0f - (Armor.Actual / (100.0f + Armor.Actual));

                // Уменьшенный урон за счет брони
                damageValue = (int)(damageValue * increaseDamage);
            }

            Health.Actual -= damageValue;

            PlayerEventManager.PlayerDamaged(damageValue);
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
            LootManager.Instance?.AddAwardAttackModifierUpgrade(criticalAttack.Name, criticalAttack.Chance);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(criticalAttack.Name, criticalAttack.DamageMultiplier);

            return;
        }

        if (attackModifier is FlameAttack flameAttack)
        {
            LootManager.Instance?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Chance);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.Damage);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.Duration);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.ArmorDecrease);

            return;
        }

        if (attackModifier is SlowAttack slowAttack)
        {
            LootManager.Instance?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Chance);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.MovementSpeedPercentageDecrease);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.AttackSpeedPercentageDecrease);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.Duration);

            return;
        }

        if (attackModifier is PenetrationProjectile penetrationProjectile)
        {
            LootManager.Instance?.AddAwardAttackModifierUpgrade(penetrationProjectile.Name, penetrationProjectile.MaxPenetrationCount);
            LootManager.Instance?.AddAwardAttackModifierUpgrade(penetrationProjectile.Name, penetrationProjectile.PenetrationDamageDecrease);

            return;
        }
    }

    #endregion Public methods

    #region Event Handlers

    private void EventHandler_EnemyKilled(EnemyUnit enemyUnit)
    {
        Experience += enemyUnit.CostInExp.Value;
        Gold += enemyUnit.CostInGold.Value;
    }

    #endregion Event Handlers
}
