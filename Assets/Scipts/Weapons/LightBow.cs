using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BowAudioController))]
public class LightBow : MonoBehaviour, IBowWeapon
{
    #region Serialize fields
    [Header("Weapon")]
    [SerializeField] private string _name = "������ ���";
    [SerializeField] private float _shotForce = 16; // ���� ��������

    [Header("Projectile")]
    //[SerializeField] private GameObject _selectedPrefabArrow; // ������ ��� ������
    [SerializeField] private GameObject _prefabArrow; // ������ �� �������� ����� ������ ��������� �����, ��������� � ���� �������
    [SerializeField] private Transform _arrowSpawn;

    [Header("AimingZoom")]
    [SerializeField] private float _zoomFOV = 30f;
    [SerializeField] private float _originalFOV = 60f;
    [SerializeField] private float _zoomStepTime = 4f;
    #endregion Serialize fields

    #region Properties
    /// <summary>
    /// �������� ����
    /// </summary>
    public string Name { get => _name; private set => _name = value; }

    /// <summary>
    /// ��������� ������������, ��������� ������
    /// </summary>
    public bool IsAiming { get; private set; }

    /// <summary>
    /// C��������, ����� ������ ��������
    /// </summary>
    public bool IsAimingLoaded { get; private set; }

    /// <summary>
    /// ��������� �� ������ �� ����� �������� �����������
    /// </summary>
    public bool IsReloading { get; private set; }
    
    /// <summary>
    /// ��������� �� ������ �������� �����������, �� ����� �������� ����� ������
    /// </summary>
    public bool IsFastShooting{ get; private set; }

    /// <summary>
    /// ��������������� ��� ��������������� ���� ����
    /// </summary>
    public BowAudioController AudioController { get; private set; }

    /// <summary>
    /// �������� ����
    /// </summary>
    public Animator Animator { get; private set; }

    /// <summary>
    /// ������ �� ��������� PlayerUnit
    /// </summary>
    public PlayerUnit PlayerUnit { get; private set; }

    #endregion Properties

    #region Private fields
    private bool _isZoomed;

    private bool _isLockControl;

    // ������ � ������� ����� ������� ���� ������, ��� �� ����� ������������
    private GameObject _cloneArrow;
    private ProjectileArrow _cloneProjectileArrow;

    #endregion Private fields

    #region Mono
    private void Awake()
    {
        Name = _name;

        GlobalGameEventManager.OnPauseGame.AddListener(SetLockControl);
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        AudioController = GetComponent<BowAudioController>();

        PlayerUnit = GetComponentInParent<PlayerUnit>();

        RespawnProjectile();
    }

    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (!_isLockControl)
        {
            // ������� ������� (������ ���)
            if (!IsReloading && !IsFastShooting && !PlayerUnit.FirstPersonController.IsSprinting && !IsAiming && Input.GetMouseButtonDown(0))
            {
                Animator.SetTrigger(HashAnimStringWeapon.IsFastShot);
            }

            // ������������� (������ ���)
            if (!IsReloading && !IsFastShooting && !PlayerUnit.FirstPersonController.IsSprinting && Input.GetMouseButtonDown(1))
            {
                Animator.SetBool(HashAnimStringWeapon.IsAimingLoad, true);

                _isZoomed = true;
            }

            // ������� �� ������������ (��������� ���)
            if ((IsAiming || IsAimingLoaded) &&  Input.GetMouseButtonUp(1))
            {
                Animator.SetBool(HashAnimStringWeapon.IsAimingLoad, false);

                _isZoomed = false;

                SetAiming(0);
                SetAimingLoaded(0);
            }

            // ���������� ������� (��������� ��� ������ ���)
            if (IsAiming && IsAimingLoaded && Input.GetMouseButtonDown(0))
            {
                Animator.SetTrigger(HashAnimStringWeapon.IsAimingShot);
            }

            Animator.SetFloat(HashAnimStringWeapon.PlayerSpeed, PlayerUnit.Rigidbody.velocity.magnitude / (PlayerUnit.MovementSpeed.Max/100f));
            Animator.SetBool(HashAnimStringWeapon.IsSprint, PlayerUnit.FirstPersonController.IsSprinting);

            AimingZoom();
        }
    }

    /// <summary>
    /// ����������� ������ ��� ������������
    /// </summary>
    private void AimingZoom()
    {
        // Lerps camera.fieldOfView to allow for a smooth transistion
        if (_isZoomed)
        {
            PlayerUnit.Camera.fieldOfView = Mathf.Lerp(PlayerUnit.Camera.fieldOfView, _zoomFOV, _zoomStepTime * Time.deltaTime);
        }
        else if (!_isZoomed && !PlayerUnit.FirstPersonController.IsSprinting)
        {
            PlayerUnit.Camera.fieldOfView = Mathf.Lerp(PlayerUnit.Camera.fieldOfView, _originalFOV, _zoomStepTime * Time.deltaTime);
        }
    }

    /// <summary>
    /// ����� ������������ ����������
    /// </summary>
    /// <param name="isLock">����������� ��� �� �����������</param>
    private void SetLockControl(bool isLock)
    {
        _isLockControl = isLock;
    }

    /// <summary>
    /// ����� ������������� ��������� IsAiming c ������� ������� � ��������
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetAiming(int animEventParam)
    {
        IsAiming = animEventParam == 1;

        if (IsAiming)
        {
            if (PlayerUnit)
            {
                // ��������� �������� ���� ������ ��� ������������
                PlayerUnit.MovementSpeed.Actual = (int)(PlayerUnit.MovementSpeed.Max * 0.5f);
                PlayerUnit.FirstPersonController.walkSpeed = PlayerUnit.MovementSpeed.Actual/100f;

                // ��������� ������ ����������� �����������
                PlayerUnit.FirstPersonController.IsBlockSprint = true;

                // ��������� ����������� ������� ������
                PlayerUnit.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // ������������� �������� ��������
                Animator.speed = PlayerUnit.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (PlayerUnit)
            {
                // ��������� �������� ���� ������ � �������� ���������
                PlayerUnit.MovementSpeed.Actual = PlayerUnit.MovementSpeed.Max;
                PlayerUnit.FirstPersonController.walkSpeed = PlayerUnit.MovementSpeed.Actual/100f;


                // ����������� ������ ����������� �����������
                PlayerUnit.FirstPersonController.IsBlockSprint = false;

                // ����������� ����������� ������� ������
                PlayerUnit.WeaponController.IsBlockChangeWeapon = false;
            }

            if (Animator)
            {
                // ��������� �������� ��������� � �������� ���������
                Animator.speed = 1;
            }
        }
    }

    /// <summary>
    /// ����� ������������� ��������� IsAimingLoaded c ������� ������� � ��������
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetAimingLoaded(int animEventParam)
    {
        IsAimingLoaded = animEventParam == 1;
    }

    /// <summary>
    /// ����� ������������� ��������� IsReloading c ������� ������� � ��������
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetReloading(int animEventParam)
    {
        IsReloading = animEventParam == 1;

        if (IsReloading)
        {
            if (PlayerUnit)
            {
                // ��������� ������ ����������� �����������
                PlayerUnit.FirstPersonController.IsBlockSprint = true;

                // ��������� ����������� ������� ������
                PlayerUnit.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // ������������� �������� ��������
                Animator.speed = PlayerUnit.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (PlayerUnit)
            {
                // ������������ ������ ����������� �����������
                PlayerUnit.FirstPersonController.IsBlockSprint = false;

                // ������������ ������ ����������� ������� ������
                PlayerUnit.WeaponController.IsBlockChangeWeapon = false;
            }

            if (Animator)
            {
                // ��������� �������� ��������� � �������� ���������
                Animator.speed = 1;
            }
        }
    }

    /// <summary>
    /// ����� ������������� ��������� IsFastShooting c ������� ������� � ��������
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetFastShooting(int animEventParam)
    {
        IsFastShooting = animEventParam == 1;

        if (IsFastShooting)
        {
            if (PlayerUnit)
            {
                // ��������� �������� ���� ������ ��� ��������
                PlayerUnit.MovementSpeed.Actual = (int)(PlayerUnit.MovementSpeed.Max * 0.5f);
                PlayerUnit.FirstPersonController.walkSpeed = PlayerUnit.MovementSpeed.Actual / 100f;

                // ��������� ������ ����������� �����������
                PlayerUnit.FirstPersonController.IsBlockSprint = true;

                // ��������� ����������� ������� ������
                PlayerUnit.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // ������������� �������� ��������
                Animator.speed = PlayerUnit.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (PlayerUnit)
            {
                // ��������� �������� ���� ������ � �������� ���������
                PlayerUnit.MovementSpeed.Actual = PlayerUnit.MovementSpeed.Max;
                PlayerUnit.FirstPersonController.walkSpeed = PlayerUnit.MovementSpeed.Actual / 100f;

                // ������������ ������ ����������� �����������
                PlayerUnit.FirstPersonController.IsBlockSprint = false;

                // ������������ ������ ����������� ������� ������
                PlayerUnit.WeaponController.IsBlockChangeWeapon = false;
            }

            if (Animator)
            {
                // ��������� �������� ��������� � �������� ���������
                Animator.speed = 1;
            }
        }
    }

    /// <summary>
    /// ����� ��������
    /// </summary>
    private void Shot()
    {
        // ��������� ������
        if(_cloneProjectileArrow)
        {
            _cloneProjectileArrow?.Launch(_shotForce);
        }
    }

    /// <summary>
    /// ����� ������� ������ ������ ��� �����������
    /// </summary>
    private void RespawnProjectile()
    {
        _cloneArrow = Instantiate(_prefabArrow);

        _cloneArrow.transform.parent = _arrowSpawn.transform;

        _cloneArrow.transform.position = _arrowSpawn.position;
        _cloneArrow.transform.rotation = _arrowSpawn.rotation;

        _cloneProjectileArrow = _cloneArrow.GetComponent<ProjectileArrow>();

        _cloneArrow.GetComponent<Rigidbody>().isKinematic = true;
    }
    #endregion Private methods
}
