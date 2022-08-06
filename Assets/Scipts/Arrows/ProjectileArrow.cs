using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectDamage))]
[RequireComponent(typeof(Rigidbody))]
public class ProjectileArrow : MonoBehaviour
{
	#region Serialize fields
	[SerializeField] private GameObject _tracerEffect;
	[SerializeField] private GameObject _hitEffect;
	#endregion Serialize fields

	#region Properties

	#endregion Properties

	#region Private fields
	private bool _isArrowInFlight;
	private bool _isBlockDamage;

	private bool _onPenetrationMod;
	private bool _onCriticalDamageMod;

	private Penetration _penetrationMod;
	private DirectDamage _directDamageMod;
	private CriticalDamage _criticalDamageMod;

	private Rigidbody _arrowRigidbody;
	private CapsuleCollider _arrowCapsuleCollider;

	private IEnemy _currentHitEnemy;

	#endregion Private fields

	#region Public fields
	[HideInInspector] 
	public bool isArrowInBowstring;

	public List<IModifier> bowAttackModifiers;
	#endregion Public fields

	#region Mono
    private void Start() 
	{
		isArrowInBowstring = true;

		_isArrowInFlight = false;
		_isBlockDamage = false;

		_onPenetrationMod = UnityUtility.HasComponent<Penetration>(gameObject);
		_onCriticalDamageMod = UnityUtility.HasComponent<CriticalDamage>(gameObject);
		
		_directDamageMod = GetComponent<DirectDamage>();

		if (_onPenetrationMod)
			_penetrationMod = GetComponent<Penetration>();
		if (_onCriticalDamageMod)
			_criticalDamageMod = GetComponent<CriticalDamage>();

		_arrowRigidbody = GetComponent<Rigidbody>();
		_arrowCapsuleCollider = GetComponent<CapsuleCollider>();

		if (_arrowCapsuleCollider != null)
			_arrowCapsuleCollider.isTrigger = false;
	}
    #endregion Mono

    #region Private methods
    private void FixedUpdate() 
	{
		// Отлавливаем момент вылета стрелы
		if (!isArrowInBowstring && !_isArrowInFlight)
		{
			_isArrowInFlight = true;

			if (_arrowCapsuleCollider != null)
				_arrowCapsuleCollider.isTrigger = true;

			if (_tracerEffect != null)
				_tracerEffect.SetActive(true);

			// Запускаем отсчет для удаления стрелы
			StartCoroutine(DeleteProjectile(5));
		}

		// Поворачиваем стрелу в полете в сторону движения
		if (_isArrowInFlight)
        {
			transform.LookAt(transform.position + _arrowRigidbody.velocity);
		}
	}

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
				if (_onCriticalDamageMod && _criticalDamageMod.GetProcCrit())
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
				}

				if (_onPenetrationMod)
                {
					_penetrationMod.CurrentPenetration++;

					// Уменьшаем урон с каждым пробитием
					_directDamageMod.Damage = (int)(_directDamageMod.Damage * (1 - _penetrationMod.DamageDecrease));

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

    private IEnumerator DeleteProjectile(int secondsBeforeDeletion)
    {
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
}