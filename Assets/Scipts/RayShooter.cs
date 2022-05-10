using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class RayShooter: MonoBehaviour
{   
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip hitWallSound;
    [SerializeField] private AudioClip hitEnemySound;

    [SerializeField] private GameObject arrowPrefab; // Префаб для стрелы
    [SerializeField] private float shotForce; // Сила выстрела

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        // Cursor.lockState = CursorLockMode.Locked; 
        // Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        // Нажатие на ЛКМ
        if (Input.GetMouseButtonDown(0)) // Проверяем, что GUI не используется.
        {   
            // Создаем стрелу
            GameObject _arrow = Instantiate(arrowPrefab) as GameObject;
            // Указываем для нее коррдинаты и поворот
            _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            _arrow.transform.rotation = transform.rotation;

            Rigidbody body = _arrow.GetComponent<Collider>().attachedRigidbody;

            if (body != null)
            {
                // Назначаем физическому телу скорость.
                body.velocity = transform.forward * shotForce;
            }
            // StartCoroutine(Shot());


            // Получаем координаты середины экрана
            // Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
            // Создаем луч
            // Ray ray = _camera.ScreenPointToRay(point);
            // Содержит информацию о пересечении объекта
            // RaycastHit hit;
            // if (Physics.Raycast(ray, out hit))
            // {   
            //     GameObject hitObject = hit.transform.gameObject;

            //     ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            //     // if (target != null) 
            //     // { 
            //     //     target.ReactToHit();
            //     //     soundSource.PlayOneShot(hitEnemySound);

            //     //     // Рассылка сообщений на реакцию попадания
            //     //     Messenger.Broadcast(GameEvent.ENEMY_HIT);
            //     // } 
            //     // else 
            //     // { 
            //     //     // Запускаем сопрограмму
            //     //     StartCoroutine(SphereIndicator(hit.point));
            //     //     soundSource.PlayOneShot(hitWallSound); 
            //     // } 
            // }
        }
    }

    private IEnumerator Shot()
    {   
        // Создаем стрелу
        GameObject _arrow = Instantiate(arrowPrefab) as GameObject;
        // Указываем для нее коррдинаты и поворот
        _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _arrow.transform.rotation = transform.rotation;

        Rigidbody body = _arrow.GetComponent<Collider>().attachedRigidbody;

        if (body != null)
        {
            // Назначаем физическому телу скорость.
            body.velocity = transform.forward * shotForce;
        }

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(2);

        // Удаляем объект со сцены и очищаем память
        Destroy(_arrow);
    }

    void OnGUI() 
    { 
        int size = 12; 
        float posX = _camera.pixelWidth/2 - size/4; 
        float posY = _camera.pixelHeight/2 - size/2; 
        GUI.Label(new Rect(posX, posY, size, size), "*");
    } 
}
