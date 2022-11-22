using UnityEngine;

/// <summary>
/// ����� �������� �� ������ �������� ������ ��������� ������, ����� ��������, �� ������� ������������ ���� �����, �������� ����� � ������� �����������.
/// ��� ������ ���������� ������ ��������� �� �������, �������� ��������� �� �������������
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    public Enemy Enemy { get; private set; }

    private void Awake()
    {
        Enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)  
    {
        // ���� ������� ��������� "�����", �� ������ �� ������
        if (Enemy.CurrentState.GetType() == typeof(IdleState))
            return;

        Enemy otherEnemy = otherSummonTriggerCollider.GetComponent<SummonTrigger>().Enemy;

        bool onChangeState = (Enemy.CurrentState.GetType() == typeof(WarriorChasingPlayerState) || Enemy.CurrentState.GetType() == typeof(GoonChasingPlayerState))
            && otherEnemy.CurrentState.GetType() == typeof(IdleState);

        // ���� �������� � ��������� "�������������" � ������ �������� (����� �������) � ��������� "�����", �� ������� ������ ��������� �� "�������������"
        if (onChangeState)
        {
            // ������������� ����
            if(otherEnemy.AudioController)
                otherEnemy.AudioController.PlaySound(EnemySoundType.Confused);

            otherEnemy.SetState<ChasingPlayerState>();
        }
    }
}
