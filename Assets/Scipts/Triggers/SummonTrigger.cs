using UnityEngine;

/// <summary>
/// ����� �������� �� ������ �������� ������ ��������� ������, ����� ��������, �� ������� ������������ ���� �����, �������� ����� � ������� �����������.
/// ��� ������ ���������� ������ ��������� �� �������, �������� ��������� �� �������������
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    public EnemyUnit EnemyUnit { get; private set; }

    private void Awake()
    {
        EnemyUnit = GetComponentInParent<EnemyUnit>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)  
    {
        // ���� ������� ��������� "�����", �� ������ �� ������
        if (EnemyUnit.CurrentState.GetType() == typeof(IdleState))
            return;

        EnemyUnit otherEnemy = otherSummonTriggerCollider.GetComponent<SummonTrigger>().EnemyUnit;

        bool onChangeState = (EnemyUnit.CurrentState.GetType() == typeof(WarriorChasingState) || EnemyUnit.CurrentState.GetType() == typeof(GoonChasingState))
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
