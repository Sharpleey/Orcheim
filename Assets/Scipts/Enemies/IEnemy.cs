using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int MaxHealth { get; }
    int Health { get; }
    float Speed { get; }
    void TakeDamage(int damage, TypeDamage typeDamage);
    void TakeHitboxDamage(int damage, Collider hitCollider, TypeDamage typeDamage);
    void SetBurning(int damagePerSecond, int duration, TypeDamage typeDamage);
    void SetSlowing(float slowdown, int duration);
}