using UnityEngine;

/// <summary>
/// Класс параметр скорости атаки юнита
/// </summary>
public class AttackSpeed : UnitParameter
{
    public override float Default
    {
        protected set => _default = Mathf.Round(Mathf.Clamp(value, 25, 1000));
    }

    public override float Max
    {
        protected set => _max = Mathf.Round(Mathf.Clamp(value, _default, 2000));
    }

    public override float Actual
    {
        set => _actual = Mathf.Round(Mathf.Clamp(value, 25, _max));
    }

    public AttackSpeed(float defaultValue, float increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, maxLevel, level)
    {
        Name = HashUnitParameterString.ATTACK_SPEED;
        UpgradeDescription = HashUnitParameterString.ATTACK_SPEED_UPGRADE_DESCRIPTION;
    }
}
