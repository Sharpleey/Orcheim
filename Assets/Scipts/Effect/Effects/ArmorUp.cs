
/// <summary>
/// ������ ����������� ����� ����� � ��������� �� ��� ������������� �������� ����� (�� 50% � ��������� 12 ���.)
/// </summary>
public class ArmorUp : Effect
{
    public override string Name => HashEffectString.ARMORUP_NAME;

    public override string Description => HashEffectString.ARMORUP_DESCRIPTION;

    /// <summary>
    /// ���������� ���������� �����
    /// </summary>
    public Parameter PercentageArmorIncrease { get; private set; }

    /// <summary>
    /// �������� �������������� ����� �� ������������� �������� ����� �����
    /// </summary>
    private float _extraArmor;

    public ArmorUp(int duration, int defaultPercentageArmorIncrease, int increaseArmorIncreasePerLevel = 0, int level = 1)
    {
        Duration = new Parameter(duration);

        PercentageArmorIncrease = new Parameter(defaultValue: defaultPercentageArmorIncrease, changeValuePerLevel: increaseArmorIncreasePerLevel, level: level);

        EffectType = EffectType.Positive;
    }

    public override void Enable()
    {
        base.Enable();

        _extraArmor = unit.Armor.Max * (PercentageArmorIncrease.Value / 100f);
        unit.Armor.Actual += _extraArmor;

        // �������� ������ �������
        enemyUnit?.IconEffectsController?.EnableIcon<ArmorUp>(true);
    }

    public override void Disable()
    {
        unit.Armor.Actual -= _extraArmor;

        // ��������� ������ �������
        enemyUnit?.IconEffectsController?.EnableIcon<ArmorUp>(false);
    }
}
