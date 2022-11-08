using UnityEngine;
using static MessengerInternal;

/// <summary>
/// ����� ���������� �� ��������� ������ ����� ������, ��������� ��������������� �� ������ � ������� ���������� �� ������ ����������
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
