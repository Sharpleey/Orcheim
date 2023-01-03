using UnityEngine;

/// <summary>
/// Класс отвечает за призыв соседних врагов атаковать игрока, когда персонаж, на котором используется этот класс, проходит рядом с другими персонажами.
/// При выходе коллайдера одного персонажа из другого, меняется состояние на преследование
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    public EnemyUnit EnemyUnit { get; private set; }

    private void Awake()
    {
        EnemyUnit = GetComponentInParent<EnemyUnit>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)  
    {
        // Если текущее состояние "покоя", то ничего не делаем
        if (EnemyUnit.CurrentState.GetType() == typeof(IdleState))
            return;

        EnemyUnit otherEnemy = otherSummonTriggerCollider.GetComponent<SummonTrigger>().EnemyUnit;

        bool onChangeState = (EnemyUnit.CurrentState.GetType() == typeof(WarriorChasingState) || EnemyUnit.CurrentState.GetType() == typeof(GoonChasingState))
            && (otherEnemy.CurrentState.GetType() == typeof(IdleState) || otherEnemy.CurrentState.GetType() == typeof(PatrollingState));

        // Если персонаж в состоянии "преследования" и другой персонаж (рядом стоящий) в состоянии "покоя" или "патрулирования", то второму меняем состояние на "преследования"
        if (onChangeState)
        {
            // Воспроизводим звук
            if(otherEnemy.AudioController)
                otherEnemy.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Confused);

            otherEnemy.SetState<ChasingState>();
        }
    }
}
