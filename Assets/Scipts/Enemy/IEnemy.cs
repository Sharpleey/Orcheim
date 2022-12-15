using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int MaxHealth { get; }
    int Health { get; }
    float Speed { get; }
    void TakeDamage(int damage, DamageType typeDamage);
    void TakeHitboxDamage(int damage, Collider hitCollider, DamageType typeDamage);
    void SetBurning(int damagePerSecond, int duration, DamageType typeDamage);
    void SetSlowing(float slowdown, int duration);
}