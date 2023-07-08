
/// <summary>
/// ���������� ������������ �����
/// </summary>
public interface IUsesAttackModifiers
{
    CriticalAttack CriticalAttack { get; }
    FlameAttack FlameAttack { get; }
    SlowAttack SlowAttack { get; }
    PenetrationProjectile PenetrationProjectile { get; }

    /// <summary>
    /// ����� ������������� ����������� ����� �� �����
    /// </summary>
    /// <param name="attackModifier"></param>
    public void SetActiveAttackModifier(AttackModifier attackModifier);
    
    /// <summary>
    /// ����� �������������� ������������ ���� �� ������ �����
    /// </summary>
    public void InitAttackModifiers();
}
