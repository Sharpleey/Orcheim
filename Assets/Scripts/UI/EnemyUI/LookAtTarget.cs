using UnityEngine;
using Zenject;

public class LookAtTarget : MonoBehaviour
{
    #region Serialize fields

    private Transform _target;

    #endregion Serialize fields

    #region Mono

    private void LateUpdate()
    {
        if(_target)
            transform.LookAt(transform.position - _target.forward);
    }

    #endregion Mono

    #region Private methods

    [Inject]
    private void Construct(PlayerUnit unitTarget)
    {
        _target = unitTarget.transform;
    }
   
    #endregion Private methods
}
