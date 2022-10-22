using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectDamage))]
public class LightBow : MonoBehaviour, IBowWeapon
{
    #region Serialize fields
    [Header("Weapon Settings")]
    [SerializeField] private string _name = "������ ���";
    [SerializeField] private float _timeReloadShot = 0.5f;

    [Header("Projectile Settings")]
    //[SerializeField] private GameObject _selectedPrefabArrow; // ������ ��� ������
    [SerializeField] private GameObject _prefabArrow; // ������ �� �������� ����� ������ ��������� �����, ����e���� � ���� �������
    [SerializeField] private Transform _arrowSpawn;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _shotForce = 8; // ���� ��������

    #endregion Serialize fields

    #region Properties
    /// <summary>
    /// �������� ����
    /// </summary>
    public string Name { get => _name; private set => _name = value; }
    /// <summary>
    /// ����� ����������� � ��������
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
    /// ������� ������ ������������ ����� ������������� �� ����
    /// </summary>
    public Dictionary<Type, IModifier> AttackModifaers { get; private set; }
    #endregion Properties

    #region Private fields
    private bool _isAiming = false;
    private bool _isReload = false;

    private Animator _animator;

    private Quaternion _defaultBowRotate;

    // ������ � ������� ����� ������� ���� ������, ��� �� ����� ������������
    private GameObject _cloneArrow;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        Name = _name;
        TimeReloadShot = _timeReloadShot;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        AttackModifaers = new Dictionary<Type, IModifier>();

        AttackModifaers[typeof(DirectDamage)] = GetComponent<DirectDamage>();
        AttackModifaers[typeof(CriticalDamage)] = GetComponent<CriticalDamage>();
        AttackModifaers[typeof(Penetration)] = GetComponent<Penetration>();
        AttackModifaers[typeof(FireArrow)] = GetComponent<FireArrow>();
        AttackModifaers[typeof(SlowArrow)] = GetComponent<SlowArrow>();
        AttackModifaers[typeof(Mjolnir)] = GetComponent<Mjolnir>();

        //_defaultBowRotate = transform.rotation;

        // ��������
        SpawnArrow();
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (!Managers.GameSceneManager.IsGamePaused)
        {
            // ���
            if (Input.GetMouseButtonDown(1))
            {
                //_animator.SetBool("Load", true);
                //_isAiming = true;

                //Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
                //RaycastHit hit;

                //if (Physics.Raycast(ray, out hit))
                //{
                //    _pointer.position = hit.point;
                //    Debug.Log(hit.point);
                //    transform.LookAt(hit.point);

                //    transform.Rotate(0.0f, 0.0f, -90.0f);
                //}
            }

            // ��������� ���
            if (Input.GetMouseButtonUp(1))
            {
                //_animator.SetBool("Load", false);
                //_isAiming = false;
            }

            // ���
            if (Input.GetMouseButtonDown(0) && !_isReload)
            {
                _animator.SetTrigger("FastShoot");
                Shot();
            }
        }
    }

    private void Shot()
    {
        // ��������� ������
        _cloneArrow.GetComponent<ProjectileArrow>().Launch(_shotForce);

        StartCoroutine(Reload(TimeReloadShot));
    }

    private void ChargedShot()
    {
        
    }

    private void SpawnArrow()
    {
        _cloneArrow = Instantiate(_prefabArrow);

        _cloneArrow.transform.parent = _arrowSpawn.transform;

        _cloneArrow.transform.position = _arrowSpawn.position;
        _cloneArrow.transform.rotation = _arrowSpawn.rotation;

        _cloneArrow.GetComponent<Rigidbody>().isKinematic = true;

    }

    private IEnumerator Reload(float timeReload)
    {
        _isReload = true;

        // �������� ����� yield ��������� �����������, ����� ������� ������������.
        yield return new WaitForSeconds(timeReload);

        SpawnArrow();

        _isReload = false;
    }

    //void Reload()
    //{
    //    Transform newProjectileInstance = Transform.Instantiate(projectile);

    //    Rigidbody projectileRigidbody = newProjectileInstance.GetComponent<Rigidbody>();
    //    if (projectileRigidbody != null)
    //    {
    //        projectileRigidbody.useGravity = false;
    //    }

    //    newProjectileInstance.parent = projectileParent;
    //    newProjectileInstance.localPosition = originalPosition;
    //    newProjectileInstance.localEulerAngles = originalEulerAngles;
    //    newProjectileInstance.localScale = originalScale;

    //    newProjectileInstance.GetComponent<MeshRenderer>().enabled = true;

    //    projectiles.Insert(0, newProjectileInstance);
    //}
    #endregion Private methods
}
