using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// ����� ���������� ����� �� �����
    /// </summary>
    /// <param name="damage">�������� �����</param>
    /// <param name="damageType">��� �����</param>
    /// <param name="isCriticalHit">����������� ����� ��� ���</param>
    /// <param name="hitBox">������� (���������) ���������</param>
    void TakeDamage(float damage, DamageType damageType, bool isArmorIgnore, bool isCriticalHit = false, Collider hitBox = null);
}
