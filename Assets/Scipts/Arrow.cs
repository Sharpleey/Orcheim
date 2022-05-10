using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField] private int damage = 1;

	private Rigidbody rg;

	private void Start() 
	{
		// rg = this.gameObject.GetComponent<Rigidbody>();
		StartCoroutine(DeleteArrow());
	}

    void FixedUpdate() 
    {
		// transform.Translate(0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (rg != null)
		{
			// rg.isKinematic = true;
			StartCoroutine(DeleteArrow());
		}

	}

	private IEnumerator DeleteArrow()
    {   
        // Ключевое слово yield указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(5);

        // Удаляем объект со сцены и очищаем память
        Destroy(this.gameObject);
    }
}
