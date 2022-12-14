using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectDamage))]
[RequireComponent(typeof(BowAudioController))]
public class LightBow : MonoBehaviour, IBowWeapon
{
    #region Serialize fields
    [Header("Weapon")]
    [SerializeField] private string _name = "Легкий лук";
    [SerializeField] private float _shotForce = 16; // Сила выстрела

    [Header("Projectile")]
    //[SerializeField] private GameObject _selectedPrefabArrow; // Префаб для стрелы
    [SerializeField] private GameObject _prefabArrow; // Объект из которого будем делать дубликаты стрел, выстeпает в роле префаба
    [SerializeField] private Transform _arrowSpawn;

    [Header("AimingZoom")]
    [SerializeField] private float _zoomFOV = 30f;
    [SerializeField] private float _originalFOV = 60f;
    [SerializeField] private float _zoomStepTime = 4f;
    #endregion Serialize fields

    #region Properties
    /// <summary>
    /// Название лука
    /// </summary>
    public string Name { get => _name; private set => _name = value; }

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
    /// Словарь хранит модификаторы атаки установленные на луке
    /// </summary>
    public Dictionary<Type, IModifier> AttackModifaers { get; private set; }

    /// <summary>
    /// Аудиоконтроллер для воспроизведения звук лука
    /// </summary>
    public BowAudioController AudioController { get; private set; }

    /// <summary>
    /// Аниматор лука
    /// </summary>
    public Animator Animator { get; private set; }

    /// <summary>
    /// Ссылка на компонент Player
    /// </summary>
    public Player Player { get; private set; }
    #endregion Properties

    #region Private fields
    private bool _isZoomed;

    private bool _isLockControl;

    // Объект в котором будем хранить клон стрелы, его же будем выстреливать
    private GameObject _cloneArrow;
    private ProjectileArrow _cloneProjectileArrow;

    public Flame Flame = new Flame();
    public Slowdown Slowdown = new Slowdown();
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
        Player = GetComponentInParent<Player>();

        AttackModifaers = new Dictionary<Type, IModifier>();

        AttackModifaers[typeof(DirectDamage)] = GetComponent<DirectDamage>();
        AttackModifaers[typeof(CriticalDamage)] = GetComponent<CriticalDamage>();
        AttackModifaers[typeof(Penetration)] = GetComponent<Penetration>();
        AttackModifaers[typeof(FireArrow)] = GetComponent<FireArrow>();
        AttackModifaers[typeof(SlowArrow)] = GetComponent<SlowArrow>();
        AttackModifaers[typeof(Mjolnir)] = GetComponent<Mjolnir>();

        RespawnProjectile();
    }

    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (!_isLockControl)
        {
            // Быстрый выстрел (Нажать ЛКМ)
            if (!IsReloading && !IsFastShooting && !Player.IsSprinting && !IsAiming && Input.GetMouseButtonDown(0))
            {
                Animator.SetTrigger(HashAnimStringWeapon.IsFastShot);
            }

            // Прицеливаемся (Нажать ПКМ)
            if (!IsReloading && !IsFastShooting && !Player.IsSprinting && Input.GetMouseButtonDown(1))
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

            Animator.SetFloat(HashAnimStringWeapon.PlayerSpeed, Player.ActualSpeed / Player.MaxRunSpeed);
            Animator.SetBool(HashAnimStringWeapon.IsSprint, Player.IsSprinting);

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
        else if (!_isZoomed && !Player.IsSprinting)
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
                Player.CurrentMaxRunSpeed = Player.MaxRunSpeed * 0.5f;

                // Блокируем игроку возможность спринтовать
                Player.IsBlockSprint = true;

                // Блокируем возможность сменить оружие
                Player.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // Устанавливаем скорость стрельбы
                Animator.speed = Player.CurrentMaxAttackSpeed / 100;
            }
        }
        else
        {
            if (Player)
            {
                // Возращаем скорость бега игрока в исходное состояние
                Player.CurrentMaxRunSpeed = Player.MaxRunSpeed;


                // Разбокируем игроку возможность спринтовать
                Player.IsBlockSprint = false;

                // Разбокируем возможность сменить оружие
                Player.IsBlockChangeWeapon = false;
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
                // Блокируем игроку возможность спринтовать
                Player.IsBlockSprint = true;

                // Блокируем возможность сменить оружие
                Player.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // Устанавливаем скорость стрельбы
                Animator.speed = Player.CurrentMaxAttackSpeed / 100;
            }
        }
        else
        {
            if (Player)
            {
                // Разблокируем игроку возможность спринтовать
                Player.IsBlockSprint = false;

                // Разблокируем игроку возможность сменить оружие
                Player.IsBlockChangeWeapon = false;
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
                Player.CurrentMaxRunSpeed = Player.MaxRunSpeed * 0.5f;

                // Блокируем игроку возможность спринтовать
                Player.IsBlockSprint = true;

                // Блокируем возможность сменить оружие
                Player.IsBlockChangeWeapon = true;
            }

            if (Animator)
            {
                // Устанавливаем скорость стрельбы
                Animator.speed = Player.CurrentMaxAttackSpeed / 100;
            }
        }
        else
        {
            if (Player)
            {
                // Возращаем скорость бега игрока в исходное состояние
                Player.CurrentMaxRunSpeed = Player.MaxRunSpeed;

                // Разблокируем игроку возможность спринтовать
                Player.IsBlockSprint = false;

                // Разблокируем игроку возможность сменить оружие
                Player.IsBlockChangeWeapon = false;
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
        if(_cloneProjectileArrow)
        {
            _cloneProjectileArrow?.Launch(_shotForce);
        }
    }

    /// <summary>
    /// Метод создает объект стрелы при перезарядке
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
