
/// <summary>
/// Класс награды, награды ввиде модификатора атаки
/// </summary>
public class AwardAttackModifaer : Award
{
    /// <summary>
    /// Модификатор атаки, который мы поулчаем в качестве награды, который потмо добавим игроку
    /// </summary>
    public AttackModifier AttackModifier { get; private set; }

    public AwardAttackModifaer(string name, string description, AttackModifier attackModifaer)
    {
        TypeName = "Модификатор атаки";
        Name = name;
        Description = description;
        AttackModifier = attackModifaer;
    }

}
