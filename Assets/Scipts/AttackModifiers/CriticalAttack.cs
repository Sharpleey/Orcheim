
/// <summary>
/// Класс модификатора атаки, позволяющий наносить критический урон целям
/// </summary>
public class CriticalAttack : ProcableAttackModifaer
{
    public override string Name => "Критическая атака";
    public override string Description => "Атаки имеют возможность нанести критический урон";
    public override int ProcСhance { get; set; } = 10;

    /// <summary>
    /// Множитель урона критической атаки
    /// </summary>
    public float DamageMultiplier { get; set; } = 1.5f;
}
