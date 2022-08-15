using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер эффекта горения. Отвечает за визуализацию эффекта горения и за нанесения урона в секунду
/// </summary>
public class BurningEffectController : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// Урона наносимый каждую секунду
    /// </summary>
    public int DamagePerSecond
    {
        get
        {
            return _damagePerSecond;
        }
        set
        {
            if (value <= 0)
            {
                _damagePerSecond = 0;
                return;
            }
            _damagePerSecond = value;
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
    public int ActualDamage
    {
        get
        {
            int range = (int)(DamagePerSecond * DamageSpread);
            return Random.Range(DamagePerSecond - range, DamagePerSecond + range);
        }
    }
    public TypeDamage TypeDamage { get; set; }
    #endregion Properties

    #region Private fields
    private ParticleSystem _particleSystem;

    private IEnemy _enemy;

    private float _timer = 0;

    private int _damagePerSecond = 6;
    private float _damageSpread = 0.25f;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        // Получаем компонент врага, что вызывать метод получения урона каждую секунду
        _enemy = GetComponentInParent<IEnemy>();

        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }
    private void OnEnable()
    {
        _timer = 0;

        //var main = _particleSystem.main;
        //main.duration = _durationBurningEffect;

        _particleSystem.Play();
    }
    private void OnDisable()
    {
        _particleSystem.Stop();
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (_timer < 1f)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            // Наносим урон каждую секунду
            _enemy.TakeDamage(ActualDamage, TypeDamage);
            _timer = 0f;
        }
    }
    #endregion Private methods
}
