using UnityEngine;

/// <summary>
/// In Progress
/// </summary>
[RequireComponent(typeof(BowAudioController))]
public class LightBow : MonoBehaviour
{
    #region Serialize fields
    [Header("Weapon")]
    [SerializeField] private float _fastShotForce = 16;
    [SerializeField] private Transform _arrowSpawn;

    [Header("AimingZoom")]
    [SerializeField] private float _zoomFOV = 30f;
    [SerializeField] private float _originalFOV = 60f;
    [SerializeField] private float _zoomStepTime = 4f;

    [Header("��������� �������� ������������ ������ ��� ������������")]
    [SerializeField, Range(0, 1f)] private float _multiplyAimingMovementSpeed = 0.5f;

    #endregion Serialize fields

    #region Properties

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
    public Player Player { get; private set; }

    /// <summary>
    /// ���� ��������
    /// </summary>
    public float ShotForce => _fastShotForce;


    #endregion Properties

    #region Private fields

    private bool _isZoomed;
    private bool _isLockControl;

    private ProjectileArrow _projectile;

    #endregion Private fields

    #region Mono
    private void Awake()
    {
        GameSceneEventManager.OnGamePause.AddListener(SetLockControl);
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        AudioController = GetComponent<BowAudioController>();

        Player = GetComponentInParent<Player>();

        RespawnProjectile();
    }

    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (!_isLockControl)
        {
            // ������� ������� (������ ���)
            if (!IsReloading && !IsFastShooting && !IsAiming && Input.GetMouseButtonDown(0))
            {
                Animator.SetTrigger(HashAnimStringWeapon.IsFastShot);
            }

            // ������������� (������ ���)
            if (!IsReloading && !IsFastShooting && Input.GetMouseButtonDown(1))
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

            Animator.SetFloat(HashAnimStringWeapon.PlayerSpeed, Player.KinematicCharacterMotor.Velocity.magnitude / (Player.MovementSpeed.Max/100f));

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
            Player.Camera.fieldOfView = Mathf.Lerp(Player.Camera.fieldOfView, _zoomFOV, _zoomStepTime * Time.deltaTime);
        }
        else if (!_isZoomed)
        {
            Player.Camera.fieldOfView = Mathf.Lerp(Player.Camera.fieldOfView, _originalFOV, _zoomStepTime * Time.deltaTime);
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
            if (Player)
            {
                // ��������� �������� ���� ������ ��� ������������
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max * _multiplyAimingMovementSpeed;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual/100f;

                // ��������� ����������� ������� ������
                Player.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // ������������� �������� ��������
                Animator.speed = Player.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (Player)
            {
                // ��������� �������� ���� ������ � �������� ���������
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual/100f;

                // ����������� ����������� ������� ������
                Player.WeaponController.IsBlockChangeWeapon = false;
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
            if (Player)
            {
                // ��������� ����������� ������� ������
                Player.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // ������������� �������� ��������
                Animator.speed = Player.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (Player)
            {
                // ������������ ������ ����������� ������� ������
                Player.WeaponController.IsBlockChangeWeapon = false;
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
            if (Player)
            {
                // ��������� �������� ���� ������ ��� ��������
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max * _multiplyAimingMovementSpeed;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual / 100f;

                // ��������� ����������� ������� ������
                Player.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // ������������� �������� ��������
                Animator.speed = Player.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (Player)
            {
                // ��������� �������� ���� ������ � �������� ���������
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual / 100f;

                // ������������ ������ ����������� ������� ������
                Player.WeaponController.IsBlockChangeWeapon = false;
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
        _projectile?.Launch(this, Player);
    }

    /// <summary>
    /// ����� ������� ������ ������ ��� �����������
    /// </summary>
    private void RespawnProjectile()
    {
        _projectile = PoolManager.Instance?.ProjectileArrowPool.GetFreeElement();

        _projectile.transform.SetParent(_arrowSpawn.transform);
        _projectile.transform.SetPositionAndRotation(_arrowSpawn.position, _arrowSpawn.rotation);
    }
    #endregion Private methods
}
