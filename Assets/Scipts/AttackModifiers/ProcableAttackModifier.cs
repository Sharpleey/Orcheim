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

    public ProcableAttackModifier(bool isActive = false, int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, int maxLevelProcChance = 18) : base(isActive)
    {
        �hance = new Parameter(defaultValue: procChance, increaseValuePerLevel: increaseProcChancePerLevel, level: levelProcChance, maxLevel: maxLevelProcChance);
    }
}
