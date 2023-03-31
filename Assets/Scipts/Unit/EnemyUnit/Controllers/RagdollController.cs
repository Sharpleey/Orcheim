using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс отвечает за управление Ragdoll персонажа
/// </summary>
public class RagdollController : MonoBehaviour
{
    #region Serialize fields

    [Space]
    [SerializeField] RigidbodyInterpolation _interpolation;
    [SerializeField] CollisionDetectionMode _collisionDetectionMode;

    [Header("Список всех Rigibody на персонаже")]
    [SerializeField] private List<Rigidbody> _allRigibodys;

    #endregion Serialize fields

    #region Private fields
    /// <summary>
    /// Аниматор персонажа
    /// </summary>
    private Animator _animator;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        SetIsKinematicAllRigibodys(true);
        SetUseGravityAllRigibodys(false);
        SetInterpolateAllRigibodys(_interpolation);
        SetCollisionDetectionModeAllRigibodys(_collisionDetectionMode);
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    #endregion Mono

    #region Private methods

    private void SetInterpolateAllRigibodys(RigidbodyInterpolation rigidbodyInterpolation)
    {
        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.interpolation = rigidbodyInterpolation;
        }
    }

    private void SetCollisionDetectionModeAllRigibodys(CollisionDetectionMode collisionDetectionMode)
    {
        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.collisionDetectionMode = collisionDetectionMode;
        }
    }

    private void SetIsKinematicAllRigibodys(bool isKinematic)
    {
        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.isKinematic = isKinematic;
        }
    }

    private void SetUseGravityAllRigibodys(bool isUseGravity)
    {
        foreach (Rigidbody rigidbody in _allRigibodys)
        {
            rigidbody.useGravity = isUseGravity;
        }
    }

    #endregion Private methods

    #region Public methods

    /// <summary>
    /// Метод делает Ragdoll персонажа физичным, отключая при этом анимации
    /// </summary>
    public void MakePhysical()
    {
        _animator.enabled = false;

        SetUseGravityAllRigibodys(true);
        SetIsKinematicAllRigibodys(false);
    }

    #endregion Public methods
}
