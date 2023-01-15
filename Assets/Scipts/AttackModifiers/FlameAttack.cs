
/// <summary>
/// ����� ������������ �����, ����������� ��������� ����
/// </summary>
public class FlameAttack : ProcableAttackModifier
{
    public override string Name => "����������� �����";
    public override string Description => $"����� c ������ {�hance.Actual}% ����������� �� ���� ������ {Effect.Name}\n{Effect.Description}";

    

    /// <summary>
    /// ������, ������� ����������� ����������� �� �����
    /// </summary>
    public Flame Effect { get; private set; }

    /// <summary>
    /// ����������� ������ ������������ ����� 
    /// </summary>
    /// <param name="procChance">���� �����, ���� ������������ ������������</param>
    /// <param name="increaseProcChancePerLevel">������� ����� �� ������� ��������� ���������</param>
    /// <param name="levelProcChance">����������� ������� ���������</param>
    /// <param name="damageFlame"></param>
    /// <param name="increaseDamageFlamePerLevel"></param>
    /// <param name="levelDamageFlame"></param>
    /// <param name="armorDecrease"></param>
    /// <param name="increaseArmorDecreasePerLevel"></param>
    /// <param name="levelArmorDecrease"></param>
    /// <param name="durationEffect"></param>
    /// <param name="increaseDurationEffectPerLevel"></param>
    /// <param name="levelDurationEffect"></param>
    public FlameAttack(
        int procChance = 10, int increaseProcChancePerLevel = 5, int levelProcChance = 1, 
        int damageFlame = 10, int increaseDamageFlamePerLevel = 2, int levelDamageFlame = 1, 
        int armorDecrease = 1, int increaseArmorDecreasePerLevel = 1, int levelArmorDecrease = 1, 
        int durationEffect = 3, int increaseDurationEffectPerLevel = 1, int levelDurationEffect = 1)
    {
        �hance = new Proc�hance(defaultValue: procChance, increaseValuePerLevel: increaseProcChancePerLevel, level: levelProcChance);

        Effect = new Flame( //TODO ����������� ����� �������
            damageFlame: damageFlame, increaseDamageFlamePerLevel: increaseDamageFlamePerLevel, levelDamageFlame: levelDamageFlame,
            armorDecrease: armorDecrease, increaseArmorDecreasePerLevel: increaseArmorDecreasePerLevel, levelArmorDecrease: levelArmorDecrease,
            durationEffect: durationEffect, increaseDurationEffectPerLevel: increaseDurationEffectPerLevel, levelDurationEffect: levelDurationEffect);

        �hance.UpgradeDescription = $"���� �������� ������ +{�hance.IncreaseValuePerLevel}% (������� {�hance.Max}%)";
    }
}
