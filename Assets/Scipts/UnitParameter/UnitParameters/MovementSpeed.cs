
/// <summary>
/// ����� �������� �������� ������������ �����
/// </summary>
public class MovementSpeed : UnitParameter
{
    public MovementSpeed(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = HashUnitParameterString.MOVEMENT_SPEED;
        UpgradeDescription = HashUnitParameterString.MOVEMENT_SPEED_UPGRADE_DESCRIPTION;
    }
}
