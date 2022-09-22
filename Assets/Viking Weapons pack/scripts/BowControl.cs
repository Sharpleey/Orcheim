using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FunFant {

	public class BowControl : MonoBehaviour {

		//Add projectile to string, apply rigidbody set useGravity to false
		public Transform projectile;
		public Transform projectileParent;

		//Force factor for shooting
		public float force = 5f;

		//Save projectile starting dimensions
		private Vector3 originalPosition;
		private Vector3 originalEulerAngles;
		private Vector3 originalScale;

		//Save in list if needed to have handles for removal
		private List<Transform> projectiles;

		Animator animator;

		void Start () {

			projectiles = new List<Transform>();

			if (projectile != null) {
				projectile.GetComponent<MeshRenderer>().enabled = false;
				originalEulerAngles = projectile.localEulerAngles;
				originalPosition = projectile.localPosition;
				originalScale = projectile.localScale;
			}

			animator = this.gameObject.GetComponent<Animator>();	
		}

		void Reload () {
			Transform newProjectileInstance = Transform.Instantiate(projectile);

			Rigidbody projectileRigidbody = newProjectileInstance.GetComponent<Rigidbody>();
			if (projectileRigidbody != null) {
				projectileRigidbody.useGravity = false;
			}

			newProjectileInstance.parent = projectileParent;
			newProjectileInstance.localPosition = originalPosition;
			newProjectileInstance.localEulerAngles = originalEulerAngles;
			newProjectileInstance.localScale = originalScale;

			newProjectileInstance.GetComponent<MeshRenderer>().enabled = true;

			projectiles.Insert(0, newProjectileInstance);
		}

		void Update () {

			if (Input.GetKeyUp(KeyCode.R) || Input.GetMouseButtonDown(2)) {
				Reload ();
			}

			else if (Input.GetKeyUp(KeyCode.Plus) ||Input.GetKeyUp(KeyCode.KeypadPlus)) {

					force+=0.5f;

				if(force>6f)
				{
					force=6f;
				}
			}
			else if (Input.GetKeyUp(KeyCode.Minus) ||Input.GetKeyUp(KeyCode.KeypadMinus)) {
				force-=0.5f;
				
				if(force<0f)
				{
					force=0f;
				}
			}

			if(Input.GetMouseButtonDown(0)) {
				animator.SetBool("Shoot", true);

				//Apply forward force to projectile when shot
				if (projectiles!=null && projectiles.Count>0) {
					if (projectiles[0] != null) {
						Rigidbody projectileRigidbody = projectiles [0].GetComponent<Rigidbody> ();

						projectiles [0].parent = null;

						if (projectileRigidbody != null && projectileRigidbody.useGravity == false) {
							projectileRigidbody.useGravity = true;
							projectileRigidbody.AddRelativeForce (Vector3.forward * force, ForceMode.Impulse);
						}
					}
				}
			}
			else {
				animator.SetBool("Shoot", false);
			}
			
			if(Input.GetMouseButtonDown(1)){
				animator.SetBool("Load", true);
			}
			else{
				animator.SetBool("Load", false);
			}
			
		}
		
		void OnGUI(){
			GUI.Label (new Rect (10, 10, 500, 300), "Press Middle Mouse button or R key to reload the bow");
			GUI.Label (new Rect (10, 50, 500, 300), "Press Right Mouse Button to load the bow");
			GUI.Label (new Rect (10, 65, 500, 300), "Press Left Mouse Button to shoot the bow");
			GUI.Label (new Rect (10, 25, 500, 300), "Press + / - key to set the force (" + force + ")");
		}
	}

}