using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    #region Serialize fields
    [SerializeField]  private Transform _target;
    #endregion Serialize fields

    #region Private fields
    private GameObject _mainCam;
    #endregion Private fields

    #region Mono
    private void Start()
    {
        if (!_target)
        {
            _mainCam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            if (_mainCam)
                _target = _mainCam.transform;
        }
    }
    #endregion Mono

    #region Private methods
    private void LateUpdate()
    {
        if (_target)
            transform.LookAt(transform.position - _target.forward);
    }
    #endregion Private methods
}
