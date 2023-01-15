
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


    public void SetAttackModifier(AttackModifier attackModifier);
    public void InitAttackModifiers();
}
