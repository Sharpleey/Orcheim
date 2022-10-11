using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс отвечает за призыв соседних врагов атаковать игрока, когда персонаж, на котором используется этот класс, проходит рядом с другими персонажами.
/// При выходе коллайдера одного персонажа из другого, меняется состояние на преследование
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    private SwordsmanEnemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<SwordsmanEnemy>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)
    {
        // Если текущее состояние "покоя", то ничего не делаем
        if (_enemy.CurrentState == _enemy.IdleState)
            return;

        SwordsmanEnemy otherEnemy = otherSummonTriggerCollider.GetComponentInParent<SwordsmanEnemy>();

        // Если персонаж в состоянии "преследования" и другой персонаж (рядом стоящий) в состоянии "покоя", то второму меняем состояние на "преследования"
        if (_enemy.CurrentState == _enemy.PursuitState && otherEnemy.CurrentState == otherEnemy.IdleState)
        {
            otherEnemy.ChangeState(otherEnemy.PursuitState);
        }
    }
}
