
/// <summary>
/// Класс модификатора атаки, позволяющий поджигать цели
/// </summary>
public class FlameAttack : ProcableAttackModifaer
{
    public override string Name => "Поджигающая атака";
    public override string Description => "Атаки накладывают на цель эффект Flame";
    public override int ProcСhance { get; set; } = 10;

    /// <summary>
    /// ффект, который отвечает за горение
    /// </summary>
    public Flame Effect { get; private set; }

    public FlameAttack()
    {
        Effect = new Flame();
    }
}
