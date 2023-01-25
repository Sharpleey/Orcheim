
/// <summary>
/// Класс параметр скорости передвижения юнита
/// </summary>
public class MovementSpeed : UnitParameter
{
    public MovementSpeed(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = "Скорость передвижения";
        UpgradeDescription = "Увеличение cкорости передвижения +{0} (Текущее значение {1})";
    }
}
