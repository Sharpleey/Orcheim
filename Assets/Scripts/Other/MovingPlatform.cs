using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovingPlatform : MonoBehaviour, IMoverController
{
    public struct MyMovingPlatformState
    {
        public PhysicsMoverState MoverState;
        public float DirectorTime;
    }

    public PhysicsMover Mover;

    public PlayableDirector Director;

    // Start is called before the first frame update
    void Start()
    {
        Mover.MoverController = this;
    }

    // ��� ���������� ������ FixedUpdate ����� PhysicsMover, ����� �������� ���, � ����� ������� �� ������ �������
    public void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
    {
        // ��������� ������� ����� ���������
        Vector3 _positionBeforeAnim = transform.position;
        Quaternion _rotationBeforeAnim = transform.rotation;

        // Update animation
        EvaluateAtTime(Time.time);

        // Set our platform's goal pose to the animation's
        goalPosition = transform.position;
        goalRotation = transform.rotation;

        // Reset the actual transform pose to where it was before evaluating. 
        // ��� �������� ��� ����, ����� �������� �������� ����� ���� ���������� �������, � �� ���������.
        transform.position = _positionBeforeAnim;
        transform.rotation = _rotationBeforeAnim;
    }

    public void EvaluateAtTime(double time)
    {
        Director.time = time % Director.duration;
        Director.Evaluate();
    }
}
