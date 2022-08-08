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
    private RagdollController _ragdollController;
    private DieEffectController _dieEffectController;
    private HealthBarController _healthBarController;
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
        _ragdollController = GetComponent<RagdollController>();
        _dieEffectController = GetComponent<DieEffectController>();
        _healthBarController = GetComponentInChildren<HealthBarController>();

        if (_dieEffectController != null)
            _dieEffectController.enabled = false;

        if (_healthBarController != null)
        {
            _healthBarController.SetMaxHealth(MaxHealth);
            _healthBarController.SetHealth(Health);
        }
    }
    #endregion Mono

    #region Private methods
    private void TakeDamage(int damage, TypeDamage typeDamage)
    {
        if (Health > 0)
        {
            Health -= damage;

            // Всплывающий дамаг
            _enemyUIController.ShowPopupDamage(damage, typeDamage); //TODO

            // Полоска хп
            if (_healthBarController != null)
            {
                _healthBarController.SetHealth(Health);
                _healthBarController.ShowHealthBar();
            }
        }

        if (Health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        if (_ragdollController != null)
            _ragdollController.MakePhysical();

        yield return new WaitForSeconds(2);

        //if (_hitBoxesController)
        //    _hitBoxesController.OnLayersAllColliders();

        if (_dieEffectController != null)
            _dieEffectController.enabled = true;

        yield return new WaitForSeconds(5);

        Destroy(gameObject);
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
