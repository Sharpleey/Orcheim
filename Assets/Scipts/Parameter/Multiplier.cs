using System;
using UnityEngine;

public class Multiplier : Parameter
{
    public override float Value
    {
        get => _value;
        protected set => _value = (float)Math.Round(Mathf.Clamp(value, 0, int.MaxValue), 2);
    }

    public override float ChangeValuePerLevel
    {
        get => _changeValuePerLevel;
        protected set => _changeValuePerLevel = (float)Math.Round(Mathf.Clamp(value, int.MinValue, int.MaxValue), 2);
    }

    public Multiplier(float defaultValue, float changeValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1) : base(defaultValue, changeValuePerLevel, maxLevel, level)
    {

    }
}
