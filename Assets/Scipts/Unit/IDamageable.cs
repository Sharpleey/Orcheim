using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// ����� ���������� ����� �� �����
    /// </summary>
    /// <param name="damage">������ ������ ��������� �����</param>
    void TakeDamage(Damage damage, Collider hitBox = null);
}
