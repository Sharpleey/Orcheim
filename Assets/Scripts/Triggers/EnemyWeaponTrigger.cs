using UnityEngine;

/// <summary>
/// Класс отвечающий за нанесение врагом урона игроку, компонент устанавливается на объект с триггер колладером на оружие противника
/// </summary>
public class EnemyWeaponTrigger : MonoBehaviour
{
    private EnemyUnit _enemyUnit;
    private void Start()
    {
        _enemyUnit = GetComponentInParent<EnemyUnit>();
    }

    private void OnTriggerEnter(Collider hitCollider)
    {
        if (hitCollider.TryGetComponent(out PlayerUnit playerUnit))
        {
            _enemyUnit.PerformAttack(playerUnit);
        }
    }
}
