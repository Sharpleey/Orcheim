using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(HitBoxesController))]
[RequireComponent(typeof(RagdollController))]

public class Enemy1 : MonoBehaviour, IEnemy
{
    public static HitBoxesController HitBoxesController {get; private set;}

    [SerializeField] private float _maxHealth = 100;

    [SerializeField] private TextMeshProUGUI _textTakingDamage;

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

        PopupDamage popupDamage = _textTakingDamage.GetComponent<PopupDamage>();
        popupDamage.SetText(damage.ToString());
        popupDamage.ShowAndHide();
        
        Health -= damage;

        if(Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() 
    {
        RagdollController ragdollControl = GetComponent<RagdollController>();
        if(ragdollControl)
            ragdollControl.MakePhysical();

		yield return new WaitForSeconds(3.5f);
		
		Destroy(gameObject);
	}
}
