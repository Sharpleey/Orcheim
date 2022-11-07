using UnityEngine;

/// <summary>
/// ����� �������� �� ������ �������� ������ ��������� ������, ����� ��������, �� ������� ������������ ���� �����, �������� ����� � ������� �����������.
/// ��� ������ ���������� ������ ��������� �� �������, �������� ��������� �� �������������
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    private Warrior _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Warrior>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)
    {
        // ���� ������� ��������� "�����", �� ������ �� ������
        if (_enemy.CurrentState.GetType() == typeof(IdleState))
            return;

        Warrior otherEnemy = otherSummonTriggerCollider.GetComponentInParent<Warrior>();

        // ���� �������� � ��������� "�������������" � ������ �������� (����� �������) � ��������� "�����", �� ������� ������ ��������� �� "�������������"
        if (_enemy.CurrentState.GetType() == typeof(PursuitState) && otherEnemy.CurrentState.GetType() == typeof(IdleState))
        {
            otherEnemy.SetPursuitState();
        }
    }
}
