using UnityEngine;

public class Damage : IUnitDamage, IUpgratable
{
    private int _defaultAvgDamage;
    public int DefaultAvgDamage
    {
        get => _defaultAvgDamage;
        private set => _defaultAvgDamage = Mathf.Clamp(value, 1, int.MaxValue);
    }

    private int _avgDamage;
    public int AvgDamage
    {
        get => _avgDamage;
        set => _avgDamage = Mathf.Clamp(value, _defaultAvgDamage, int.MaxValue);
    }

    private int _actualDamage;
    public int ActualDamage
    {
        get
        {
            int range = (int)(_actualDamage * GeneralParameter.OFFSET_DAMAGE_HEALING);
            return Random.Range(_actualDamage - range, _actualDamage + range);
        }
        private set => _actualDamage = Mathf.Clamp(value, 0, AvgDamage);
    }

    public bool IsArmorIgnore { get; private set; }

    public DamageType DamageType { get; private set; }

    private int _maxLevel;
    public int MaxLevel
    {
        get => _maxLevel;
        private set => _maxLevel = Mathf.Clamp(value, 1, int.MaxValue);
    }

    int _level;
    public int Level
    {
        get => _level;
        set => _level = Mathf.Clamp(value, 1, _maxLevel);
    }

    float _upgradeValue;
    public float UpgradeValue
    {
        get => _upgradeValue;
        set => _upgradeValue = Mathf.Clamp(value, 0, float.MaxValue);
    }

    public string DescriptionUpdate { get; private set; }

    public Damage(int defaultAvgDamage, float upgradeValue, DamageType damageType, bool isArmorIgnore, string description = "", int maxLevel = int.MaxValue, int level = 1)
    {
        DefaultAvgDamage = defaultAvgDamage;
        UpgradeValue = upgradeValue;
        DamageType = damageType;
        IsArmorIgnore = isArmorIgnore;
        DescriptionUpdate = description;
        MaxLevel = maxLevel;

        SetLevel(level);
    }

    public void Upgrade(int levelUp = 1)
    {
        Level += levelUp;

        AvgDamage = DefaultAvgDamage + (int)UpgradeValue * Level;
        ActualDamage = AvgDamage;
    }

    public void SetLevel(int level)
    {
        Level = level;

        AvgDamage = DefaultAvgDamage + (int)UpgradeValue * Level;
        ActualDamage = AvgDamage;
    }
}
