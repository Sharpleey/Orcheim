using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class RayShooter: MonoBehaviour
{   
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioClip _hitWallSound;
    [SerializeField] private AudioClip _hitEnemySound;

    [SerializeField] private GameObject _arrowPrefab; // Префаб для стрелы
    [SerializeField] private GameObject _bow;
    [SerializeField] private float _shotForce = 8; // Сила выстрела
    [SerializeField] private float _timeReload = 0.5f; // Время

    [SerializeField] private Transform _arrowSpawn;

    private Camera _camera;
    private GameObject _arrow;

    private bool _isArrowInBowstring = false;
    private bool _isReload = false;

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
        if (!_isArrowInBowstring && !_isReload)
        {
            _arrow = Instantiate(_arrowPrefab) as GameObject;

            _arrow.transform.parent = _bow.transform;

            _arrow.transform.position = _arrowSpawn.position;
            _arrow.transform.rotation = _arrowSpawn.rotation;

            Rigidbody arrowRb = _arrow.GetComponent<Rigidbody>();
            arrowRb.isKinematic = true;

            _isArrowInBowstring = true;
        }

        // Ray rayShot = new Ray (_arrow.transform.position, _camera.transform.forward * 100);
        // Debug.DrawRay(_bow.transform.position, _camera.transform.forward * 100, Color.yellow);


        // Нажатие на ЛКМ
        if (Input.GetMouseButtonDown(0) && _isArrowInBowstring && !_isReload) // Проверяем, что GUI не используется.
        {   
            _arrow.transform.parent = null;

            Rigidbody arrowRb = _arrow.GetComponent<Rigidbody>();
            
            if (arrowRb != null)
            {
                arrowRb.isKinematic = false;
                // Назначаем физическому телу скорость.
                arrowRb.AddForce((_arrow.transform.forward) * _shotForce, ForceMode.Impulse);
            }
            
            _isArrowInBowstring = false;

            Arrow arrow = _arrow.GetComponent<Arrow>();
            if (arrow)
            {
                arrow.isArrowInBowstring = false;
            }

            StartCoroutine(Reload());


            // // Создаем стрелу
            // GameObject _arrow = Instantiate(_arrowPrefab) as GameObject;
            // // Указываем для нее коррдинаты и поворот
            // _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            // _arrow.transform.rotation = transform.rotation;

            // // Transform arrowHead = _arrow.transform.Find("arrowGeoGrp/arrowHead");


            // Rigidbody body = _arrow.GetComponent<Rigidbody>();

            // if (body != null)
            // {
            //     // Назначаем физическому телу скорость.
            //     // body.velocity = transform.forward * _shotForce;
            //     body.AddRelativeForce((transform.forward) * _shotForce, ForceMode.Impulse);
            // }
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
            //     //     _soundSource.PlayOneShot(_hitEnemySound);

            //     //     // Рассылка сообщений на реакцию попадания
            //     //     Messenger.Broadcast(GameEvent.ENEMY_HIT);
            //     // } 
            //     // else 
            //     // { 
            //     //     // Запускаем сопрограмму
            //     //     StartCoroutine(SphereIndicator(hit.point));
            //     //     _soundSource.PlayOneShot(_hitWallSound); 
            //     // } 
            // }
        }
    }

    private IEnumerator Reload()
    {   
        _isReload = true;

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(_timeReload);

        _isReload = false;
    }

    private IEnumerator Shot()
    {   
        // Создаем стрелу
        GameObject _arrow = Instantiate(_arrowPrefab) as GameObject;
        // Указываем для нее коррдинаты и поворот
        _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _arrow.transform.rotation = transform.rotation;

        Rigidbody body = _arrow.GetComponent<Collider>().attachedRigidbody;

        if (body != null)
        {
            // Назначаем физическому телу скорость.
            body.velocity = transform.forward * _shotForce;
        }

        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(2);

        // Удаляем объект со сцены и очищаем память
        Destroy(_arrow);
    }

    // void OnGUI() 
    // { 
    //     int size = 12; 
    //     float posX = _camera.pixelWidth/2 - size/4; 
    //     float posY = _camera.pixelHeight/2 - size/2; 
    //     GUI.Label(new Rect(posX, posY, size, size), "*");
    // } 
}
