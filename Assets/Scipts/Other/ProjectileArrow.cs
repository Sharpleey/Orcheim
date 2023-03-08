using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class ProjectileArrow : MonoBehaviour
{
	#region Private fields

	private bool _isArrowInFlight;
	private bool _isBlockDamage;

	private LightBow _lightBow;

	private TracerEffect _tracerEffect;
	private HitEffect _hitEffect;

	private Rigidbody _rigidbody;
	private BoxCollider _collider;

	private EnemyUnit _currentHitUnit;
	private PlayerUnit _playerUnit;

	#endregion Private fields

	#region Mono

	private void Awake()
	{
		_collider = GetComponent<BoxCollider>();
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void OnEnable()
    {
		_isArrowInFlight = false;
		_isBlockDamage = false;

		_collider.isTrigger = false;
		_rigidbody.isKinematic = true;

		_currentHitUnit = null;
	}

	private void Update()
	{
		// Поворачиваем стрелу в полете в сторону движения
		if (_isArrowInFlight)
		{
			transform.LookAt(transform.position + _rigidbody.velocity);
		}
	}

	#endregion Mono

	#region Private methods

	/// <summary>
	/// Метод срабатывает при касании стрелы с другими объектами на которых есть коллайдер
	/// </summary>
	/// <param name="hitCollider">Коллайдер, с которым соприкоснулась стрела</param>
	private void OnTriggerEnter(Collider hitCollider)
    {
		EnemyUnit unit = hitCollider.GetComponentInParent<EnemyUnit>();

		// Если мы попали в противника
		if (unit)
		{
			if (unit != _currentHitUnit)
			{
				if (!_isBlockDamage)
				{
					_playerUnit.PerformAttack(unit, hitCollider);

					// Воспроизводим звук попадания
					_lightBow.AudioController.PlayHit();

					// Эффект попадания
					ShowHitEffect();
				}

				if (_playerUnit.PenetrationProjectile.IsActive)
				{
					_playerUnit.PenetrationProjectile.CurrentPenetration++;

					// Уменьшаем урон с каждым пробитием
					_playerUnit.Damage.Actual = _playerUnit.Damage.Max * (1 - _playerUnit.PenetrationProjectile.PenetrationDamageDecrease.Value / 100f);

					// Если число пробитий подошло к пределу, то удаляем стрелу
					if (_playerUnit.PenetrationProjectile.CurrentPenetration == _playerUnit.PenetrationProjectile.MaxPenetrationCount.Value)
					{
						_isBlockDamage = true;
						DeleteProjectile();
					}
				}
				else
				{
					_isBlockDamage = true;
					DeleteProjectile();
				}
			}

			if (unit != _currentHitUnit)
				_currentHitUnit = unit;
		}
		else
			DeleteProjectile();
	}

	private void ShowHitEffect()
    {
		_hitEffect = PoolManager.Instance?.HitEffectPool.GetFreeElement();

		_hitEffect?.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
	}

	/// <summary>
	/// Метод удаляет объект стрелы с определенной задержкой по времени
	/// </summary>
	/// <param name="secondsBeforeDeletion"></param>
	/// <returns>Задержка (в секундах) до удаления объекта стрелы</returns>
	private IEnumerator DeleteProjectileInDelay(int secondsBeforeDeletion)
    {
		if (_playerUnit.PenetrationProjectile.IsActive)
        {
			// Обнуляем кол-во пробитий
			_playerUnit.PenetrationProjectile.CurrentPenetration = 0;

			// Возвращаем исходный урон
			_playerUnit.Damage.Actual = _playerUnit.Damage.Max; //TODO Кароче это не вариант так делать
		}			

		// Ключевое слово yield указывает сопрограмме, когда следует остановиться.
		yield return new WaitForSeconds(secondsBeforeDeletion);

		// Отвязываем эффект от стрелы
        _tracerEffect?.transform.SetParent(null);
		// Удаляем объект трассера через некоторое время
		_tracerEffect?.StartCountdownToDelete();

		// Возвращаем объект в пул
		PoolManager.Instance?.ProjectileArrowPool.ReturnToContainerPool(this);
    }

	private void DeleteProjectile()
    {
		if (_playerUnit.PenetrationProjectile.IsActive)
		{
			// Обнуляем кол-во пробитий
			_playerUnit.PenetrationProjectile.CurrentPenetration = 0;

			// Возвращаем исходный урон 
			_playerUnit.Damage.Actual = _playerUnit.Damage.Max; //TODO Кароче это не вариант так делать
		}

		// Отвязываем эффект от стрелы
		_tracerEffect?.transform.SetParent(null);
		// Удаляем объект трассера через некоторое время
		_tracerEffect?.StartCountdownToDelete();

		// Возвращаем объект в пул
		PoolManager.Instance?.ProjectileArrowPool.ReturnToContainerPool(this);
	}

	private void EnableTracerEffect()
    {
		_tracerEffect = PoolManager.Instance?.TracerEffectPool.GetFreeElement();

		_tracerEffect.gameObject.transform.SetParent(transform);
		_tracerEffect.gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
	}

	#endregion Private methods

	#region Public methods

	/// <summary>
	/// Запуск снаярда стрелы
	/// </summary>
	/// <param name="lightBow">Оружие с которого запускается снаряд</param>
	/// <param name="playerUnit">Игрок, который стреляет</param>
	public void Launch (LightBow lightBow, PlayerUnit playerUnit)
    {
		_lightBow = lightBow;
		_playerUnit = playerUnit;

		transform.SetParent(null);

		_rigidbody.isKinematic = false;
		_rigidbody.AddForce((transform.forward) * _lightBow.ShotForce, ForceMode.Impulse);

        _isArrowInFlight = true;

        _collider.isTrigger = true;

		EnableTracerEffect();

		// Запускаем отсчет для удаления стрелы
		StartCoroutine(DeleteProjectileInDelay(5));
    }
	#endregion Public methods
}