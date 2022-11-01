using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private Transform _target;
    [SerializeField] private string _nameTargetObjectOnScene = "Player";
    #endregion Serialize fields

    #region Private fields
    private GameObject _mainCam;
    #endregion Private fields

    #region Mono
    private void Start()
    {
        if (!_target)
        {
            _target = UnityUtility.FindGameObjectTransformWithTag(_nameTargetObjectOnScene);
        }
    }
    #endregion Mono

    #region Private methods
    private void LateUpdate()
    {
        if(!_target)
            _target = UnityUtility.FindGameObjectTransformWithTag(_nameTargetObjectOnScene);

        transform.LookAt(transform.position - _target.forward);
    }
    #endregion Private methods
}
