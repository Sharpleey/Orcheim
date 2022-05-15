using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField] private int damage = 1;

	private Rigidbody rg;
	
	private void Start() 
	{
		rg = this.gameObject.GetComponent<Rigidbody>();
	}

	private void FixedUpdate() 
	{
		transform.LookAt(transform.position + rg.velocity);
		Debug.Log(rg.velocity);
	}

	void OnTriggerEnter(Collider other)
    {
        rg.isKinematic = true;
		StartCoroutine(DeleteArrow());
    }

	private IEnumerator DeleteArrow()
    {   
        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(5);

        // Удаляем объект со сцены и очищаем память
        Destroy(this.gameObject);
    }

	// private void OnCollisionEnter(Collision other) {
	// 	rg.isKinematic = true;
	// 	StartCoroutine(DeleteArrow());
	// }
}
