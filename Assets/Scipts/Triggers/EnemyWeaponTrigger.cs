using UnityEngine;

/// <summary>
/// Класс отвечающий за нанесение врагом урона игроку, компонент устанавливается на объект с триггер колладером на оружие противника
/// </summary>
public class EnemyWeaponTrigger : MonoBehaviour
{
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider hitCollider)
    {
        //PlayerCharacterController player = hitCollider.GetComponentInParent<PlayerCharacterController>();

        Player player = hitCollider.GetComponent<Player>();

        if (player)
        {
            player.TakeDamage(_enemy.ActualDamage);
        }
    }
}
