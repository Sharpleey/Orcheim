using UnityEngine;

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

        Player player = hitCollider.GetComponent<Player>();

        if (player)
        {
            PlayerManager.Instance.TakeDamage(_enemy.Damage.Actual, _enemy.Damage.DamageType, _enemy.Damage.IsArmorIgnore);
        }
    }
}
