using KinematicCharacterController;
using UnityEngine;

public class Player : PlayerUnit
{
    #region Properties

    public Camera Camera { get; private set; }
    public PlayerWeaponController WeaponController { get; private set; }
    public PlayerCharacterController PlayerCharacterController { get; private set; }
    public KinematicCharacterMotor  KinematicCharacterMotor { get; private set; }


    #endregion Properties

    #region Private methods

    protected override void AddListeners()
    {
        base.AddListeners();

        PlayerEventManager.OnPlayerMovementSpeedChanged.AddListener(EventHandler_PlayerMovementSpeedChanged);
    }

    protected override void InitControllers()
    {
        Camera = GetComponentInChildren<Camera>();
        WeaponController = GetComponent<PlayerWeaponController>();
        PlayerCharacterController = GetComponent<PlayerCharacterController>();
        KinematicCharacterMotor = GetComponent<KinematicCharacterMotor>();
    }

    protected override void InitControllersParameters()
    {
        PlayerCharacterController.maxStableMoveSpeed = MovementSpeed.Max / 100f;
        PlayerCharacterController.maxAirMoveSpeed = MovementSpeed.Max / 100f;
        PlayerCharacterController.jumpSpeed = 2 * (MovementSpeed.Max / 100f);
    }

    #endregion Private methods

    #region Event Handlers

    private void EventHandler_PlayerMovementSpeedChanged()
    {
        PlayerCharacterController.maxStableMoveSpeed = MovementSpeed.Max / 100f;
    }

    #endregion
}
