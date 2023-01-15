using UnityEngine;

public abstract class ProcableAttackModifier : AttackModifier
{
    /// <summary>
    /// ���� ����� ������������
    /// </summary>
    public Parameter �hance { get; protected set; }

    /// <summary>
    /// ������� ����������� ��� ���
    /// </summary>
    /// <returns>true/false</returns>
    public bool IsProc
    {
        get => Random.Range(0, 100) <= �hance.Value;
    }
}
