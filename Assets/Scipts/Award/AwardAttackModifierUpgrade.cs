

public class AwardAttackModifierUpgrade : Award
{
    public UpgratableParameter Parameter { get; private set; }

    public AwardAttackModifierUpgrade(string name, string description, UpgratableParameter parameter)
    {
        TypeName = "��������� ������������ �����";
        Name = name;
        Description = description;
        Parameter = parameter;
    }
}
