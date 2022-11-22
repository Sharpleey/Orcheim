using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectDamage))]
[RequireComponent(typeof(AudioController))]
public class LightBow : MonoBehaviour, IBowWeapon
{
    #region Serialize fields
    [Header("Weapon Settings")]
    [SerializeField] private string _name = "Легкий лук";
    [SerializeField] private float _timeReloadShot = 0.5f;

    [Header("Projectile Settings")]
    //[SerializeField] private GameObject _selectedPrefabArrow; // Префаб для стрелы
    [SerializeField] private GameObject _prefabArrow; // Объект из которого будем делать дубликаты стрел, выстeпает в роле префаба
    [SerializeField] private Transform _arrowSpawn;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _shotForce = 8; // Сила выстрела

    #endregion Serialize fields

    #region Properties
    /// <summary>
    /// Название лука
    /// </summary>
    public string Name { get => _name; private set => _name = value; }
    /// <summary>
    /// Время перезарядки в секундах
    /// </summary>
    public float TimeReloadShot
    {
        get
        {
            return _timeReloadShot;
        }
        set
        {
            if (value < 0.1f)
            {
                _timeReloadShot = 0.1f;
                return;
            }
            _timeReloadShot = value;
        }
    }
    /// <summary>
    /// Словарь хранит модификаторы атаки установленные на луке
    /// </summary>
    public Dictionary<Type, IModifier> AttackModifaers { get; private set; }
    #endregion Properties

    #region Private fields
    private bool _isAiming = false;
    private bool _isReload = false;

    private bool _isLockControl = false;

    private Animator _animator;
    private AudioController _audioController;

    private Quaternion _defaultBowRotate;

    // Объект в котором будем хранить клон стрелы, его же будем выстреливать
    private GameObject _cloneArrow;
    private ProjectileArrow _cloneProjectileArrow;

    private IEnumerator _coroutineReload;

    #endregion Private fields

    #region Mono
    private void Awake()
    {
        Name = _name;
        TimeReloadShot = _timeReloadShot;

        Messenger<bool>.AddListener(GameSceneManager.Event.PAUSE_GAME, LockControl);
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameSceneManager.Event.PAUSE_GAME, LockControl);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioController = GetComponent<AudioController>();

        AttackModifaers = new Dictionary<Type, IModifier>();

        AttackModifaers[typeof(DirectDamage)] = GetComponent<DirectDamage>();
        AttackModifaers[typeof(CriticalDamage)] = GetComponent<CriticalDamage>();
        AttackModifaers[typeof(Penetration)] = GetComponent<Penetration>();
        AttackModifaers[typeof(FireArrow)] = GetComponent<FireArrow>();
        AttackModifaers[typeof(SlowArrow)] = GetComponent<SlowArrow>();
        AttackModifaers[typeof(Mjolnir)] = GetComponent<Mjolnir>();

        SpawnArrow();
    }

    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (!_isLockControl)
        {
            // TODO ----------------------------------------
            // ПКМ
            // Idle -> Aiming
            if (Input.GetMouseButtonDown(1))
            {
                _animator.SetBool(HashAnimStringWeapon.IsIdle, false);
                _animator.SetBool(HashAnimStringWeapon.IsAimingLoad, true);
            }

            // Отпустить ПКМ
            // Aiming -> Idle
            if (Input.GetMouseButtonUp(1))
            {
                _animator.SetBool(HashAnimStringWeapon.IsAimingLoad, false);
                _animator.SetBool(HashAnimStringWeapon.IsIdle, true);
            }

            // Удерживание ПКМ + ЛКМ
            // Aiming -> Shoot
            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            {
                _animator.SetBool(HashAnimStringWeapon.IsAimingLoad, false);

                ChargedShot();
            }

            // Idle -> Run
            if (Input.GetKeyDown(KeyCode.W))
            {
                _animator.SetBool(HashAnimStringWeapon.IsIdle, false);
                _animator.SetBool(HashAnimStringWeapon.IsRun, true);
            }

            // Run -> Idle
            if (Input.GetKeyUp(KeyCode.W))
            {
                _animator.SetBool(HashAnimStringWeapon.IsRun, false);
                _animator.SetBool(HashAnimStringWeapon.IsIdle, true);
            }

            // Run -> Sprint
            if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                _animator.SetBool(HashAnimStringWeapon.IsRun, false);
                _animator.SetBool(HashAnimStringWeapon.IsIdle, false);
                _animator.SetBool(HashAnimStringWeapon.IsSprint, true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _animator.SetBool(HashAnimStringWeapon.IsSprint, false);
            }

            // Sprint -> Run
            if (Input.GetKeyUp(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                _animator.SetBool(HashAnimStringWeapon.IsRun, true);
                _animator.SetBool(HashAnimStringWeapon.IsSprint, false);
            }

            // Sprint -> Idle
            if (Input.GetKeyUp(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                _animator.SetBool(HashAnimStringWeapon.IsIdle, true);
                _animator.SetBool(HashAnimStringWeapon.IsSprint, false);
            }

            // ЛКМ
            if (Input.GetMouseButtonDown(0) && !_isReload && !Input.GetMouseButton(1))
            {
                FastShot();
            }
        }
    }

    /// <summary>
    /// Метод блокирования управления
    /// </summary>
    /// <param name="isPaused">Блокировать или не блокировать</param>
    private void LockControl(bool isPaused)
    {
        _isLockControl = isPaused;
    }

    private void FastShot()
    {
        _animator.SetTrigger(HashAnimStringWeapon.IsShoot);
    }

    private void ChargedShot()
    {
        _animator.SetTrigger(HashAnimStringWeapon.IsShoot);
    }

    private void ReleaseArrow()
    {
        //_isReload = true;
        // Запускаем стрелу
        _cloneProjectileArrow.Launch(_shotForce);
    }

    private void SpawnArrow()
    {
        _isReload = true;

        _cloneArrow = Instantiate(_prefabArrow);

        _cloneArrow.transform.parent = _arrowSpawn.transform;

        _cloneArrow.transform.position = _arrowSpawn.position;
        _cloneArrow.transform.rotation = _arrowSpawn.rotation;
        //_cloneArrow.transform.localScale = _arrowSpawn.lossyScale;

        _cloneProjectileArrow = _cloneArrow.GetComponent<ProjectileArrow>();

        _cloneArrow.GetComponent<Rigidbody>().isKinematic = true;

        _isReload = false;
    }
    #endregion Private methods
}
