using UnityEngine;

public abstract class PlayerUnit : Unit, IPlayerUnitParameters
{
    #region Properties

    public int Gold { get; protected set; }
    public int Experience { get; protected set; }

    #endregion Properties


    #region Public methods

    public override void InitParameters()
    {
        base.InitParameters();

        PlayerUnitConfig? playerUnitConfig = _unitConfig as PlayerUnitConfig;

        if (playerUnitConfig != null)
        {
            Gold = playerUnitConfig.Gold;
        }

        // Добавляем параметры в пул наград
        if (LootManager.Instance)
        {
            LootManager.Instance.AddAwardPlayerStatUpgrade(Health.Name, Health);
            LootManager.Instance.AddAwardPlayerStatUpgrade(Armor.Name, Armor);
            LootManager.Instance.AddAwardPlayerStatUpgrade(Damage.Name, Damage);
            LootManager.Instance.AddAwardPlayerStatUpgrade(MovementSpeed.Name, MovementSpeed);
            LootManager.Instance.AddAwardPlayerStatUpgrade(AttackSpeed.Name, AttackSpeed);
        }
    }

    public override void InitAttackModifiers()
    {
        base.InitAttackModifiers();

        if(LootManager.Instance)
        {
            if (!_unitConfig.OnCriticalAttack)
                LootManager.Instance.AddAwardAttackModifier(CriticalAttack);

            if (!_unitConfig.OnFlameAttack)
                LootManager.Instance.AddAwardAttackModifier(FlameAttack);

            if (!_unitConfig.OnSlowAttack)
                LootManager.Instance.AddAwardAttackModifier(SlowAttack);

            if (!_unitConfig.OnPenetrationProjectile)
                LootManager.Instance.AddAwardAttackModifier(PenetrationProjectile);
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

        CriticalAttack? criticalAttack = attackModifier as CriticalAttack;

        if (criticalAttack != null)
        {
            if (LootManager.Instance)
            {
                // Добавление параметров модификатора в пул наград
                LootManager.Instance.AddAwardAttackModifierUpgrade(CriticalAttack.Name, CriticalAttack.Сhance);
                LootManager.Instance.AddAwardAttackModifierUpgrade(CriticalAttack.Name, CriticalAttack.DamageMultiplier);
            }

            return;
        }

        FlameAttack? flameAttack = attackModifier as FlameAttack;
        if (flameAttack != null)
        {

            if (LootManager.Instance)
            {
                // Добавление параметров модификатора в пул наград
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.Сhance);
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.Effect.Damage);
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.Effect.Duration);
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.Effect.ArmorDecrease);
            }

            return;
        }

        SlowAttack? slowAttack = attackModifier as SlowAttack;
        if (slowAttack != null)
        {
            if (LootManager.Instance)
            {
                // Добавление параметров модификатора в пул наград
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.Сhance);
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.Effect.MovementSpeedPercentageDecrease);
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.Effect.AttackSpeedPercentageDecrease);
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.Effect.Duration);
            }

            return;
        }

        PenetrationProjectile? penetrationProjectile = attackModifier as PenetrationProjectile;
        if (penetrationProjectile != null)
        {

            if (LootManager.Instance)
            {
                // Добавление параметров модификатора в пул наград
                LootManager.Instance.AddAwardAttackModifierUpgrade(PenetrationProjectile.Name, PenetrationProjectile.MaxPenetrationCount);
                LootManager.Instance.AddAwardAttackModifierUpgrade(PenetrationProjectile.Name, PenetrationProjectile.PenetrationDamageDecrease);
            }

            return;
        }
    }

    #endregion Public methods
}
