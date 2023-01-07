
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
        base.Enable();

        unit.MovementSpeed.Actual = (int)(unit.MovementSpeed.Max * (1f - MovementSpeedPercentageDecrease));
        unit.AttackSpeed.Actual = (int)(unit.AttackSpeed.Max * (1f - AttackSpeedPercentageDecrease));

        if (enemyUnit)
        {
            if(enemyUnit.NavMeshAgent)
                enemyUnit.NavMeshAgent.speed = unit.MovementSpeed.Actual / 100f;

            if(enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconSlowdown(true);
        }

        if (playerUnit)
        {
            //TODO ����������� �������� ������� �� ������
        }
    }

    public override void Disable()
    {
        unit.MovementSpeed.Actual = unit.MovementSpeed.Max;
        unit.AttackSpeed.Actual = unit.AttackSpeed.Max;

        if (enemyUnit)
        {
            if (enemyUnit.NavMeshAgent)
                enemyUnit.NavMeshAgent.speed = unit.MovementSpeed.Actual / 100f;

            if (enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconSlowdown(false);

        }

        if (playerUnit)
        {
            //TODO ����������� �������� ������� �� ������
        }
    }
}
