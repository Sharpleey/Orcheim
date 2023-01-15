
/// <summary>
///  ласс модификатора атаки, позвол€ющий замедл€ть передвижение и скорость атаки целей
/// </summary>
public class SlowAttack : ProcableAttackModifier
{
    public override string Name => "«амедл€юща€ атака";
    public override string Description => "јтаки накладывают на цель эффект Slowdown";

    /// <summary>
    /// Ёффект, который отвечает за замедление
    /// </summary>
    public Slowdown Effect { get; private set; }

    public SlowAttack()
    {
        Effect = new Slowdown();
    }
}
