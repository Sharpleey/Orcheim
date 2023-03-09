using UnityEngine;

/// <summary>
/// Класс обработки триггера, отвечающего за смену состояния врага на Chasing, если из этого триггера вышел EnemyUnit в состоянии Chasing
/// </summary>
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SummonTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask = 4096;

    private EnemyUnit _enemyUnit;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _enemyUnit = GetComponentInParent<EnemyUnit>();
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerExit(Collider otherEnemyCollider)  
    {
        JoinChasing(otherEnemyCollider);
    }

    private void OnTriggerEnter(Collider otherEnemyCollider)
    {
        JoinChasing(otherEnemyCollider); 
    }

    private void JoinChasing(Collider otherEnemyCollider)
    {
        EnemyUnit otherEnemyUnit = otherEnemyCollider.GetComponent<EnemyUnit>();

        // Если рядом с текущим юнитов проходит враг в состоянии Chasing, то меняем состояние на Chasing
        if (otherEnemyUnit?.CurrentState is ChasingState)
        {
            _enemyUnit.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.Confused);

            _enemyUnit.SetState<ChasingState>();
        }
    }

    public void SummonNearbyUnits(int radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, collisionMask);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.TryGetComponent(out EnemyUnit enemyUnit))
                enemyUnit.SetState<ChasingState>();
        }
    }

    /// <summary>
    /// Включает/отключает работу триггера
    /// </summary>
    /// <param name="isEnable">Включить/отключить</param>
    public void SetEnable(bool isEnable)
    {
        _boxCollider.enabled = isEnable;
    }
}
