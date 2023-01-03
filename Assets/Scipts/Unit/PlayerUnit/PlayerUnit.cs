using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(PlayerWeaponController))]
public class PlayerUnit : Unit, IPlayerUnitParameters
{
    #region Properties

    public int Gold { get; private set; }
    public int Experience { get; private set; }

    public Camera Camera { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public FirstPersonController FirstPersonController { get; private set; }
    public PlayerWeaponController WeaponController  { get; private set; }

    #endregion Properties

    #region Private methods

    protected override void InitControllers()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Camera = GetComponentInChildren<Camera>();

        WeaponController = GetComponent<PlayerWeaponController>();
        FirstPersonController = GetComponent<FirstPersonController>();
    }

    protected override void InitControllersParameters()
    {
        FirstPersonController.walkSpeed = MovementSpeed.Max / 100f;
        FirstPersonController.sprintSpeed = MovementSpeed.Max * (1f + 0.18f) / 100f; //TODO спринт сделать как способность
    }

    #endregion Private methods

    #region Public methods

    public override void InitParameters()
    {
        if (PlayerManager.Instance)
        {
            Level = PlayerManager.Instance.Level;

            Health = PlayerManager.Instance.Health;
            Armor = PlayerManager.Instance.Armor;
            Damage = PlayerManager.Instance.Damage;
            MovementSpeed = PlayerManager.Instance.MovementSpeed;
            AttackSpeed = PlayerManager.Instance.AttackSpeed;
            Gold = PlayerManager.Instance.Gold; //TODO Это не ссылочный тип, поэтому над что-то придумать

            ActiveEffects = new Dictionary<Type, Effect>();

            return;
        }

        base.InitParameters();

        PlayerUnitConfig? playerUnitConfig = _unitConfig as PlayerUnitConfig;

        if (playerUnitConfig)
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

    #endregion Public methods
}
