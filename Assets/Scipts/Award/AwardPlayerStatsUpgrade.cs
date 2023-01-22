

public class AwardPlayerStatsUpgrade : Award
{
    public Upgratable UpgratableParameter { get; private set; }

    public AwardPlayerStatsUpgrade(string name, string description, Upgratable parameter)
    {
        TypeName = "Улучшение характеристик игрока";
        Name = name;
        Description = description;
        UpgratableParameter = parameter;
    }
}
