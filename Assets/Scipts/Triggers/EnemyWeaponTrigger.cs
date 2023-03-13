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
        if (hitCollider.TryGetComponent(out PlayerUnit playerUnit))
        {
            _enemyUnit.PerformAttack(playerUnit);
        }
    }
}
