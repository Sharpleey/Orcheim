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

    [Header("Множитель скорости передвижения игрока при прицеливании")]
    [SerializeField, Range(0, 1f)] private float _multiplyAimingMovementSpeed = 0.5f;

    #endregion Serialize fields

    #region Properties

    /// <summary>
    /// Состояние прицеливания, нятяжения тетевы
    /// </summary>
    public bool IsAiming { get; private set; }

    /// <summary>
    /// Cостояние, когда тетива натянута
    /// </summary>
    public bool IsAimingLoaded { get; private set; }

    /// <summary>
    /// Состояние от начала до конца анимации перезарядки
    /// </summary>
    public bool IsReloading { get; private set; }
    
    /// <summary>
    /// Состояние от начала анимации натягивания, до конца анимация пуска стрелы
    /// </summary>
    public bool IsFastShooting{ get; private set; }

    /// <summary>
    /// Аудиоконтроллер для воспроизведения звук лука
    /// </summary>
    public BowAudioController AudioController { get; private set; }

    /// <summary>
    /// Аниматор лука
    /// </summary>
    public Animator Animator { get; private set; }

    /// <summary>
    /// Ссылка на компонент PlayerUnit
    /// </summary>
    public Player Player { get; private set; }

    /// <summary>
    /// Сила выстрела
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
            // Быстрый выстрел (Нажать ЛКМ)
            if (!IsReloading && !IsFastShooting && !IsAiming && Input.GetMouseButtonDown(0))
            {
                Animator.SetTrigger(HashAnimStringWeapon.IsFastShot);
            }

            // Прицеливаемся (Нажать ПКМ)
            if (!IsReloading && !IsFastShooting && Input.GetMouseButtonDown(1))
            {
                Animator.SetBool(HashAnimStringWeapon.IsAimingLoad, true);

                _isZoomed = true;
            }

            // Выходим из прицеливания (Отпустить ПКМ)
            if ((IsAiming || IsAimingLoaded) &&  Input.GetMouseButtonUp(1))
            {
                Animator.SetBool(HashAnimStringWeapon.IsAimingLoad, false);

                _isZoomed = false;

                SetAiming(0);
                SetAimingLoaded(0);
            }

            // Прицельный выстрел (Удерживая ПКМ нажать ЛКМ)
            if (IsAiming && IsAimingLoaded && Input.GetMouseButtonDown(0))
            {
                Animator.SetTrigger(HashAnimStringWeapon.IsAimingShot);
            }

            Animator.SetFloat(HashAnimStringWeapon.PlayerSpeed, Player.KinematicCharacterMotor.Velocity.magnitude / (Player.MovementSpeed.Max/100f));

            AimingZoom();
        }
    }

    /// <summary>
    /// Приближение камеры при причеливании
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
    /// Метод блокирования управления
    /// </summary>
    /// <param name="isLock">Блокировать или не блокировать</param>
    private void SetLockControl(bool isLock)
    {
        _isLockControl = isLock;
    }

    /// <summary>
    /// Метод устанавливает состояние IsAiming c помощью события в анимации
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetAiming(int animEventParam)
    {
        IsAiming = animEventParam == 1;

        if (IsAiming)
        {
            if (Player)
            {
                // Замедляем скорость бега игрока при прицеливании
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max * _multiplyAimingMovementSpeed;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual/100f;

                // Блокируем возможность сменить оружие
                Player.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // Устанавливаем скорость стрельбы
                Animator.speed = Player.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (Player)
            {
                // Возращаем скорость бега игрока в исходное состояние
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual/100f;

                // Разбокируем возможность сменить оружие
                Player.WeaponController.IsBlockChangeWeapon = false;
            }

            if (Animator)
            {
                // Возращаем скорость аниматора в исходное состояние
                Animator.speed = 1;
            }
        }
    }

    /// <summary>
    /// Метод устанавливает состояние IsAimingLoaded c помощью события в анимации
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetAimingLoaded(int animEventParam)
    {
        IsAimingLoaded = animEventParam == 1;
    }

    /// <summary>
    /// Метод устанавливает состояние IsReloading c помощью события в анимации
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetReloading(int animEventParam)
    {
        IsReloading = animEventParam == 1;

        if (IsReloading)
        {
            if (Player)
            {
                // Блокируем возможность сменить оружие
                Player.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // Устанавливаем скорость стрельбы
                Animator.speed = Player.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (Player)
            {
                // Разблокируем игроку возможность сменить оружие
                Player.WeaponController.IsBlockChangeWeapon = false;
            }

            if (Animator)
            {
                // Возращаем скорость аниматора в исходное состояние
                Animator.speed = 1;
            }
        }
    }

    /// <summary>
    /// Метод устанавливает состояние IsFastShooting c помощью события в анимации
    /// </summary>
    /// <param name="animEventParam">1 = true, 0 = false</param>
    private void SetFastShooting(int animEventParam)
    {
        IsFastShooting = animEventParam == 1;

        if (IsFastShooting)
        {
            if (Player)
            {
                // Замедляем скорость бега игрока при стрельбе
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max * _multiplyAimingMovementSpeed;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual / 100f;

                // Блокируем возможность сменить оружие
                Player.WeaponController.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // Устанавливаем скорость стрельбы
                Animator.speed = Player.AttackSpeed.Actual / 100f;
            }
        }
        else
        {
            if (Player)
            {
                // Возращаем скорость бега игрока в исходное состояние
                Player.MovementSpeed.Actual = Player.MovementSpeed.Max;
                Player.PlayerCharacterController.maxStableMoveSpeed = Player.MovementSpeed.Actual / 100f;

                // Разблокируем игроку возможность сменить оружие
                Player.WeaponController.IsBlockChangeWeapon = false;
            }

            if (Animator)
            {
                // Возращаем скорость аниматора в исходное состояние
                Animator.speed = 1;
            }
        }
    }

    /// <summary>
    /// Метод выстрела
    /// </summary>
    private void Shot()
    {
        // Запускаем стрелу
        _projectile?.Launch(this, Player);
    }

    /// <summary>
    /// Метод создает объект стрелы при перезарядке
    /// </summary>
    private void RespawnProjectile()
    {
        _projectile = PoolManager.Instance?.ProjectileArrowPool.GetFreeElement();

        _projectile.transform.SetParent(_arrowSpawn.transform);
        _projectile.transform.SetPositionAndRotation(_arrowSpawn.position, _arrowSpawn.rotation);
    }
    #endregion Private methods
}
