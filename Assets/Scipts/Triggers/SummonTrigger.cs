using UnityEngine;

/// <summary>
/// ����� �������� �� ������ �������� ������ ��������� ������, ����� ��������, �� ������� ������������ ���� �����, �������� ����� � ������� �����������.
/// ��� ������ ���������� ������ ��������� �� �������, �������� ��������� �� �������������
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)
    {
        // ���� ������� ��������� "�����", �� ������ �� ������
        if (_enemy.CurrentState.GetType() == typeof(IdleState))
            return;

        Enemy otherEnemy = otherSummonTriggerCollider.GetComponentInParent<Enemy>();

        // ���� �������� � ��������� "�������������" � ������ �������� (����� �������) � ��������� "�����", �� ������� ������ ��������� �� "�������������"
        if (_enemy.CurrentState.GetType() == typeof(PursuitState) && otherEnemy.CurrentState.GetType() == typeof(IdleState))
        {
            otherEnemy.SetState<PursuitState>();
        }
    }
}
