using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerCharacterController _characterController;
    [SerializeField] private CharacterCamera _characterCamera;

    public Transform cameraFollowPoint;

    private Vector3 _lookInputVector = Vector3.zero;

    #region Mono
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Tell camera to follow transform
        _characterCamera.SetFollowTransform(cameraFollowPoint);
        _characterCamera.TargetDistance = 0f;

        // Ignore the character's collider(s) for camera obstruction checks
        _characterCamera.IgnoredColliders.Clear();
        _characterCamera.IgnoredColliders.AddRange(_characterController.GetComponentsInChildren<Collider>());
    }

    private void Update()
    {
        HandleCharacterInput();
    }

    private void LateUpdate()
    {
        HandleCameraInput();
    }
    #endregion Mono

    #region Private methods
    private void HandleCameraInput()
    {
        // Create the look input vector for the camera
        float mouseLookAxisUp = Input.GetAxisRaw(HashInputString.MOUSE_Y);
        float mouseLookAxisRight = Input.GetAxisRaw(HashInputString.MOUSE_X);

        _lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

        // Prevent moving the camera while the cursor isn't locked
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            _lookInputVector = Vector3.zero;
        }

        // Apply inputs to the camera
        _characterCamera.UpdateWithInput(Time.deltaTime, _lookInputVector);
    }

    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        // Build the CharacterInputs struct
        characterInputs.moveAxisForward = Input.GetAxisRaw(HashInputString.VERTICAL);
        characterInputs.moveAxisRight = Input.GetAxisRaw(HashInputString.HORIZONTAL);
        characterInputs.cameraRotation = _characterCamera.Transform.rotation;
        characterInputs.isJumpDown = Input.GetKeyDown(KeyCode.Space);
        characterInputs.isJumpHeld = Input.GetKey(KeyCode.Space);
        characterInputs.isChargingDown = Input.GetKeyDown(KeyCode.Q);
        characterInputs.isNoClipDown = Input.GetKeyUp(KeyCode.G);

        // Apply inputs to character
        _characterController.SetInputs(ref characterInputs);

        // Apply impulse
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Оторвать от земли
            _characterController.motor.ForceUnground(0.1f);

            // Применить импульс
            _characterController.AddVelocity(_characterController.transform.forward * 20f);
        }
    }
    #endregion Private methods
}
