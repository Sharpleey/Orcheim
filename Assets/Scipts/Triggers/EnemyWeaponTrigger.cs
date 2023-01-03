using UnityEngine;

/// <summary>
/// ����� ���������� �� ��������� ������ ����� ������, ��������� ��������������� �� ������ � ������� ���������� �� ������ ����������
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
