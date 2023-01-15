using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(PlayerWeaponController))]
public class Player : PlayerUnit
{
    #region Properties

    public Camera Camera { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public FirstPersonController FirstPersonController { get; private set; }
    public PlayerWeaponController WeaponController { get; private set; }

    #endregion Properties

    #region Private methods

    public override void InitParameters()
    {
        // Если игра запускается в обычном режиме со всеми менеджарами, то все данные берутся из менеджера игрока, т.е. мы будем работать с данными игрока, которые хранятся в менеджере
        if(PlayerManager.Instance)
        {
            Level = PlayerManager.Instance.Level; //TODO ссылочный тип???

            Health = PlayerManager.Instance.Health;
            Armor = PlayerManager.Instance.Armor;
            Damage = PlayerManager.Instance.Damage;
            MovementSpeed = PlayerManager.Instance.MovementSpeed;
            AttackSpeed = PlayerManager.Instance.AttackSpeed;

            Gold = PlayerManager.Instance.Gold; //TODO ссылочный тип???
            Experience = PlayerManager.Instance.Experience; //TODO ссылочный тип???

            ActiveEffects = PlayerManager.Instance.ActiveEffects;

            return;
        }

        base.InitParameters();
    }

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
}
