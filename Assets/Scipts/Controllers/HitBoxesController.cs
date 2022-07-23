using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxesController : MonoBehaviour
{
    #region Serialize fields
    [Header("Hit Box Colliders")]
    [SerializeField] private Collider _headCollider;
    [SerializeField] private List<Collider> _handColliders;
    [SerializeField] private List<Collider> _legColliders;
    [SerializeField] private List<Collider> _bodyColliders;

    [Header("Hit Boxe Multiply Damage")]
    [SerializeField] private float _headDamageMultiplier = 3.0f;
    [SerializeField] private float _handDamageMultiplier = 0.5f;
    [SerializeField] private float _legDamageMultiplier = 0.5f;
    [SerializeField] private float _bodyDamageMultiplier = 1.0f;
    #endregion Serialize fields

    #region Public methods
    public int GetDamageValue(int damage, Collider hitCollider)
    {
        // TODO Возможно стоит оптимизировать/отрефакторить
        if (hitCollider == this._headCollider)
        {
            float actualDamage = damage * _headDamageMultiplier;
            return (int)actualDamage;
        }

        foreach (Collider handCollider in _handColliders)
        {
            if (hitCollider == handCollider)
            {
                float actualDamage = damage * _handDamageMultiplier;
                return (int)actualDamage;
            }
        } 
        
        foreach (Collider legCollider in _legColliders)
        {
            if (hitCollider == legCollider)
            {
                float actualDamage = damage * _legDamageMultiplier;
                return (int)actualDamage;
            }
        }

        foreach (Collider bodyCollider in _bodyColliders)
        {
            if (hitCollider == bodyCollider)
            {
                float actualDamage = damage * _bodyDamageMultiplier;
                return (int)actualDamage;
            }
        }

        return 1;
    }

    public void OnLayersAllColliders()
    {
        Physics.IgnoreLayerCollision(9, 8);
    }

    #endregion Public methods
}
