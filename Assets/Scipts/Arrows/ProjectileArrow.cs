using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectDamage))]
[RequireComponent(typeof(Rigidbody))]
public class ProjectileArrow : MonoBehaviour
{
	#region Serialize fields

	#endregion Serialize fields

	#region Properties

	#endregion Properties

	#region Private fields
	private bool _isArrowInFlight;
	private bool _onPenetrationMod;

	private Rigidbody _arrowRigidbody;

	#endregion Private fields

	#region Public fields
	[HideInInspector] 
	public bool isArrowInBowstring;

	public List<IModifier> bowAttackModifiers;
	#endregion Public fields

	#region Mono
	private void Awake()
    {
		//DirectDamage = _directDamage;
		//DamageSpread = _damageSpread;
		//Name = _name;
    }

    private void Start() 
	{
		isArrowInBowstring = true;
		_isArrowInFlight = false;

		_onPenetrationMod = UnityUtility.HasComponent<Penetration>(gameObject);

        _arrowRigidbody = GetComponent<Rigidbody>();


	}
    #endregion Mono

    #region Private methods
    private void FixedUpdate() 
	{
		// Отлавливаем момент вылета стрелы
		if (!isArrowInBowstring && !_isArrowInFlight)
		{
			_isArrowInFlight = true;
			// Запускаем отсчет для удаления стрелы
			StartCoroutine(DeleteArrow(5));
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
		// Удаляем стрелу, если (она находится в полете и попала не в противника) или (она попала в противника и находится в полете и не включен мод на пробитие)
		if ((_isArrowInFlight && enemy == null) || (_isArrowInFlight && !_onPenetrationMod))
			Destroy(gameObject);
    }

    private IEnumerator DeleteArrow(int secondsBeforeDeletion)
    {   
        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(secondsBeforeDeletion);

        // Удаляем объект со сцены и очищаем память
        Destroy(gameObject);
    }

	#endregion Private methods
}
