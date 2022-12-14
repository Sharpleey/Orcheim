using UnityEngine;

public abstract class ProcableAttackModifaer : AttackModifaer, IProcable
{
    /// <summary>
    /// ���� ����� ������������
    /// </summary>
    public abstract int Proc�hance { get; set; }

    /// <summary>
    /// ������� ����������� ��� ���
    /// </summary>
    /// <returns>true/false</returns>
    public bool IsProc()
    {
        return Random.Range(0, 100) <= Proc�hance;
    }
}
