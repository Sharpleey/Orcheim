using UnityEngine;

/// <summary>
/// ��������� ����
/// </summary>
public interface IAttacking
{
    /// <summary>
    /// ����� ���������� �����, ��������� �����
    /// </summary>
    /// <param name="attackedUnit">����������� ����</param>
    /// <param name="currentPenetration">������� ���-�� �������� ��������</param>
    /// <param name="hitBox">�������, � ������� ����� ������ �������� ������</param>
    void PerformAttack(Unit attackedUnit, int currentPenetration, Collider hitBox);
}
