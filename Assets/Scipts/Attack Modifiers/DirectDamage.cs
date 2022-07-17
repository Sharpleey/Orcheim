using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : MonoBehaviour ,IModifier
{
    #region Serialize fields
    [SerializeField] private string _name = "”рон";
    [SerializeField] private string _description = "Ќаносит противнику урон при попадании";

    [SerializeField] [Range(1, 300)] private int _damage = 25;
    [SerializeField] [Range(0f, 0.5f)] private float _damageSpread = 0.25f;
    [SerializeField] private TypeDamage _typeDamage = TypeDamage.Physical;
    #endregion Serialize fields

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
    public float DamageSpread
    {
        get
        {
            return _damageSpread;
        }
        private set
        {
            if (value < 0f)
            {
                _damageSpread = 0f;
                return;
            }
            if (value > 0.5f)
            {
                _damageSpread = 0.5f;
                return;
            }
            _damageSpread = value;
        }
    }
    public TypeDamage TypeDamage { get => _typeDamage; private set => _typeDamage = value; }
    /// <summary>
    /// ”рон с учетом разброса
    /// </summary>
    public int ActualDamage
    {
        get
        {
            int range = (int)(Damage * DamageSpread);
            return Random.Range(Damage - range, Damage + range);
        }
    }
    #endregion Properties

    #region Private fields
    /// <summary>
    /// C ее помощью фиксим баг, когда стрела успевает попасть по нескольким коллайдерам до момента удалени€
    /// </summary>
    private IEnemy _currentHitEnemy;
    private bool _onPenetrationMod;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Damage = _damage;
        DamageSpread = _damageSpread;
        TypeDamage = _typeDamage;
    }
    private void Start()
    {
        _onPenetrationMod = UnityUtility.HasComponent<Penetration>(gameObject);
    }
    #endregion Mono

    #region Private methods
    private void OnTriggerEnter(Collider hitCollider)
    {
        IEnemy enemy = hitCollider.GetComponentInParent<IEnemy>();
        if (enemy != null)
        {
            if (_currentHitEnemy == null || (enemy != _currentHitEnemy && _onPenetrationMod))
            {
                _currentHitEnemy = enemy;
                enemy.TakeHitboxDamage(ActualDamage, hitCollider, TypeDamage);
            }
        }
    }
    #endregion Private methods
}
