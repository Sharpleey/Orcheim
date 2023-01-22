
/// <summary>
/// ����� ������������ �����, ����������� �������� ����������� ���� �����
/// </summary>
public class CriticalAttack : ProcableAttackModifier
{
    public override string Name => "����������� �����";
    public override string Description => $"����� � ������ {�hance.Value}% ����� ������� {DamageMultiplier.Value}% �����";

    /// <summary>
    /// ��������� ����� ����������� �����
    /// </summary>
    public Parameter DamageMultiplier { get; protected set; }

    public CriticalAttack(bool isActive = false,
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, int maxLevelProcChance = 18,
        int defaultValueDamageMultiplier = 150, int increaseDamageMultiplierPerLevel= 10, int levelDamageMultiplier = 1): base(isActive, procChance, increaseProcChancePerLevel, levelProcChance, maxLevelProcChance)
    {
        DamageMultiplier = new Parameter(defaultValue: defaultValueDamageMultiplier, increaseValuePerLevel: increaseDamageMultiplierPerLevel, level: levelDamageMultiplier);

        �hance.UpgradeDescription = $"���� ������� {DamageMultiplier.Value}% �����  +{�hance.IncreaseValuePerLevel}% (������� {�hance.Value}%)";
        DamageMultiplier.UpgradeDescription = $"��������� ������������ �����  +{DamageMultiplier.IncreaseValuePerLevel}% (������� {DamageMultiplier.Value}%)";
    }
}
