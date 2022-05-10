using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{   
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    // Скорость передвижения врага
    public float speed = 3.0f;
    // Базовая скорость, которая регулируется положением ползунка
    public const float baseSpeed = 3.0f; 
    // Расстояние для реакции на препятствие
    public float obstacleRange = 5.0f; 
    // Состояние врага
    private bool _alive;

    void Start() 
    { 
        _alive = true; 
    }

    // Update is called once per frame
    void Update()
    {   
        if (_alive)
        {   
            // Двигаем врага
            transform.Translate(0, 0, speed * Time.deltaTime);

            // Создаем луч, указываем позицию начала луча и его направление
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            // Запускаем конусообразный луч на 75 градусов
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {   
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {   
                        // Метод, копирующий объект-шаблон fireball
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        // Указываем позицию откуда полетит шар
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        // Указываем направление
                        _fireball.transform.rotation = transform.rotation;
                    }

                }
                else if (hit.distance < obstacleRange) // Поварачиваем на рандомный угол если впереди препятствие
                {
                    // Рандомим угол между -110 и 110
                    float angle = Random.Range(-110, 110);
                    // Поварачиваем врага на этот угол
                    transform.Rotate(0, angle, 0); 
                }
            } 
        }
    }

    void Awake()
    {   
        // Подписываемся на события
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {   
        // При разрушении объекта отписываемся
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Метод для установки состояние врага
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    // Метод, объявленный в подписчике для события SPEED_CHANGED
    private void OnSpeedChanged(float value)
    {   
        // Изменяем скорость
        speed = baseSpeed * value;
    }
}
