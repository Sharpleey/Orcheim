
/// <summary>
/// Абстрактный класс награды
/// </summary>
public abstract class Award
{
    /// <summary>
    /// Типа награды
    /// </summary>
    public AwardType Type { get; protected set; }

    /// <summary>
    /// Название типа награды
    /// </summary>
    public string TypeName { get; protected set; }

    /// <summary>
    /// Название предмета, способности, эффекта или т.п., т.е. то что мы улучшаем
    /// </summary>
    public string Name { get; protected set; }

    public Award(AwardType awardType, string name)
    {
        Type = awardType;
        TypeName = GetNameType(Type);
        Name = name;
    }

    public string GetNameType(AwardType awardType)
    {
        switch(awardType)
        {
            case AwardType.AttackModifaer:
                return HashAwardTypeString.AWARD_ATTACK_MODIFIER_TYPE;
            case AwardType.AttackModifierUpgrade:
                return HashAwardTypeString.AWARD_ATTACK_MODIFIER_UPGRADE_TYPE;
            case AwardType.PlayerStatUpgrade:
                return HashAwardTypeString.AWARD_PLAYER_STAT_UPGRADE_TYPE;
            default:
                return HashAwardTypeString.AWARD_UNKNOWN_TYPE;
        }
            
    }
}
