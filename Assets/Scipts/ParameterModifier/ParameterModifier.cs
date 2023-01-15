
/// <summary>
/// ����� ������������ ��������� �����
/// </summary>
public class ParameterModifier : UpgratableParameter
{
    /// <summary>
    /// ��������, �� ������� ���������� ��� ��� ���� ��������
    /// </summary>
    public virtual float ValueOfModify { get; protected set; }

    /// <summary>
    /// ��� ��������� �������� ���������
    /// </summary>
    public virtual ParameterModifierType ParameterModifierType { get; protected set; }


    public ParameterModifier(float valueOfModify, ParameterModifierType parameterModifierType, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) :base(increaseValuePerLevel, maxLevel, level)
    {
        ValueOfModify = valueOfModify;
        ParameterModifierType = parameterModifierType;

        SetLevel(Level);
    }

    public override void Upgrade(int levelUp = 1)
    {
        base.Upgrade(levelUp);

        ValueOfModify += IncreaseValuePerLevel * (Level - 1);
    }

    public override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        ValueOfModify += IncreaseValuePerLevel * (Level - 1);
    }
}
