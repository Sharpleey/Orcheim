using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField] private int _damage = 20;
	[SerializeField] private int _damageSpread = 4;
	private int _actualDamage;

	public bool isArrowInBowstring = true;
	private bool _isArrowInFlight = false;
	private Rigidbody _arrowRigidbody;
	
	private void Start() 
	{
		_arrowRigidbody = this.gameObject.GetComponent<Rigidbody>();
		_actualDamage = GetActualDamage();
	}

	private void FixedUpdate() 
	{
		// Момент вылета стрелы
		if (!isArrowInBowstring && !_isArrowInFlight)
		{
			_isArrowInFlight = true;
			// Запускаем отсчет для удаления стрелы
			StartCoroutine(DeleteArrow(5));
		}

		transform.LookAt(transform.position + _arrowRigidbody.velocity);
	}

	void OnTriggerEnter(Collider other)
    {
		var enemy = other.GetComponentInParent<Enemy1>();

		// var enemy = other.GetComponent<Enemy1>();
		if(enemy)
		{
			enemy.ReactToHit(_actualDamage);
		}

		if(!isArrowInBowstring)
		{
			StartCoroutine(DeleteArrow(0));
		}
    }

	private IEnumerator DeleteArrow(int secondsBeforeDeletion)
    {   
        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(secondsBeforeDeletion);

        // Удаляем объект со сцены и очищаем память
        Destroy(this.gameObject);
    }

	private int GetActualDamage()
	{	
		int actualDamage = Random.Range(_damage - _damageSpread, _damage + _damageSpread);
		return actualDamage;
	}

	// private void OnCollisionEnter(Collision other) {
	// 	_rg.isKinematic = true;
	// 	StartCoroutine(DeleteArrow());
	// }
}
