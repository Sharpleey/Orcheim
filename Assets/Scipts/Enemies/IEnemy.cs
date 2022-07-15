using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int MaxHealth {get; }
    int Health {get; }
    float Speed { get; }
    void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage);
}