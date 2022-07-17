using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]
[RequireComponent(typeof(EnemyUIController))]

public class Enemy1 : MonoBehaviour, IEnemy
{
    #region Serialize fields
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;
    [SerializeField] private float _speed = 5;
    #endregion Serialize fields

    #region Properties
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if (value <= 0)
            {
                _maxHealth = 1;
                return;
            }
            _maxHealth = value;
        }
    }
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value < 0)
            {
                _health = 0;
                return;
            }
            if (value > _maxHealth)
            {
                _health = _maxHealth;
                return;
            }
            _health = value;
        }
    }
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (value < 0)
            {
                _speed = 0;
                return;
            }
            _speed = value;
        }
    }
    #endregion Properties

    #region Private fields
    private HitBoxesController _hitBoxesController;
    private EnemyUIController _enemyUIController;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        MaxHealth = _maxHealth;
        Health = MaxHealth;
        Speed = _speed;
    }

    private void Start()
    {
        _hitBoxesController = GetComponent<HitBoxesController>();
        _enemyUIController = GetComponent<EnemyUIController>();
    }
    #endregion Mono

    #region Private methods
    private void TakeDamage(int damage, TypeDamage typeDamage)
    {
        Health -= damage;

        StartCoroutine(_enemyUIController.ShowDamage(damage, typeDamage));

        if (Health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        RagdollController ragdollControl = GetComponent<RagdollController>();
        if (ragdollControl)
            ragdollControl.MakePhysical();

        yield return new WaitForSeconds(3.5f);

        Destroy(this.gameObject);
    }

    #endregion Private methods

    #region Public methods
    public void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage)
    {
        // Получаем значение урона с учетом попадания в ту или иную часть тела
        damage = _hitBoxesController.GetDamageValue(damage, hitCollider);
        TakeDamage(damage, typeDamage);
    }
    #endregion Public methods
}
