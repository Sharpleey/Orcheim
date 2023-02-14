using System.Collections;
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

	private LightBow _lightBow;
	private BowAudioController _bowAudioController;
	private Rigidbody _arrowRigidbody;
	private BoxCollider _arrowCollider;

	private Unit _currentHitUnit;

	private PlayerUnit _playerUnit;
	#endregion Private fields

	#region Mono
	private void Start() 
	{
		_isArrowInFlight = false;
		_isBlockDamage = false;

		_lightBow = GetComponentInParent<LightBow>();
		_playerUnit = _lightBow.Player;

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
		Unit unit = hitCollider.GetComponentInParent<EnemyUnit>();

		// Если мы попали в противника
		if (unit)
		{
			if (unit != _currentHitUnit)
			{
                if (!_isBlockDamage)
                {
					_playerUnit.PerformAttack(unit, hitCollider);

					// Воспроизводим звук попадания
					_bowAudioController.PlayHit();

					// Эффект попадания
					GameObject hitObj = Instantiate(_hitEffect, transform.position, transform.rotation); //TODO Сделать через Pull objects
				}

				if (_playerUnit.PenetrationProjectile.IsActive)
                {
					_playerUnit.PenetrationProjectile.CurrentPenetration++;

					// Уменьшаем урон с каждым пробитием
					_playerUnit.Damage.Actual = _playerUnit.Damage.Max * (1 - _playerUnit.PenetrationProjectile.PenetrationDamageDecrease.Value/100f);

					// Если число пробитий подошло к пределу, то удаляем стрелу
					if (_playerUnit.PenetrationProjectile.CurrentPenetration == _playerUnit.PenetrationProjectile.MaxPenetrationCount.Value)
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

			if (unit != _currentHitUnit)
				_currentHitUnit = unit;
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
		if (_playerUnit.PenetrationProjectile.IsActive)
        {
			// Обнуляем кол-во пробитий
			_playerUnit.PenetrationProjectile.CurrentPenetration = 0;

			// Возвращаем исходный урон
			_playerUnit.Damage.Actual = _playerUnit.Damage.Max;
		}			

		// Ключевое слово yield указывает сопрограмме, когда следует остановиться.
		yield return new WaitForSeconds(secondsBeforeDeletion);

		// Отвязываем эффект от стрелы
        _tracerEffect?.transform.SetParent(null);
		// Удаляем объект трассера через некоторое время
		_tracerEffect?.GetComponent<TracerEffectController>()?.DeleteTracer();

        // Удаляем объект со сцены и очищаем память
        Destroy(gameObject); //TODO Сделать через Pull objects
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