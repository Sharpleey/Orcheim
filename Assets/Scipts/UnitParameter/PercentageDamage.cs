using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageDamage : Damage
{

    public PercentageDamage(int defaultValue, int increaseValuePerLevel = 0, DamageType damageType = DamageType.Physical, bool isArmorIgnore = false, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, increaseValuePerLevel, damageType, isArmorIgnore, maxLevel, level)
    {
    }
}
