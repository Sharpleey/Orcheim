
/// <summary>
/// Класс модификатора атаки, позволяющий замедлять передвижение и скорость атаки целей
/// </summary>
public class SlowAttack : ProcableAttackModifier
{
    public override string Name => "Замедляющая атака";
    public override string Description => $"Атаки c шансом {Сhance.Value}% накладывают на цель эффект {Effect.Name}\n{Effect.Description}";

    /// <summary>
    /// Эффект, который отвечает за замедление
    /// </summary>
    public Slowdown Effect { get; private set; }

    public SlowAttack(
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1,
        int defaultMovementSpeedPercentageDecrease = 20, int increaseMovementSpeedPercentageDecrease = 5, int levelMovementSpeedPercentageDecrease = 1,
        int defaultAttackSpeedPercentageDecrease = 20, int increaseAttackSpeedPercentageDecrease = 5, int levelAttackSpeedPercentageDecrease = 1,
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        Сhance = new Parameter(defaultValue: procChance, increaseValuePerLevel: increaseProcChancePerLevel, level: levelProcChance, maxLevel: 18);

        Effect = new Slowdown(
            defaultMovementSpeedPercentageDecrease: defaultMovementSpeedPercentageDecrease, increaseMovementSpeedPercentageDecrease: increaseMovementSpeedPercentageDecrease, levelMovementSpeedPercentageDecrease: levelMovementSpeedPercentageDecrease,
            defaultAttackSpeedPercentageDecrease: defaultAttackSpeedPercentageDecrease, increaseAttackSpeedPercentageDecrease: increaseAttackSpeedPercentageDecrease, levelAttackSpeedPercentageDecrease: levelAttackSpeedPercentageDecrease,
            durationEffect: durationEffect, increaseDurationEffectPerLevel: increaseDurationEffectPerLevel, levelDurationEffect: levelDurationEffect);
    }
}
