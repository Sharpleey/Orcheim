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
        FirstPersonController.sprintSpeed = MovementSpeed.Max * (1f + 0.25f) / 100f; //TODO спринт сделать как способность
    }

    #endregion Private methods
}
