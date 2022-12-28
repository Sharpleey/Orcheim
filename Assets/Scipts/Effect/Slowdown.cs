
public class Slowdown : Effect, IMovementSpeedPercentageDecrease, IAttackSpeedPercentageDecrease
{
    public override string Name => "Замедление";

    public override string Description => "Эффект замедляет персонажа на";

    public override EffectType EffectType => EffectType.Negative;
    public override float Duration { get; set; } = 3;
    public float MovementSpeedPercentageDecrease { get; set; } = 0.2f;
    public float AttackSpeedPercentageDecrease { get; set; } = 0.2f;

    public override void Enable()
    {
        enemy.MovementSpeed.Actual = (int)(enemy.MovementSpeed.Max * (1f - MovementSpeedPercentageDecrease));
        enemy.NavMeshAgent.speed = enemy.MovementSpeed.Actual/100f;

        //enemy.AttackSpeed = enemy.MaxAttackSpeed * (1f - AttackSpeedPercentageDecrease);

        // Включаем иконку замедления над противником
        if (enemy.IconEffectsController != null)
            enemy.IconEffectsController.SetActiveIconSlowdown(true);
    }

    public override void Disable()
    {
        enemy.MovementSpeed.Actual = enemy.MovementSpeed.Max;
        enemy.NavMeshAgent.speed = enemy.MovementSpeed.Actual/100f;

        //enemy.AttackSpeed = enemy.MaxAttackSpeed;

        // Выключаем иконку замедления над противником
        if (enemy.IconEffectsController != null)
            enemy.IconEffectsController.SetActiveIconSlowdown(false);
    }
}
