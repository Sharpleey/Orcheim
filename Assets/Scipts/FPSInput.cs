using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{   
    public float speed = 6.0f;
    public const float baseSpeed = 6.0f;
    public float gravity = -9.8f;


    private CharacterController _charController; 
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed; 
        float deltaZ = Input.GetAxis("Vertical") * speed; 
        
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        // Ограничиваем скорость по диагонали
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity; 

        // Делаем скорость перемещения независимой от скорости работы ПК
        movement *= Time.deltaTime;
        // Преобразуем вектор движения от локальных к глобальным координатам
        movement = transform.TransformDirection(movement);
        // Заставим этот вектор перемещать компонент CharacterController 
        _charController.Move(movement); 
    }

    void Awake() 
    {
        // Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy() 
    {
        // Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Метод, объявленный в подписчике для события SPEED_CHANGED
    private void OnSpeedChanged(float value)
    {   
        // Изменяем скорость
        speed = baseSpeed * value;
    }
}
