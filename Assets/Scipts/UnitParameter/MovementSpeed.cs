
/// <summary>
/// ����� �������� �������� ������������ �����
/// </summary>
public class MovementSpeed : UnitParameter
{
    public MovementSpeed(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = "�������� ������������";
        UpgradeDescription = "���������� c������� ������������ +{0} (������� �������� {1})";
    }
}
