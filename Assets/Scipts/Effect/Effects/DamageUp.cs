
public class DamageUp : Effect
{
    public override string Name => HashEffectString.DAMAGEUP_NAME;
    public override string Description => HashEffectString.DAMAGEUP_DESCRIPTION;

    /// <summary>
    /// ���������� ���������� �����
    /// </summary>
    public Parameter PercentageDamageIncrease { get; private set; }

    /// <summary>
    /// �������� ��������������� ����� �� ������������� �������� ����� �����
    /// </summary>
    private float _extraDamage;

    public DamageUp(int duration, int defaultPercentageArmorIncrease, int increaseArmorIncreasePerLevel = 0, int level = 1)
    {
        Duration = new Parameter(duration);

        PercentageDamageIncrease = new Parameter(defaultValue: defaultPercentageArmorIncrease, changeValuePerLevel: increaseArmorIncreasePerLevel, level: level);

        Type = EffectType.Positive;
    }

    public override void Enable()
    {
        base.Enable();

        _extraDamage = unit.Damage.Max * (PercentageDamageIncrease.Value / 100f);
        unit.Armor.Actual += _extraDamage;

        // �������� ������ �������
        enemyUnit?.IconEffectsController?.EnableIcon<DamageUp>(true);
    }

    public override void Disable()
    {
        unit.Armor.Actual -= _extraDamage;

        // ��������� ������ �������
        enemyUnit?.IconEffectsController?.EnableIcon<DamageUp>(false);
    }
}
