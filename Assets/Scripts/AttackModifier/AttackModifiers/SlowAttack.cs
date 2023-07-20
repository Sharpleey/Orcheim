
/// <summary>
///  ласс модификатора атаки, позвол€ющий замедл€ть передвижение и скорость атаки целей
/// </summary>
public class SlowAttack : ProcableAttackModifier
{
    public override string Name => HashAttackModString.SLOW_ATTACK_NAME;
    public override string Description => string.Format(HashAttackModString.SLOW_ATTACK_DESCRIPTION, Chance.Value, Effect.Name, Effect.Description);

    /// <summary>
    /// Ёффект, который отвечает за замедление
    /// </summary>
    public Slowdown Effect { get; private set; }

    public SlowAttack(bool isActive = true,
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, int maxLevelProcChance = 18,
        int defaultMovementSpeedPercentageDecrease = 20, int increaseMovementSpeedPercentageDecrease = 5, int levelMovementSpeedPercentageDecrease = 1,
        int defaultAttackSpeedPercentageDecrease = 20, int increaseAttackSpeedPercentageDecrease = 5, int levelAttackSpeedPercentageDecrease = 1,
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1) : base(isActive, procChance, increaseProcChancePerLevel, levelProcChance, maxLevelProcChance)
    {
        Effect = new Slowdown(
            defaultMovementSpeedPercentageDecrease: defaultMovementSpeedPercentageDecrease, increaseMovementSpeedPercentageDecrease: increaseMovementSpeedPercentageDecrease, levelMovementSpeedPercentageDecrease: levelMovementSpeedPercentageDecrease,
            defaultAttackSpeedPercentageDecrease: defaultAttackSpeedPercentageDecrease, increaseAttackSpeedPercentageDecrease: increaseAttackSpeedPercentageDecrease, levelAttackSpeedPercentageDecrease: levelAttackSpeedPercentageDecrease,
            durationEffect: durationEffect, increaseDurationEffectPerLevel: increaseDurationEffectPerLevel, levelDurationEffect: levelDurationEffect);

        Chance.UpgradeDescription = HashAttackModString.SLOW_ATTACK_CHANCE_UPGRADE_DESCRIPTION;
    }
}
