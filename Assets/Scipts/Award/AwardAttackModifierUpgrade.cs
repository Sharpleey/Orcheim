

public class AwardAttackModifierUpgrade : Award
{
    public Upgratable UpgratableParameter { get; private set; }

    public AwardAttackModifierUpgrade(string name, Upgratable parameter)
    {
        TypeName = "��������� ������������ �����";
        Name = name;
        UpgratableParameter = parameter;
    }
}
