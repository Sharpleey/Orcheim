using UnityEngine;

/// <summary>
/// Класс параметр скорости атаки юнита
/// </summary>
public class AttackSpeed : UnitParameter
{
    public override int Default
    {
        protected set => _default = Mathf.Clamp(value, 25, 1000);
    }

    public override int Max
    {
        protected set => _max = Mathf.Clamp(value, _default, 2000);
    }

    public override int Actual
    {
        set => _actual = Mathf.Clamp(value, 25, _max);
    }

    public AttackSpeed(int defaultValue, int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = "Скорость атаки";
        UpgradeDescription = "Увеличение cкорости атаки +{0} (Текущее значение {1})";
    }
}
