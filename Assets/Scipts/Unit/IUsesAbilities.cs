using System;
using System.Collections.Generic;

/// <summary>
/// ���� ���������� �����������
/// </summary>
public interface IUsesAbilities
{
    /// <summary>
    /// ������� ��� �������� ������������ �����
    /// </summary>
    public Dictionary<Type, Ability> Abilities { get; }

    /// <summary>
    /// ������������� ������������
    /// </summary>
    public void InitAbilities();
}
