using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_1 : MonoBehaviour
{
    // private Transform _pointer;
    public Camera camera;

    private bool _isAiming = false;

    private Vector3 rotationVector;
    private Vector3 currentAngle;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        rotationVector = new Vector3(0.0f, 0.0f, 90.0f);
        Quaternion targetRotation = Quaternion.Euler(rotationVector);
        // currentAngle = transform.eulerAngles;
    }

    
    private void FixedUpdate()
    {
        // Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // _pointer.position = hit.point;
            // Debug.Log(hit.point);
            transform.LookAt(hit.point);

            if (!_isAiming)
            {
                transform.Rotate(0.0f, 0.0f, -90.0f);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 0.1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        // ПКМ
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"Прицеливание");
            _isAiming = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log($"Отмена Прицеливание");
            _isAiming = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_isAiming)
            {
                Debug.Log($"Заряженный выстрел");
            }
            else
            {
                Debug.Log($"Выстрел");
            }
            
        }

        // transform.LookAt(_pointer.position);
    }
}
