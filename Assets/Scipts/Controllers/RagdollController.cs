using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Rigidbody> _allRigibodys;
    #endregion Serialize fields

    #region Mono
    private void Awake()
    {
        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.isKinematic = true;
        }
    }
    #endregion Mono

    #region Public methods
    public void MakePhysical()
    {
        _animator.enabled = false;

        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.isKinematic = false;
        }
    }
    #endregion Public methods
}
