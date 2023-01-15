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
        // ���� ���� ����������� � ������� ������ �� ����� �����������, �� ��� ������ ������� �� ��������� ������, �.�. �� ����� �������� � ������� ������, ������� �������� � ���������
        if(PlayerManager.Instance)
        {
            Level = PlayerManager.Instance.Level; //TODO ��������� ���???

            Health = PlayerManager.Instance.Health;
            Armor = PlayerManager.Instance.Armor;
            Damage = PlayerManager.Instance.Damage;
            MovementSpeed = PlayerManager.Instance.MovementSpeed;
            AttackSpeed = PlayerManager.Instance.AttackSpeed;

            Gold = PlayerManager.Instance.Gold; //TODO ��������� ���???
            Experience = PlayerManager.Instance.Experience; //TODO ��������� ���???

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
        FirstPersonController.sprintSpeed = MovementSpeed.Max * (1f + 0.18f) / 100f; //TODO ������ ������� ��� �����������
    }

    #endregion Private methods
}
