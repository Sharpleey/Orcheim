using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float MaxHealth {get; set;}
    float Health {get; set;}
    void TakeHitboxDamage(int damage, Collider hitCollider);
}