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
    /// <param name="hitBox">�������, � ������� ����� ������ �������� ������</param>
    void PerformAttack(Unit attackedUnit, Collider hitBox);
}
