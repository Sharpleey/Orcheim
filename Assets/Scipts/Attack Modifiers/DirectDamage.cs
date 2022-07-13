using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : MonoBehaviour ,IModifier
{
    #region Serialize Fields
    [SerializeField] string _name = "Урон";
    [SerializeField] string _description = "Наносит противнику урон при попадании";

    [SerializeField] int _damage = 25;
    #endregion Serialize Fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; private set => _description = value; }
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            if (value < 0)
            {
                _damage = 0;
                return;
            }
            _damage = value;
        }
    }
    #endregion Properties

    #region Methods
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Damage = _damage;
    }
    private void OnTriggerEnter(Collider hitCollider)
    {
        IEnemy enemy = hitCollider.GetComponentInParent<Enemy1>();
        if (enemy != null)
        {
            enemy.TakeHitboxDamage(Damage, hitCollider);
        }
    }
    #endregion Methods
}
