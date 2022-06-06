using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Rigidbody> _allRigibodys;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void MakePhysical()
    {
        _animator.enabled = false;

        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.isKinematic = false;
        }
    }
}
