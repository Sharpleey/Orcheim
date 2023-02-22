using UnityEngine;

public class Inspiration : ActiveAbility
{
    /// <summary>
    /// ������ �������� �����������
    /// </summary>
    public Parameter Radius { get; private set; }

    public Inspiration(Unit unit, int timeCooldown, int radius, int decreaseTimeCooldownPerLevel = 1, bool isActive = false) : base(unit, timeCooldown, decreaseTimeCooldownPerLevel, isActive)
    {
        Radius = new Parameter(radius);
    }

    public override void Apply()
    {
        Debug.Log("���������� ����������� Inspiration");
    }
}
