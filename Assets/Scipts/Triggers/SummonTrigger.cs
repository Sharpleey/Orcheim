using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTrigger : MonoBehaviour
{
    [SerializeField] private SwordsmanEnemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<SwordsmanEnemy>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)
    {
        if (_enemy.CurrentState == _enemy.IdleState)
            return;

        SwordsmanEnemy otherEnemy = otherSummonTriggerCollider.GetComponentInParent<SwordsmanEnemy>();

        if (_enemy.CurrentState == _enemy.PursuitState && otherEnemy.CurrentState == otherEnemy.IdleState)
        {
            otherEnemy.ChangeState(otherEnemy.PursuitState);
        }
    }
}
