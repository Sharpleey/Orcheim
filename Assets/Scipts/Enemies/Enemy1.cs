using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy1 : MonoBehaviour, IEnemy
{
    [SerializeField] private float _maxHealth = 100;

    [SerializeField] private TextMeshProUGUI _textTakingDamage;

    public float MaxHealth {get; set;}
    public float Health {get; set;}

    private Material _enemyMaterial;
    

    private void Awake()
    {
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

    public void ReactToHit(int damage)
    {
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
        var ragdollControl = GetComponent<RagdollControl>();
        if(ragdollControl)
            ragdollControl.MakePhysical();

		yield return new WaitForSeconds(3.5f);
		
		Destroy(this.gameObject);
	}
}
