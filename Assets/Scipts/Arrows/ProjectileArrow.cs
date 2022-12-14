using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class ProjectileArrow : MonoBehaviour
{
	#region Serialize fields
	[SerializeField] private GameObject _tracerEffect;
	[SerializeField] private GameObject _hitEffect;
	#endregion Serialize fields

	#region Private fields
	private bool _isArrowInFlight;
	private bool _isBlockDamage;

    #region AttackModifaers
    //private Penetration _penetrationMod;
    private FlameAttack _flameAttack;
	private SlowAttack _slowAttack;
	private CriticalAttack _criticalAttack;
	private PenetrationProjectile _penetrationProjectile;
	#endregion

	private LightBow _lightBow;
	private BowAudioController _bowAudioController;
	private Rigidbody _arrowRigidbody;
	private BoxCollider _arrowCollider;

	private Enemy _currentHitEnemy;

	private int _damage;
	#endregion Private fields

	#region Mono
	private void Start() 
	{
		_isArrowInFlight = false;
		_isBlockDamage = false;

		_lightBow = GetComponentInParent<LightBow>();

		_damage = _lightBow.Player.Damage;

		_flameAttack = (FlameAttack)_lightBow.Player.GetAttackModifaer<FlameAttack>();
		_slowAttack = (SlowAttack)_lightBow.Player.GetAttackModifaer<SlowAttack>();
		_criticalAttack = (CriticalAttack)_lightBow.Player.GetAttackModifaer<CriticalAttack>();
		_penetrationProjectile = (PenetrationProjectile)_lightBow.Player.GetAttackModifaer<PenetrationProjectile>();

		_arrowRigidbody = GetComponent<Rigidbody>();
		_arrowCollider = GetComponent<BoxCollider>();

		_bowAudioController = _lightBow.AudioController;

		_arrowCollider.isTrigger = false;
	}
    #endregion Mono

    #region Private methods
    private void Update() 
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
		Enemy enemy = hitCollider.GetComponentInParent<Enemy>();
		// Если мы попали в противника
		if (enemy)
		{
			if (enemy != _currentHitEnemy)
			{
                if (!_isBlockDamage)
                {
					int damage = _damage;

					// Если (влючен мод на криты) и (Прокнул крит)
					if (_criticalAttack != null && _criticalAttack.IsProc())
						damage = (int)(_damage * _criticalAttack.DamageMultiplier); // Рассчитываем критический урон

					// Поджигаем противника, если прокнуло
					if (_flameAttack != null && _flameAttack.IsProc())
						enemy.SetEffect(_flameAttack.Effect);

					// Замедляем противника, если прокнуло
					if (_slowAttack != null && _slowAttack.IsProc())
						enemy.SetEffect(_slowAttack.Effect);

					// Наносим урон противнику
					enemy.TakeHitboxDamage(damage, hitCollider, TypeDamage.Physical);

					// Воспроизводим звук попадания
					_bowAudioController.PlayHit();

					// Эффект попадания
					GameObject hitObj = Instantiate(_hitEffect, transform.position, transform.rotation); //TODO Убрать Instantiate
				}

				if (_penetrationProjectile != null)
                {
					_penetrationProjectile.CurrentPenetration++;

					// Уменьшаем урон с каждым пробитием
					_damage = (int)(_damage * (1 - _penetrationProjectile.PenetrationDamageDecrease));

					// Если число пробитий подошло к пределу, то удаляем стрелу
					if (_penetrationProjectile.CurrentPenetration == _penetrationProjectile.MaxPenetrationCount)
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
				_currentHitEnemy = enemy;
		}
		else
			StartCoroutine(DeleteProjectile(0));
	}

	/// <summary>
	/// Метод удаляет объект стрелы с определенной задержкой по времени
	/// </summary>
	/// <param name="secondsBeforeDeletion"></param>
	/// <returns>Задержка (в секундах) до удаления объекта стрелы</returns>
	private IEnumerator DeleteProjectile(int secondsBeforeDeletion)
    {
		if (_penetrationProjectile != null)
        {
			// Обнуляем кол-во пробитий
			_penetrationProjectile.CurrentPenetration = 0;

			// Возвращаем исходный урон
			_damage = _lightBow.Player.Damage;
		}			

		// Ключевое слово yield указывает сопрограмме, когда следует остановиться.
		yield return new WaitForSeconds(secondsBeforeDeletion);

		// Отвязываем эффект от стрелы
        _tracerEffect?.transform.SetParent(null);
		// Удаляем объект трассера через некоторое время
		_tracerEffect?.GetComponent<TracerEffectController>()?.DeleteTracer();

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

        _arrowCollider.isTrigger = true;

        if (_tracerEffect != null)
            _tracerEffect.SetActive(true);

        // Запускаем отсчет для удаления стрелы
        StartCoroutine(DeleteProjectile(5));

    }
	#endregion Public methods
}