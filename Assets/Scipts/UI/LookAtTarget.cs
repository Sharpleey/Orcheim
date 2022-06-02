using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private GameObject _mainCam;
    // Start is called before the first frame update
    void Start()
    {
        if (!_target)
        {
            _mainCam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            if (_mainCam)
                _target = _mainCam.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_target)
            transform.LookAt(transform.position - _target.forward);
    }
}
