

public class AwardAttackModifierUpgrade : Award
{
    public Upgratable UpgratableParameter { get; private set; }

    public AwardAttackModifierUpgrade(string name, string description, Upgratable parameter)
    {
        TypeName = "��������� ������������ �����";
        Name = name;
        Description = description;
        UpgratableParameter = parameter;
    }
}
