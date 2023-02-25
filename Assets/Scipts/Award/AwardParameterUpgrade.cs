
/// <summary>
///  Класс награды, награды ввиде улучшения параметра игрока или параметров модификаторов, способностей
/// </summary>
public class AwardParameterUpgrade : Award
{
    public Upgratable UpgratableParameter { get; private set; }

    public AwardParameterUpgrade(AwardType awardType, string name, Upgratable parameter) : base(awardType, name)
    {
        UpgratableParameter = parameter;
    }
}
