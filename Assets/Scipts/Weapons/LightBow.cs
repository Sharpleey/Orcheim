using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBow : MonoBehaviour, IBowWeapon
{
    #region Serialize Fields
    [Header("Weapon Settings")]
    [SerializeField] private string _name = "Легкий лук";
    [SerializeField] private float _damageSpread = 0.25f;
    [SerializeField] private float _timeReloadShot = 0.5f;

    [Header("Projectile Settings")]
    //[SerializeField] private GameObject _selectedPrefabArrow; // Префаб для стрелы
    [SerializeField] private GameObject _originalArrow; // Объект из которого будем делать дубликаты стрел
    [SerializeField] private Transform _arrowSpawn;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _shotForce = 8; // Сила выстрела

    [Header("Attack Modifiers")]
    [SerializeField] private bool _onPenetrationMod = false;
    [SerializeField] private bool _onCriticalDamageMod = false;
    [SerializeField] private bool _onFireArrowMod = false;
    [SerializeField] private bool _onSlowArrowMod = false;
    [SerializeField] private bool _onMjolnirMod = false;
    [SerializeField] private const bool _onDirectDamageMod = true;

    [Header("Others")]
    [SerializeField] private bool _debugMod = false;

    #endregion Serialize Fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
    public float DamageSpread
    {
        get
        {
            return _damageSpread;
        }
        private set
        {
            if (value < 0.0f)
            {
                _damageSpread = 0;
                return;
            }
            if (value > 1.0f)
            {
                _damageSpread = 1.0f;
                return;
            }
            _damageSpread = value;
        }
    }
    public float TimeReloadShot
    {
        get
        {
            return _timeReloadShot;
        }
        set
        {
            if (value <= 0.0f)
            {
                _damageSpread = 0.1f;
                return;
            }
            _damageSpread = value;
        }
    }

    #endregion Properties

    #region Fields
    private bool _isAiming = false;
    private bool _isReload = false;

    private Quaternion _defaultBowRotate;

    // Объект в котором будем хранить клон стрелы, его же будем выстреливать
    private GameObject _cloneArrow;

    // Для хранения модификаторов атаки
    public List<IModifier> AttackMods = new List<IModifier>();

    #endregion Fields

    #region Methods
    private void Awake()
    {
        Name = _name;
        DamageSpread = _damageSpread;
    }

    private void Start()
    {

        //_defaultBowRotate = transform.rotation;

        // Устанавливаем модификаторы атаки на original arrow
        SetupAttackModifiersOnTheOriginalArrow();

        // Заряжаем
        SpawnArrow();
    }

    private void Update()
    {
        // ПКМ
        if (Input.GetMouseButtonDown(1))
        {
            if(_debugMod) Debug.Log($"Прицеливание");

            _isAiming = true;

            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // _pointer.position = hit.point;
                // Debug.Log(hit.point);
                //transform.LookAt(hit.point);

                //transform.Rotate(0.0f, 0.0f, -90.0f);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (_debugMod) Debug.Log($"Отмена Прицеливание");
            //transform.rotation = _defaultBowRotate.rotation; //TODO
            _isAiming = false;
        }

        if (Input.GetMouseButtonDown(0)
            //&& _isArrowInBowstring 
            && !_isReload)
        {
            if (_isAiming)
            {
                // ChargedShot
                Shot();
            }
            else
            {
                // Shot
                Shot();
            }

        }
    }

    public void Shot()
    {
        if (_debugMod) Debug.Log($"Выстрел");

        _cloneArrow.transform.parent = null;

        Rigidbody arrowRb = _cloneArrow.GetComponent<Rigidbody>();
        if (arrowRb != null)
        {
            arrowRb.isKinematic = false;

            // Назначаем физическому телу скорость.
            arrowRb.AddForce((_cloneArrow.transform.forward) * _shotForce, ForceMode.Impulse);
        }

        ProjectileArrow projectileArrow = _cloneArrow.GetComponent<ProjectileArrow>();
        if (projectileArrow != null)
        { 
            projectileArrow.isArrowInBowstring = false;
        }

        StartCoroutine(Reload(TimeReloadShot));
    }

    public void ChargedShot()
    {
        if (_debugMod) Debug.Log($"Заряженный выстрел");
    }

    private IEnumerator Reload(float timeReload)
    {
        _isReload = true;

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(timeReload);

        SpawnArrow();

        _isReload = false;
    }

    private void SpawnArrow()
    {
        _cloneArrow = Instantiate(_originalArrow);
        _cloneArrow.SetActive(true);

        _cloneArrow.transform.parent = transform;

        _cloneArrow.transform.position = _arrowSpawn.position;
        _cloneArrow.transform.rotation = _arrowSpawn.rotation;

        Rigidbody arrowRb = _cloneArrow.GetComponent<Rigidbody>();
        if (arrowRb != null)
            arrowRb.isKinematic = true;

        //ProjectileArrow projectileArrow = _cloneArrow.GetComponent<ProjectileArrow>();
        //if (projectileArrow != null)
        //    projectileArrow.bowAttackModifiers = attackModifiers;
    }

    private void SetupAttackModifiersOnTheOriginalArrow()
    {
        // TODO избавиться от if-ов
        if(_onDirectDamageMod)
        {
            IModifier modifier = _originalArrow.GetComponent<DirectDamage>();
            if (modifier == null)
            {
                modifier = _originalArrow.AddComponent<DirectDamage>() as IModifier;
            }
            AttackMods.Add(modifier);
        }
        if (_onPenetrationMod)
        {
            IModifier modifier = _originalArrow.GetComponent<Penetration>();
            if (modifier == null)
            {
                modifier = _originalArrow.AddComponent<Penetration>() as IModifier;
            }
            AttackMods.Add(modifier);
        }
        //if (_onCriticalDamageMod)
        //{
        //    IModifier mod = _originalArrow.AddComponent(typeof(DirectDamage)) as IModifier;
        //    AttackMods.Add(mod);
        //}
        //if (_onFireArrowMod)
        //{
        //    IModifier mod = _originalArrow.AddComponent(typeof(DirectDamage)) as IModifier;
        //    AttackMods.Add(mod);
        //}
        //if (_onSlowArrowMod)
        //{
        //    IModifier mod = _originalArrow.AddComponent(typeof(DirectDamage)) as IModifier;
        //    AttackMods.Add(mod);
        //}
        //if (_onMjolnirMod)
        //{
        //    IModifier mod = _originalArrow.AddComponent(typeof(DirectDamage)) as IModifier;
        //    AttackMods.Add(mod);
        //}
    }

    #endregion Methods
}
