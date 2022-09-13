using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class ProjectileArrow : MonoBehaviour
{
	#region Serialize fields
	[SerializeField] private GameObject _tracerEffect;
	[SerializeField] private GameObject _hitEffect;
	#endregion Serialize fields

	#region Private fields
	private bool _isArrowInFlight;
	private bool _isBlockDamage;

	private DirectDamage _directDamageMod;
	private CriticalDamage _criticalDamageMod;
	private Penetration _penetrationMod;
	private FireArrow _fireArrowMod;
	private SlowArrow _slowArrowMod;
	private Mjolnir _mjolnirMod;

	private Rigidbody _arrowRigidbody;
	private CapsuleCollider _arrowCapsuleCollider;

	private IEnemy _currentHitEnemy;

	private Dictionary<Type, IModifier> _bowAttackModifaers = new Dictionary<Type, IModifier>();
	#endregion Private fields

	#region Mono
    private void Start() 
	{
		_isArrowInFlight = false;
		_isBlockDamage = false;

		// Получаем модификаторы атаки, установленные на луке
		_bowAttackModifaers = GetComponentInParent<LightBow>().AttackModifaers;

		_directDamageMod = (DirectDamage)_bowAttackModifaers[typeof(DirectDamage)];
		_criticalDamageMod = (CriticalDamage)_bowAttackModifaers[typeof(CriticalDamage)];
		_penetrationMod = (Penetration)_bowAttackModifaers[typeof(Penetration)];
		_fireArrowMod = (FireArrow)_bowAttackModifaers[typeof(FireArrow)];
		_slowArrowMod = (SlowArrow)_bowAttackModifaers[typeof(SlowArrow)];
		_mjolnirMod = (Mjolnir)_bowAttackModifaers[typeof(Mjolnir)];

		_arrowRigidbody = GetComponent<Rigidbody>();
		_arrowCapsuleCollider = GetComponent<CapsuleCollider>();

		_arrowCapsuleCollider.isTrigger = false;
	}
    #endregion Mono

    #region Private methods
    private void FixedUpdate() 
	{
		// Поворачиваем стрелу в полете в сторону движения
		if (_isArrowInFlight)
        {
			transform.LookAt(transform.position + _arrowRigidbody.velocity);
		}
	}
	/// <summary>
	/// Метод срабатывает при касании стрелы с другими объектами на которых есть коллайдер
	/// </summary>
	/// <param name="hitCollider">Коллайдер, с которым соприкоснулась стрела</param>
	private void OnTriggerEnter(Collider hitCollider)
    {
		IEnemy enemy = hitCollider.GetComponentInParent<IEnemy>();
		// Если мы попали в противника
		if (enemy != null)
		{
			if (enemy != _currentHitEnemy)
			{
				int damage = _directDamageMod.ActualDamage;
				// Если (влючен мод на криты) и (Прокнул крит)
				if (_criticalDamageMod != null && _criticalDamageMod.GetProcCrit())
                {
                    // Рассчитываем критический урон
                    damage = (int)(damage * _criticalDamageMod.CritMultiplierDamage);
                }

				if (!_isBlockDamage)
                {
					// Эффект попадания
					GameObject hitObj = Instantiate(_hitEffect, transform.position, transform.rotation);

					// Наносим урон противнику
					enemy.TakeHitboxDamage(damage, hitCollider, _directDamageMod.TypeDamage);

					// Поджигаем противника
					if (_fireArrowMod != null && _fireArrowMod.GetProcBurning())
                    {
						enemy.SetBurning(_fireArrowMod.DamagePerSecond, _fireArrowMod.Duration, _fireArrowMod.TypeDamage);
                    }						
				}

				if (_penetrationMod != null)
                {
					_penetrationMod.CurrentPenetration++;

					// Уменьшаем урон с каждым пробитием
					_directDamageMod.CurrentAverageDamage = (int)(_directDamageMod.CurrentAverageDamage * (1 - _penetrationMod.DamageDecrease));

					// Если число пробитий подошло к пределу, то удаляем стрелу
					if (_penetrationMod.CurrentPenetration == _penetrationMod.MaxTargetPenetration)
                    {
						_isBlockDamage = true;
						StartCoroutine(DeleteProjectile(0));
					}
				}
				else
                {
					_isBlockDamage = true;
					StartCoroutine(DeleteProjectile(0));
				}
			}

			if (enemy != _currentHitEnemy)
			{
				_currentHitEnemy = enemy;
			}
		}
		else
		{
			StartCoroutine(DeleteProjectile(0));
		}
	}
	/// <summary>
	/// Метод удаляет объект стрелы с определенной задержкой по времени
	/// </summary>
	/// <param name="secondsBeforeDeletion"></param>
	/// <returns>Задержка (в секундах) до удаления объекта стрелы</returns>
	private IEnumerator DeleteProjectile(int secondsBeforeDeletion)
    {
		if (_penetrationMod != null)
        {
			_penetrationMod.CurrentPenetration = 0;
			_directDamageMod.CurrentAverageDamage = _directDamageMod.AverageDamage;
		}			

		// Ключевое слово yield указывает сопрограмме, когда следует остановиться.
		yield return new WaitForSeconds(secondsBeforeDeletion);

		if (_tracerEffect != null)
        {
            _tracerEffect.transform.SetParent(null);
			TracerEffectController traccerController = _tracerEffect.GetComponent<TracerEffectController>();
			if (traccerController != null)
				traccerController.DeleteTracer();
		}

        // Удаляем объект со сцены и очищаем память
        Destroy(gameObject);
    }
	#endregion Private methods

	#region Public methods
	/// <summary>
	/// Запуск стрелы, с применение определенного значения силы
	/// </summary>
	/// <param name="forceLaunch">Сила импульса придаваемого стреле</param>
	public void Launch (float forceLaunch)
    {
		transform.parent = null;

		_arrowRigidbody.isKinematic = false;
		_arrowRigidbody.AddForce((transform.forward) * forceLaunch, ForceMode.Impulse);

        _isArrowInFlight = true;

        _arrowCapsuleCollider.isTrigger = true;

        if (_tracerEffect != null)
            _tracerEffect.SetActive(true);

        // Запускаем отсчет для удаления стрелы
        StartCoroutine(DeleteProjectile(5));

    }
	#endregion Public methods
}