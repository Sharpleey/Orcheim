using UnityEngine;

/// <summary>
/// Класс отвечает за призыв соседних врагов атаковать игрока, когда персонаж, на котором используется этот класс, проходит рядом с другими персонажами.
/// При выходе коллайдера одного персонажа из другого, меняется состояние на преследование
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)
    {
        // Если текущее состояние "покоя", то ничего не делаем
        if (_enemy.CurrentState.GetType() == typeof(IdleState))
            return;

        Enemy otherEnemy = otherSummonTriggerCollider.GetComponentInParent<Enemy>();

        // Если персонаж в состоянии "преследования" и другой персонаж (рядом стоящий) в состоянии "покоя", то второму меняем состояние на "преследования"
        if (_enemy.CurrentState.GetType() == typeof(PursuitState) && otherEnemy.CurrentState.GetType() == typeof(IdleState))
        {
            otherEnemy.SetState<PursuitState>();
        }
    }
}
