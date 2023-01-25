
/// <summary>
/// ����� �������, ������� ����� ������������ �����
/// </summary>
public class AwardAttackModifaer : Award
{
    /// <summary>
    /// ����������� �����, ������� �� �������� � �������� �������, ������� ����� ������� ������
    /// </summary>
    public AttackModifier AttackModifier { get; private set; }

    public AwardAttackModifaer(string name, AttackModifier attackModifaer)
    {
        TypeName = "����������� �����";
        Name = name;
        AttackModifier = attackModifaer;
    }

}
