using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class ProjectileArrow : MonoBehaviour
{
	#region Private fields

	private bool _isArrowInFlight;
	private bool _isBlockDamage;

	private int _currentPenetration;

	private LightBow _lightBow;

	private TracerEffect _tracerEffect;
	private HitEffect _hitEffect;

	private Rigidbody _rigidbody;
	private BoxCollider _collider;

	private EnemyUnit _currentHitUnit;
	private PlayerUnit _playerUnit;

	private Pool<HitEffect> _hitEffectPool;
	private Pool<TracerEffect> _tracerEffectPool;
	private Pool<ProjectileArrow> _projectileArrowPool;

	#endregion Private fields

	#region Mono

	[Inject]
	private void Construct(Pool<HitEffect> hitEffectPool, Pool<TracerEffect> tracerEffectPool, Pool<ProjectileArrow> projectileArrowPool)
    {
		_hitEffectPool = hitEffectPool;
		_tracerEffectPool = tracerEffectPool;
		_projectileArrowPool = projectileArrowPool;
	}

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
		_currentPenetration = 0;
	}

	private void Update()
	{
		// Поворачиваем стрелу в полете в сторону движения
		if (_isArrowInFlight)
		{
			transform.LookAt(transform.position + _rigidbody.velocity);
		}
	}

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
			if (unit != _currentHitUnit && !(unit.CurrentState is DieState))
			{
				if (!_isBlockDamage)
				{
					_playerUnit.PerformAttack(unit, _currentPenetration, hitCollider);

					// Воспроизводим звук попадания
					_lightBow.AudioController.PlayHit();

					// Эффект попадания
					ShowHitEffect();
				}

				if (_playerUnit.AttackModifiers.TryGetValue(typeof(PenetrationProjectile), out AttackModifier modifierPenetrationProjectile))
				{
					PenetrationProjectile penetrationProjectile = (PenetrationProjectile)modifierPenetrationProjectile;

					if (penetrationProjectile.IsActive)
					{
						_currentPenetration++;

						// Если число пробитий подошло к пределу, то удаляем стрелу
						if (_currentPenetration == penetrationProjectile.MaxPenetrationCount.Value)
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

	#endregion Mono

	#region Private methods

	private void ShowHitEffect()
    {
		_hitEffect = _hitEffectPool?.GetFreeElement();

		_hitEffect?.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
	}

	/// <summary>
	/// Метод удаляет объект стрелы с определенной задержкой по времени
	/// </summary>
	/// <param name="secondsBeforeDeletion"></param>
	/// <returns>Задержка (в секундах) до удаления объекта стрелы</returns>
	private IEnumerator DeleteProjectileInDelay(int secondsBeforeDeletion)
    {
		// Ключевое слово yield указывает сопрограмме, когда следует остановиться.
		yield return new WaitForSeconds(secondsBeforeDeletion);

		DeleteProjectile();
	}

	private void DeleteProjectile()
    {
        // Отвязываем эффект от стрелы
        _tracerEffect?.transform.SetParent(null);
		// Удаляем объект трассера через некоторое время
		_tracerEffect?.StartCountdownToDelete();

		// Возвращаем объект в пул
		_projectileArrowPool?.ReturnToContainerPool(this);
	}

	private void EnableTracerEffect()
    {
		_tracerEffect = _tracerEffectPool?.GetFreeElement();

		_tracerEffect.gameObject.transform.SetParent(transform);
		_tracerEffect.gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
		_tracerEffect.TrailRenderer.enabled = true;
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