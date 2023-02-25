
/// <summary>
/// Класс награды, награды ввиде модификатора атаки
/// </summary>
public class AwardAttackModifier : Award
{
    /// <summary>
    /// Модификатор атаки, который мы поулчаем в качестве награды, который потмо добавим игроку
    /// </summary>
    public AttackModifier AttackModifier { get; private set; }

    public AwardAttackModifier(AwardType awardType, string name, AttackModifier attackModifier) : base(awardType, name)
    {
        AttackModifier = attackModifier;
    }
}
