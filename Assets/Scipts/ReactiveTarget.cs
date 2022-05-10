using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
   public void ReactToHit()
   {
       WanderingAI behavior = GetComponent<WanderingAI>(); 
       
       if (behavior != null) 
       {    
           // Указываем врагу новое состояние
           behavior.SetAlive(false);
       } 
       
       // Запускаем эффект смерти
       StartCoroutine(Die());
   }

   private IEnumerator Die()
   {
       // Эффект смерти
       this.transform.Rotate(-75, 0, 0);
       
       yield return new WaitForSeconds(1.5f);

       Destroy(this.gameObject);
   }
}
