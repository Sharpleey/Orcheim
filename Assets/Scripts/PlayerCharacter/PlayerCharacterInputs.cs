using UnityEngine;

public struct PlayerCharacterInputs
{
    public Quaternion cameraRotation;
    public float moveAxisForward;
    public float moveAxisRight;
    public bool isJumpDown;
    public bool isJumpHeld;
    public bool isChargingDown;
    public bool isNoClipDown;
}