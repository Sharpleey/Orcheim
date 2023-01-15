
/// <summary>
/// Класс модификатора атаки, позволяющий наносить критический урон целям
/// </summary>
public class CriticalAttack : ProcableAttackModifier
{
    public override string Name => "Критическая атака";
    public override string Description => $"Атаки c шансом {Сhance.Actual}% могут нанести {DamageMultiplier * 100}% урон";

    /// <summary>
    /// Множитель урона критической атаки
    /// </summary>
    public float DamageMultiplier { get; set; } = 1.5f;

    public CriticalAttack()
    {

    }
}
