
/// <summary>
/// ����� �������, ������� ����� ������������ �����
/// </summary>
public class AwardAttackModifier : Award
{
    /// <summary>
    /// ����������� �����, ������� �� �������� � �������� �������, ������� ����� ������� ������
    /// </summary>
    public AttackModifier AttackModifier { get; private set; }

    public AwardAttackModifier(AwardType awardType, string name, AttackModifier attackModifier) : base(awardType, name)
    {
        AttackModifier = attackModifier;
    }
}
