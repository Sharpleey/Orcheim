using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBow : MonoBehaviour, IBowWeapon
{
    #region Serialize fields
    [Header("Weapon Settings")]
    [SerializeField] private string _name = "Легкий лук";
    [SerializeField] private float _timeReloadShot = 0.5f;

    [Header("Projectile Settings")]
    //[SerializeField] private GameObject _selectedPrefabArrow; // Префаб для стрелы
    [SerializeField] private GameObject _originalArrow; // Объект из которого будем делать дубликаты стрел, выстпает в роле префаба
    [SerializeField] private Transform _arrowSpawn;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _shotForce = 8; // Сила выстрела

    [Header("Others")]
    [SerializeField] private bool _debugMod = false;
    #endregion Serialize fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
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
    #endregion Properties

    #region Private fields
    private bool _isAiming = false;
    private bool _isReload = false;

    private Quaternion _defaultBowRotate;

    // Объект в котором будем хранить клон стрелы, его же будем выстреливать
    private GameObject _cloneArrow;
    #endregion Private fields

    #region Public fields
    // Для хранения модификаторов атаки
    public IModifier[] atachedAttaksModifaers;
    #endregion Public fields

    #region Mono
    private void Awake()
    {
        Name = _name;
        TimeReloadShot = _timeReloadShot;
    }

    private void Start()
    {

        //_defaultBowRotate = transform.rotation;

        // Получаем список модификаторов установленных на стреле (Луке)
        atachedAttaksModifaers = _originalArrow.GetComponents<IModifier>();

        // Заряжаем
        SpawnArrow();
    }
    #endregion Mono

    #region Private methods
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

    private void Shot()
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

    private void ChargedShot()
    {
        if (_debugMod) Debug.Log($"Заряженный выстрел");
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

    private IEnumerator Reload(float timeReload)
    {
        _isReload = true;

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(timeReload);

        SpawnArrow();

        _isReload = false;
    }
    #endregion Private methods
}
