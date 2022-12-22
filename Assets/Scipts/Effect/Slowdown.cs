
public class Slowdown : Effect, IMovementSpeedPercentageDecrease, IAttackSpeedPercentageDecrease
{
    public override string Name => "����������";

    public override string Description => "������ ��������� ��������� ��";

    public override EffectType EffectType => EffectType.Negative;
    public override float Duration { get; set; } = 3;
    public float MovementSpeedPercentageDecrease { get; set; } = 0.2f;
    public float AttackSpeedPercentageDecrease { get; set; } = 0.2f;

    public override void Enable()
    {
        enemy.MovementSpeed.ActualSpeed = enemy.MovementSpeed.MaxSpeed * (1f - MovementSpeedPercentageDecrease);
        enemy.NavMeshAgent.speed = enemy.MovementSpeed.ActualSpeed;

        //enemy.AttackSpeed = enemy.MaxAttackSpeed * (1f - AttackSpeedPercentageDecrease);

        // �������� ������ ���������� ��� �����������
        if (enemy.IconEffectsController != null)
            enemy.IconEffectsController.SetActiveIconSlowdown(true);
    }

    public override void Disable()
    {
        enemy.MovementSpeed.ActualSpeed = enemy.MovementSpeed.MaxSpeed;
        enemy.NavMeshAgent.speed = enemy.MovementSpeed.ActualSpeed;

        //enemy.AttackSpeed = enemy.MaxAttackSpeed;

        // ��������� ������ ���������� ��� �����������
        if (enemy.IconEffectsController != null)
            enemy.IconEffectsController.SetActiveIconSlowdown(false);
    }
}
