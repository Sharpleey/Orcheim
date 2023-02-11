using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// ����� ���������� ����� �� �����
    /// </summary>
    /// <param name="damage">������ ��������� Damage</param>
    /// <param name="isCriticalHit">����������� ����� ��� ���</param>
    /// <param name="hitBox">������� (���������) ���������</param>
    void TakeDamage(Damage damage, bool isCriticalHit = false, Collider hitBox = null);
}
