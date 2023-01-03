using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� �������� �� ���������� Ragdoll ���������
/// </summary>
public class RagdollController : MonoBehaviour
{
    #region Serialize fields
    /// <summary>
    /// ������ ���� Rigibody �� ���������
    /// </summary>
    [SerializeField] private List<Rigidbody> _allRigibodys;
    #endregion Serialize fields

    #region Private fields
    /// <summary>
    /// �������� ���������
    /// </summary>
    private Animator _animator;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.isKinematic = true;
        }
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    #endregion Mono

    #region Public methods
    /// <summary>
    /// ����� ������ Ragdoll ��������� ��������, �������� ��� ���� ��������
    /// </summary>
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
