using System;
using System.Collections.Generic;

/// <summary>
/// ��������� ��������
/// </summary>
public interface IInfluenceOfEffects
{
    /// <summary>
    /// ������� �������� �������� ������������� �� ����� 
    /// </summary>
    public Dictionary<Type, Effect> ActiveEffects { get; }

    /// <summary>
    /// ����� ������������� � ��������� ������ �� �����
    /// </summary>
    /// <param name="effect">������, ������� ����� ���������� �� �����</param>
    void SetEffect(Effect effect);

    /// <summary>
    /// ����� ������� ������������ ������ � �����
    /// </summary>
    /// <param name="effect">������, ������� ����� �����</param>
    void RemoveEffect(Effect effect);
}
