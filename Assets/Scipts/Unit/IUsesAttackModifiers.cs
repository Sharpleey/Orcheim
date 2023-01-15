
/// <summary>
/// ���������� ������������ �����
/// </summary>
public interface IUsesAttackModifiers
{
    /// <summary>
    /// ������� ������������ ������ ������������� ����
    /// </summary>
    //public Dictionary<Type, AttackModifaer> AttackModifaers { get; }

    CriticalAttack CriticalAttack { get; }
    FlameAttack FlameAttack { get; }
    SlowAttack SlowAttack { get; }
    PenetrationProjectile PenetrationProjectile { get; }

    /// <summary>
    /// ����� ������������� ����������� ����� �� �����
    /// </summary>
    /// <param name="attackModifier"></param>
    public void SetAttackModifier(AttackModifier attackModifier);
    
    /// <summary>
    /// ����� �������������� ������������ ���� �� ������ �����
    /// </summary>
    public void InitAttackModifiers();
}
