

public class AwardPlayerStatsUpgrade : Award
{
    public Upgratable UpgratableParameter { get; private set; }

    public AwardPlayerStatsUpgrade(string name, string description, Upgratable parameter)
    {
        TypeName = "��������� ������������� ������";
        Name = name;
        Description = description;
        UpgratableParameter = parameter;
    }
}
