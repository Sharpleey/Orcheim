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
    [SerializeField] private GameObject bow;
    [SerializeField] private float shotForce; // Сила выстрела
    [SerializeField] private float timeReload; // Время

    [SerializeField] private Transform arrowSpawn;

    private Camera camera;
    private GameObject arrow;

    private bool _arrowInBowstring = false;
    private bool _reload = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();

        // Cursor.lockState = CursorLockMode.Locked; 
        // Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!_arrowInBowstring && !_reload)
        {
            arrow = Instantiate(arrowPrefab) as GameObject;
            arrow.transform.parent = bow.transform;

            arrow.transform.position = arrowSpawn.position;
            arrow.transform.rotation = arrowSpawn.rotation;

            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            arrowRb.isKinematic = true;

            _arrowInBowstring = true;
        }

        // Ray rayShot = new Ray (arrow.transform.position, camera.transform.forward * 100);
        // Debug.DrawRay(bow.transform.position, camera.transform.forward * 100, Color.yellow);


        // Нажатие на ЛКМ
        if (Input.GetMouseButtonDown(0) && _arrowInBowstring && !_reload) // Проверяем, что GUI не используется.
        {   
            arrow.transform.parent = null;

            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            
            if (arrowRb != null)
            {
                arrowRb.isKinematic = false;
                // Назначаем физическому телу скорость.
                arrowRb.AddForce((arrow.transform.forward) * shotForce, ForceMode.Impulse);
            }
            
            _arrowInBowstring = false;

            StartCoroutine(Reload());


            // // Создаем стрелу
            // GameObject arrow = Instantiate(arrowPrefab) as GameObject;
            // // Указываем для нее коррдинаты и поворот
            // arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            // arrow.transform.rotation = transform.rotation;

            // // Transform arrowHead = arrow.transform.Find("arrowGeoGrp/arrowHead");


            // Rigidbody body = arrow.GetComponent<Rigidbody>();

            // if (body != null)
            // {
            //     // Назначаем физическому телу скорость.
            //     // body.velocity = transform.forward * shotForce;
            //     body.AddRelativeForce((transform.forward) * shotForce, ForceMode.Impulse);
            // }
            // StartCoroutine(Shot());


            // Получаем координаты середины экрана
            // Vector3 point = new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 0);
            // Создаем луч
            // Ray ray = camera.ScreenPointToRay(point);
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

    private IEnumerator Reload()
    {   
        _reload = true;

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(timeReload);

        _reload = false;
    }

    private IEnumerator Shot()
    {   
        // Создаем стрелу
        GameObject arrow = Instantiate(arrowPrefab) as GameObject;
        // Указываем для нее коррдинаты и поворот
        arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        arrow.transform.rotation = transform.rotation;

        Rigidbody body = arrow.GetComponent<Collider>().attachedRigidbody;

        if (body != null)
        {
            // Назначаем физическому телу скорость.
            body.velocity = transform.forward * shotForce;
        }

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(2);

        // Удаляем объект со сцены и очищаем память
        Destroy(arrow);
    }

    void OnGUI() 
    { 
        int size = 12; 
        float posX = camera.pixelWidth/2 - size/4; 
        float posY = camera.pixelHeight/2 - size/2; 
        GUI.Label(new Rect(posX, posY, size, size), "*");
    } 
}
