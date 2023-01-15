
/// <summary>
/// ����� �������, ������� ����� ������������ �����
/// </summary>
public class AwardAttackModifaer : Award
{
    /// <summary>
    /// ����������� �����, ������� �� �������� � �������� �������, ������� ����� ������� ������
    /// </summary>
    public AttackModifier AttackModifier { get; private set; }

    public AwardAttackModifaer(string name, string description, AttackModifier attackModifaer)
    {
        TypeName = "����������� �����";
        Name = name;
        Description = description;
        AttackModifier = attackModifaer;
    }

}
