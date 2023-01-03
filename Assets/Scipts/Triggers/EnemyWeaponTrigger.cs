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
        PlayerUnit playerUnit = hitCollider.GetComponent<PlayerUnit>();

        if (playerUnit)
        {
            _enemyUnit.PerformAttack(playerUnit);
        }
    }
}
