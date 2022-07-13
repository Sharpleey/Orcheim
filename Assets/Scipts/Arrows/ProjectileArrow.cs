using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
	#region Serialize Fields

	#endregion Serialize Fields

	#region Properties

	//public int BowDamage
	//   {
	//	get
	//	{
	//		return _bowDamage;
	//	}
	//	set
	//	{
	//		if (value < 0)
	//		{
	//			_bowDamage = 0;
	//			return;
	//		}
	//		_bowDamage = value;
	//	}
	//}
	#endregion Properties

	#region Fields
	[HideInInspector] public bool isArrowInBowstring;

	public List<IModifier> bowAttackModifiers;

	private bool _isArrowInFlight;
	[SerializeField] private bool _onPenetrationMod;

	private Rigidbody _arrowRigidbody;
	#endregion Fields

	#region Methods
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

		//_onPenetrationMod = CheakPenetrationMod();

		_arrowRigidbody = gameObject.GetComponent<Rigidbody>();


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

		// Стрела в полете
		if (_isArrowInFlight)
        {
			transform.LookAt(transform.position + _arrowRigidbody.velocity);
		}
	}

    void OnTriggerEnter(Collider hitCollider)
    {
        IEnemy enemy = hitCollider.GetComponentInParent<Enemy1>();
        if (enemy != null)
        {
			
        }

        if (!isArrowInBowstring)
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

	#endregion Methods
}
