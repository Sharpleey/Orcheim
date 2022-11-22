using UnityEngine;

/// <summary>
/// Класс отвечает за призыв соседних врагов атаковать игрока, когда персонаж, на котором используется этот класс, проходит рядом с другими персонажами.
/// При выходе коллайдера одного персонажа из другого, меняется состояние на преследование
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    public Enemy Enemy { get; private set; }

    private void Awake()
    {
        Enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)  
    {
        // Если текущее состояние "покоя", то ничего не делаем
        if (Enemy.CurrentState.GetType() == typeof(IdleState))
            return;

        Enemy otherEnemy = otherSummonTriggerCollider.GetComponent<SummonTrigger>().Enemy;

        bool onChangeState = (Enemy.CurrentState.GetType() == typeof(WarriorChasingPlayerState) || Enemy.CurrentState.GetType() == typeof(GoonChasingPlayerState))
            && otherEnemy.CurrentState.GetType() == typeof(IdleState);

        // Если персонаж в состоянии "преследования" и другой персонаж (рядом стоящий) в состоянии "покоя", то второму меняем состояние на "преследования"
        if (onChangeState)
        {
            // Воспроизводим звук
            if(otherEnemy.AudioController)
                otherEnemy.AudioController.PlaySound(EnemySoundType.Confused);

            otherEnemy.SetState<ChasingPlayerState>();
        }
    }
}
