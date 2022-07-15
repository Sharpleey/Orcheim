using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]
[RequireComponent(typeof(EnemyUIController))]

public class Enemy1 : MonoBehaviour, IEnemy
{
    #region Serialize Fields

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;
    [SerializeField] private float _speed = 5;

    #endregion Serialize Fields

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

    #region Fields
    private HitBoxesController _hitBoxesController;
    private EnemyUIController _enemyUIController;

    #endregion Fields

    #region Methods
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

    private IEnumerator Die()
    {
        RagdollController ragdollControl = GetComponent<RagdollController>();
        if (ragdollControl)
            ragdollControl.MakePhysical();

        yield return new WaitForSeconds(3.5f);

        Destroy(this.gameObject);
    }

    public void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage)
    {
        // Получаем значение урона с учетом попадания в ту или иную часть тела
        damage = _hitBoxesController.GetDamageValue(damage, hitCollider);
        TakeDamage(damage, typeDamage);
    }

    private void TakeDamage(int damage, TypeDamage typeDamage)
    {
        Health -= damage;

        StartCoroutine(_enemyUIController.ShowDamage(damage, typeDamage));

        if (Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    //private void ShowPopupDamage(int damage)
    //{
    //    PopupDamage popupDamage = GetComponent<PopupDamage>();
    //    popupDamage.SetText(damage.ToString());
    //    popupDamage.ShowAndHide();
    //}

    #endregion Methods
}
