using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField] private int damage = 1;
	// [SerializeField] private Transform pointTrigger;

	private Rigidbody rg;
	
	private void Start() 
	{
		rg = this.gameObject.GetComponent<Rigidbody>();
		// StartCoroutine(DeleteArrow());
	}

	// void OnTriggerEnter(Collider other) 
	// {
	// 	if (rg != null)
	// 	{
	// 		// rg.isKinematic = true;
	// 		StartCoroutine(DeleteArrow());
	// 	}

	// }
	private void FixedUpdate() {
		transform.LookAt(transform.position + rg.velocity);
	}

	private IEnumerator DeleteArrow()
    {   
        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(5);

        // Удаляем объект со сцены и очищаем память
        Destroy(this.gameObject);
    }

	void OnTriggerEnter(Collider other)
    {
        rg.isKinematic = true;
    }
}
