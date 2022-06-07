using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]

public class Enemy1 : MonoBehaviour, IEnemy
{
    public HitBoxesController HitBoxesController {get; set;}

    [SerializeField] private float _maxHealth = 100;

    // [SerializeField] private TextMeshProUGUI _textTakingDamage;

    public float MaxHealth {get; set;}
    public float Health {get; set;}

    private Material _enemyMaterial;
    

    private void Awake()
    {
        HitBoxesController = GetComponent<HitBoxesController>();

        MaxHealth = _maxHealth;
        Health = MaxHealth;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToHit(int damage, Collider hitCollider)
    {
        // Получаем значение урона с учетом попадания в ту или иную часть тела
        damage = HitBoxesController.GetDamageValue(damage, hitCollider);
        // damage = GetDamageValue(damage, hitCollider);

        PopupDamage popupDamage = GetComponent<PopupDamage>();
        popupDamage.SetText(damage.ToString());
        popupDamage.ShowAndHide();
        
        Health -= damage;

        if(Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    // private int GetDamageValue(int damage, Collider hitCollider)
    // {   
    //     Debug.Log("Стрела: " + HitBoxesController.hitCollider.GetInstanceID());
    //     Debug.Log("Противник голова: " + HitBoxesController_headCollider.GetInstanceID());
    //     // Сравниваю по имени, потому что при сравнении объектов, работает только на первои экземпляре префаба
    //     if (hitCollider.name == HitBoxesController._headCollider.name)
    //     {
    //         float actualDamage = damage * HitBoxesController._headDamageMultiplier;
    //         return (int)actualDamage;
    //     }

    //     foreach (Collider handCollider in HitBoxesController._handColliders)
    //     {
    //         if (hitCollider.name == HitBoxesController.handCollider.name)
    //         {
    //             float actualDamage = damage * HitBoxesController._handDamageMultiplier;
    //             return (int)actualDamage;
    //         }
    //     } 
        
    //     foreach (Collider legCollider in HitBoxesController._legColliders)
    //     {
    //         if (hitCollider.name == HitBoxesController.legCollider.name)
    //         {
    //             float actualDamage = damage * HitBoxesController._legDamageMultiplier;
    //             return (int)actualDamage;
    //         }
    //     }

    //     foreach (Collider bodyCollider in HitBoxesController._bodyColliders)
    //     {
    //         if (hitCollider.name == HitBoxesController.bodyCollider.name)
    //         {
    //             float actualDamage = damage * HitBoxesController._bodyDamageMultiplier;
    //             return (int)actualDamage;
    //         }
    //     }

    //     return 1;
    // }

    private IEnumerator Die() 
    {
        RagdollController ragdollControl = GetComponent<RagdollController>();
        if(ragdollControl)
            ragdollControl.MakePhysical();

		yield return new WaitForSeconds(3.5f);
		
		Destroy(this.gameObject);
	}
}
