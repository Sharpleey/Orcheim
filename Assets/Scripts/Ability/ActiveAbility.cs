using System.Collections;
using UnityEngine;

/// <summary>
/// ����������� ����� �������� ����������� 
/// </summary>
public abstract class ActiveAbility : Ability
{
    /// <summary>
    /// ��������� �� ����������� � �����������
    /// </summary>
    public bool IsCooldown { get; protected set; }

    /// <summary>
    /// ����� ������ �����������
    /// </summary>
    public Parameter TimeCooldown { get; protected set; }

    public ActiveAbility(Unit unit, int timeCooldown, int decreaseTimeCooldownPerLevel = 1, bool isActive = false) : base(unit: unit, isActive: isActive)
    {
        TimeCooldown = new Parameter(defaultValue: timeCooldown, changeValuePerLevel: decreaseTimeCooldownPerLevel);
    }

    /// <summary>
    /// ����� ���������� �����������
    /// </summary>
    public abstract void Apply();

    /// <summary>
    /// ����� �����������
    /// </summary>
    /// <returns></returns>
    public IEnumerator Cooldown()
    {
        IsCooldown = true;

        yield return new WaitForSeconds(TimeCooldown.Value);

        IsCooldown = false;
    }
}
