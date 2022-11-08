using UnityEngine;
using static MessengerInternal;

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

        if(hitCollider.tag == "Player")
        {
            try
            {
                Messenger<int>.Broadcast(GlobalGameEvent.PLAYER_DAMAGED, _enemy.ActualDamage);
            }
            catch (BroadcastException exeption)
            {
                Debug.Log(exeption.Message);
            }
        }
    }
}
