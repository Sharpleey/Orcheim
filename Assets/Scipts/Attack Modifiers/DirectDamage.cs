using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : MonoBehaviour ,IModifier
{
    #region Serialize Fields
    [SerializeField] private string _name = "”рон";
    [SerializeField] private string _description = "Ќаносит противнику урон при попадании";

    [SerializeField] [Range(1, 300)] private int _damage = 25;
    [SerializeField] private TypeDamage _typeDamage = TypeDamage.Physical;
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
    public TypeDamage TypeDamage { get => _typeDamage; private set => _typeDamage = value; }
    #endregion Properties

    #region Fields

    /// <summary>
    /// C ее помощью фиксим баг, когда стрела успевает попасть по нескольким коллайдерам до момента удалени€
    /// </summary>
    private bool _isHitEnemy = false; 

    #endregion Fields

    #region Methods
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Damage = _damage;
        TypeDamage = _typeDamage;
    }
    private void OnTriggerEnter(Collider hitCollider)
    {
        if(!_isHitEnemy)
        {
            IEnemy enemy = hitCollider.GetComponentInParent<Enemy1>();
            if (enemy != null)
            {
                _isHitEnemy = true;
                enemy.TakeHitboxDamage(Damage, hitCollider, TypeDamage);
            }
        }
    }
    #endregion Methods
}
