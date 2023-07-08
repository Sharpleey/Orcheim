using UnityEngine;

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

    private void Start()
    {
        _target = GetComponentInParent<EnemyUnit>().TargetUnit.transform;
    }

    #endregion Mono
}
