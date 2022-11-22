using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [System.Serializable]
// public class AmmoInventoryEntry
// {
//     [AmmoType]
//     public int ammoType;
//     public int amount = 0;
// }

[RequireComponent(typeof(CharacterController))]

public class PlayerCharacterController : MonoBehaviour
{
    //Urg that's ugly, maybe find a better way
    // public static PlayerCharacter2 Instance { get; protected set; }

    [SerializeField] private Camera _mainCamera;
    //[SerializeField] private Transform CameraPosition;
    // public Transform WeaponPosition;

    // public _weapon[] startingWeapons;

    //this is only use at start, allow to grant ammo in the inspector. m_AmmoInventory is used during gameplay
    // public AmmoInventoryEntry[] startingAmmo;

    [Header("Weapon Settings")]
    [SerializeField] public List<GameObject> _weapons;
    [SerializeField] private GameObject _usedWeapon;
    [SerializeField] private Transform _weaponPosition;

    [Header("Control Settings")]
    [SerializeField] private float _horizontalMouseSensitivity = 5.0f;
    [SerializeField] private float _verticalMouseSensitivity = 5.0f;
    [SerializeField] private float _playerSpeed = 5.0f;
    [SerializeField] private float _runningSpeed = 7.0f;
    [SerializeField] private float _jumpSpeed = 5.0f;
    

    // [Header("Audio")]
    // public RandomPlayer FootstepPlayer;
    // public AudioClip JumpingAudioCLip;
    // public AudioClip LandingAudioClip;
    
    private float _verticalSpeed = 0.0f;
    private float _verticalAngle;
    private float _horizontalAngle;

    private bool _isLockControl = false;
    
    public float Speed { get; private set; } = 0.0f;
    public bool CanPause { get; set; } = true;

    //public bool isGrounded => _isGrounded; //??? Надо ли это нам вообще  (свойства только для чтения)(член, воплощающий выражение)

    private bool _isGrounded = true;
    private float _groundedTimer;
    private float _speedAtJump = 0.0f;

    private CharacterController _characterController;

    // List<_weapon> m_Weapons = new List<_weapon>();
    // Dictionary<int, int> m_AmmoInventory = new Dictionary<int, int>();

    private void Awake()
    {
        // Instance = this;

        Messenger<bool>.AddListener(GameSceneManager.Event.PAUSE_GAME, LockControl);
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameSceneManager.Event.PAUSE_GAME, LockControl);
    }


    private void Start()
    {   
        // Блокируем курсор и делаем его невидимым 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        // _mainCamera.transform.SetParent(CameraPosition, false);
        // _mainCamera.transform.localPosition = Vector3.zero;
        // _mainCamera.transform.localRotation = Quaternion.identity;

        _characterController = GetComponent<CharacterController>();

        // Задаем начальные значение поворота персонажа 
        _verticalAngle = 0.0f;
        _horizontalAngle = transform.localEulerAngles.y;
    }

    private void Update()
    {
        bool wasGrounded = _isGrounded;
        bool loosedGrounding = false;
        
        //we define our own grounded and not use the Character controller one as the character controller can flicker
        //between grounded/not grounded on small step and the like. So we actually make the controller "not grounded" only
        //if the character controller reported not being grounded for at least .5 second;
        if (!_characterController.isGrounded)
        {
            if (_isGrounded)
            {
                _groundedTimer += Time.deltaTime;
                if (_groundedTimer >= 0.5f)
                {
                    loosedGrounding = true;
                    _isGrounded = false;
                }
            }
        }
        else
        {
            _groundedTimer = 0.0f;
            _isGrounded = true;
        }

        Vector3 move = Vector3.zero;

        if (!_isLockControl)
        {
            // Прыжок
            // --------------------------------------------------------------------
            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                _verticalSpeed = _jumpSpeed;
                _isGrounded = false;
                loosedGrounding = true;
                // FootstepPlayer.PlayClip(JumpingAudioCLip, 0.8f,1.1f);
            }
            
            bool running = Input.GetButton("Run");
            float actualSpeed = running ? _runningSpeed : _playerSpeed;

            if (loosedGrounding)
            {
                _speedAtJump = actualSpeed;
            }
            // --------------------------------------------------------------------

            // Перемещение персонажа WASD
            // --------------------------------------------------------------------
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (move.sqrMagnitude > 1.0f)
                move.Normalize();

            float usedSpeed = _isGrounded ? actualSpeed : _speedAtJump;
            
            move = move * usedSpeed * Time.deltaTime;
            
            move = transform.TransformDirection(move);
            _characterController.Move(move);
            // --------------------------------------------------------------------
            
            // Поворот персонажа налево/направо
            // --------------------------------------------------------------------
            float turnPlayer =  Input.GetAxis("Mouse X") * _horizontalMouseSensitivity;
            _horizontalAngle += turnPlayer;
            
            // Делаем значение поворота персонажа в предлах от 0 до 360 градусов
            if (_horizontalAngle > 360) 
                _horizontalAngle -= 360.0f;
            if (_horizontalAngle < 0) 
                _horizontalAngle += 360.0f;
            
            Vector3 currentAngles = transform.localEulerAngles;
            currentAngles.y = _horizontalAngle;
            transform.localEulerAngles = currentAngles;
            // --------------------------------------------------------------------

            // Поворот камеры вверх/вниз
            // --------------------------------------------------------------------
            var turnCam = -Input.GetAxis("Mouse Y");
            turnCam = turnCam * _verticalMouseSensitivity;
            _verticalAngle = Mathf.Clamp(turnCam + _verticalAngle, -89.0f, 89.0f);
            currentAngles = _mainCamera.transform.localEulerAngles;
            currentAngles.x = _verticalAngle;
            _mainCamera.transform.localEulerAngles = currentAngles;
            // --------------------------------------------------------------------
        }

        // Падение/гравитация
        // --------------------------------------------------------------------
        _verticalSpeed -= 11.8f * Time.deltaTime;
        var verticalMove = new Vector3(0, _verticalSpeed * Time.deltaTime, 0);
        var flag = _characterController.Move(verticalMove); // Возращает направление столкновения персонажа (Below (Низ контроллера))
        // Если мы на земле задаем вертикальную скорость около нуля, т.к. она постоянно увеличивается со временем
        if (flag == CollisionFlags.Below)
        {
            _verticalSpeed = -0.3f;
        }

        if (!wasGrounded && _isGrounded)
        {
            // FootstepPlayer.PlayClip(LandingAudioClip, 0.8f,1.1f);
        }
        // --------------------------------------------------------------------
    }

    /// <summary>
    /// Метод блокирования управления
    /// </summary>
    /// <param name="isPaused">Блокировать или не блокировать</param>
    private void LockControl(bool isPaused)
    {
        _isLockControl = isPaused;
    }
}
