using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    [Header("�������, ����� �� ������� ����� ������������ �������������� (����������� �� ��������)")]
    [SerializeField] private Transform[] _patrolWayPoints;

    /// <summary>
    /// ����� ��������, ������������� � ������� ������ �� ���������� ����������
    /// </summary>
    public Transform[] WayPoints => _patrolWayPoints;
}
