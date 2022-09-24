using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider otherSummonTriggerCollider)
    {
        bool curEnemyIsAttacked = GetComponentInParent<SwordsmanEnemy>().IsAttacked;

        if (!curEnemyIsAttacked)
            return;

        SwordsmanEnemy swordsmanEnemy = otherSummonTriggerCollider.GetComponentInParent<SwordsmanEnemy>();

        if (!swordsmanEnemy.IsAttacked)
        {
            swordsmanEnemy.IsAttacked = true;
        }
    }
}
