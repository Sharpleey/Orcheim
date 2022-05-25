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

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CharacterController))]

public class PlayerCharacter2 : MonoBehaviour
{
    //Urg that's ugly, maybe find a better way
    // public static PlayerCharacter2 Instance { get; protected set; }

    [SerializeField] private Camera _mainCamera;
    // public Camera WeaponCamera;
    
    // public Transform CameraPosition;
    // public Transform WeaponPosition;
    
    // public Weapon[] startingWeapons;

    //this is only use at start, allow to grant ammo in the inspector. m_AmmoInventory is used during gameplay
    // public AmmoInventoryEntry[] startingAmmo;

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
  
    [Header("Other")]
    public TextMeshProUGUI infoText;
    
    private float _verticalSpeed = 0.0f;
    private float _verticalAngle;
    private float _horizontalAngle;

    private bool _isPaused = false; //??? Надо понять для чего оно
    // int m_CurrentWeapon;
    
    public float Speed { get; private set; } = 0.0f;

    public bool LockControl { get; set; }
    public bool CanPause { get; set; } = true;

    // public bool isGrounded => _isGrounded; //??? Надо ли это нам вообще  (свойства только для чтения)(член, воплощающий выражение)

    private bool _isGrounded;
    private float _groundedTimer;
    private float _speedAtJump = 0.0f;

    private CharacterController _characterController;

    // List<Weapon> m_Weapons = new List<Weapon>();
    // Dictionary<int, int> m_AmmoInventory = new Dictionary<int, int>();

    void Awake()
    {
        // Instance = this;
    }
    
    void Start()
    {   
        // Блокируем курсор и делаем его невидимым 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _isPaused = false;
        _isGrounded = true;
        
        // _mainCamera.transform.SetParent(CameraPosition, false);
        // _mainCamera.transform.localPosition = Vector3.zero;
        // _mainCamera.transform.localRotation = Quaternion.identity;

        _characterController = GetComponent<CharacterController>();

        // for (int i = 0; i < startingWeapons.Length; ++i)
        // {
        //     PickupWeapon(startingWeapons[i]);
        // }

        // for (int i = 0; i < startingAmmo.Length; ++i)
        // {
        //     ChangeAmmo(startingAmmo[i].ammoType, startingAmmo[i].amount);
        // }
        
        // m_CurrentWeapon = -1;
        // ChangeWeapon(0);

        // for (int i = 0; i < startingAmmo.Length; ++i)
        // {
        //     m_AmmoInventory[startingAmmo[i].ammoType] = startingAmmo[i].amount;
        // }

        // Задаем начальные значение поворота персонажа 
        _verticalAngle = 0.0f;
        _horizontalAngle = transform.localEulerAngles.y;
    }

    void Update()
    {
        infoText.text = "";
        // if (CanPause && Input.GetButtonDown("Menu"))
        // {
        //     PauseMenu.Instance.Display();
        // }
        
        // FullscreenMap.Instance.gameObject.SetActive(Input.GetButton("Map"));

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

        // Speed = 0;
        Vector3 move = Vector3.zero;

        if (!_isPaused && !LockControl)
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
  
            // m_Weapons[m_CurrentWeapon].triggerDown = Input.GetMouseButton(0);

            // Speed = move.magnitude / (_playerSpeed * Time.deltaTime); //???
            // infoText.text += "\n" + "Speed:" + Speed.ToString();

            // if (Input.GetButton("Reload"))
            //     m_Weapons[m_CurrentWeapon].Reload();

            // if (Input.GetAxis("Mouse ScrollWheel") < 0)
            // {
            //     ChangeWeapon(m_CurrentWeapon - 1);
            // }
            // else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            // {
            //     ChangeWeapon(m_CurrentWeapon + 1);
            // }
            
            //Key input to change weapon

            // for (int i = 0; i < 10; ++i)
            // {
            //     if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            //     {
            //         int num = 0;
            //         if (i == 0)
            //             num = 10;
            //         else
            //             num = i - 1;

            //         if (num < m_Weapons.Count)
            //         {
            //             ChangeWeapon(num);
            //         }
            //     }
            // }
        }

        // Падение/гравитация
        // --------------------------------------------------------------------
        _verticalSpeed -= 9.8f * Time.deltaTime;
        var verticalMove = new Vector3(0, _verticalSpeed * Time.deltaTime, 0);
        var flag = _characterController.Move(verticalMove); // Возращает направление столкновения персонажа (Below (Низ контроллера))
        // Если мы на земле задаем вертикальную скорость около нуля, т.к. она постоянно увеличивается со временем
        if (flag == CollisionFlags.Below)
        {
            _verticalSpeed = -0.3f;
        }

        // infoText.text += "\n" + "wasGrounded:" + wasGrounded.ToString();
        // infoText.text += "\n" + "_isGrounded:" + _isGrounded.ToString();
        // infoText.text += "\n" + "loosedGrounding:" + loosedGrounding.ToString();

        if (!wasGrounded && _isGrounded)
        {
            // FootstepPlayer.PlayClip(LandingAudioClip, 0.8f,1.1f);
        }
        // --------------------------------------------------------------------
    }

    // public void DisplayCursor(bool display)
    // {
    //     _isPaused = display;
    //     Cursor.lockState = display ? CursorLockMode.None : CursorLockMode.Locked;
    //     Cursor.visible = display;
    // }

    // void PickupWeapon(Weapon prefab)
    // {
    //     //TODO : maybe find a better way than comparing name...
    //     if (m_Weapons.Exists(weapon => weapon.name == prefab.name))
    //     {//if we already have that weapon, grant a clip size of the ammo type instead
    //         ChangeAmmo(prefab.ammoType, prefab.clipSize);
    //     }
    //     else
    //     {
    //         var w = Instantiate(prefab, WeaponPosition, false);
    //         w.name = prefab.name;
    //         w.transform.localPosition = Vector3.zero;
    //         w.transform.localRotation = Quaternion.identity;
    //         w.gameObject.SetActive(false);
            
    //         w.PickedUp(this);
            
    //         m_Weapons.Add(w);
    //     }
    // }

    // void ChangeWeapon(int number)
    // {
    //     if (m_CurrentWeapon != -1)
    //     {
    //         m_Weapons[m_CurrentWeapon].PutAway();
    //         m_Weapons[m_CurrentWeapon].gameObject.SetActive(false);
    //     }

    //     m_CurrentWeapon = number;

    //     if (m_CurrentWeapon < 0)
    //         m_CurrentWeapon = m_Weapons.Count - 1;
    //     else if (m_CurrentWeapon >= m_Weapons.Count)
    //         m_CurrentWeapon = 0;
        
    //     m_Weapons[m_CurrentWeapon].gameObject.SetActive(true);
    //     m_Weapons[m_CurrentWeapon].Selected();
    // }

    // public int GetAmmo(int ammoType)
    // {
    //     int value = 0;
    //     m_AmmoInventory.TryGetValue(ammoType, out value);

    //     return value;
    // }

    // public void ChangeAmmo(int ammoType, int amount)
    // {
    //     if (!m_AmmoInventory.ContainsKey(ammoType))
    //         m_AmmoInventory[ammoType] = 0;

    //     var previous = m_AmmoInventory[ammoType];
    //     m_AmmoInventory[ammoType] = Mathf.Clamp(m_AmmoInventory[ammoType] + amount, 0, 999);

    //     if (m_Weapons[m_CurrentWeapon].ammoType == ammoType)
    //     {
    //         if (previous == 0 && amount > 0)
    //         {//we just grabbed ammo for a weapon that add non left, so it's disabled right now. Reselect it.
    //             m_Weapons[m_CurrentWeapon].Selected();
    //         }
            
    //         WeaponInfoUI.Instance.UpdateAmmoAmount(GetAmmo(ammoType));
    //     }
    // }

    // public void PlayFootstep()
    // {
    //     FootstepPlayer.PlayRandom();
    // }
}
