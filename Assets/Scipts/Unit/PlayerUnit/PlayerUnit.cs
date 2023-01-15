using System;
using System.Collections.Generic;
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
    }

    public override void TakeDamage(Damage damage, Collider hitBox = null)
    {
        if (Health.Actual > 0)
        {
            int damageValue = damage.Actual;

            if (!damage.IsArmorIgnore)
            {
                // �������� ���������� �����
                float increaseDamage = 1.0f - (Armor.Actual / (100.0f + Armor.Actual));

                // ����������� ���� �� ���� �����
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

    public override void SetAttackModifier(AttackModifier attackModifier)
    {
        CriticalAttack? criticalAttack = attackModifier as CriticalAttack;
        if (criticalAttack != null)
        {
            CriticalAttack = criticalAttack;

            //
            // ���������� ���������� ������������ � ��� ������
            //

            return;
        }

        FlameAttack? flameAttack = attackModifier as FlameAttack;
        if (flameAttack != null)
        {
            FlameAttack = flameAttack;

            if (LootManager.Instance)
            {
                // ���������� ���������� ������������ � ��� ������
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.�hance);
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.Effect.Damage);
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.Effect.Duration);
                LootManager.Instance.AddAwardAttackModifierUpgrade(FlameAttack.Name, FlameAttack.Effect.ArmorDecrease);
            }

            return;
        }

        SlowAttack? slowAttack = attackModifier as SlowAttack;
        if (slowAttack != null)
        {
            SlowAttack = slowAttack;

            if (LootManager.Instance)
            {
                // ���������� ���������� ������������ � ��� ������
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.�hance);
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.Effect.MovementSpeedPercentageDecrease);
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.Effect.AttackSpeedPercentageDecrease);
                LootManager.Instance.AddAwardAttackModifierUpgrade(SlowAttack.Name, SlowAttack.Effect.Duration);
            }

            return;
        }

        PenetrationProjectile? penetrationProjectile = attackModifier as PenetrationProjectile;
        if (penetrationProjectile != null)
        {
            PenetrationProjectile = penetrationProjectile;

            //
            // ���������� ���������� ������������ � ��� ������
            //

            return;
        }
    }

    #endregion Public methods
}
