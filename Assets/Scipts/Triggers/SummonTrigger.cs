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

        bool onChangeState = (Enemy.CurrentState.GetType() == typeof(WarriorChasingState) || Enemy.CurrentState.GetType() == typeof(GoonChasingState))
            && (otherEnemy.CurrentState.GetType() == typeof(IdleState) || otherEnemy.CurrentState.GetType() == typeof(PatrollingState));

        // ���� �������� � ��������� "�������������" � ������ �������� (����� �������) � ��������� "�����" ��� "��������������", �� ������� ������ ��������� �� "�������������"
        if (onChangeState)
        {
            // ������������� ����
            if(otherEnemy.AudioController)
                otherEnemy.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Confused);

            otherEnemy.SetState<ChasingState>();
        }
    }
}
