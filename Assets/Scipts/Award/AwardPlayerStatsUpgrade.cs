

public class AwardPlayerStatsUpgrade : Award
{
    public Upgratable UpgratableParameter { get; private set; }

    public AwardPlayerStatsUpgrade(string name, Upgratable parameter)
    {
        TypeName = "Улучшение характеристик игрока";
        Name = name;
        UpgratableParameter = parameter;
    }
}
