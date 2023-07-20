using System;
using System.Collections.Generic;

/// <summary>
/// ���������� ������������ �����
/// </summary>
public interface IUsesAttackModifiers
{
    public Dictionary<Type, AttackModifier> AttackModifiers { get; }
    public void SetAttackModifier(AttackModifier attackModifier);
    public void RemoveAttackModifier<T>() where T : AttackModifier;

    /// <summary>
    /// ����� �������������� ������������ ���� �� ������ �����
    /// </summary>
    public void InitAttackModifiers();
}
