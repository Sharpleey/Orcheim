
using UnityEngine;

public class Armor : IArmor, IUpgratable
{
    private int _defaultArmor;
    public int DefaultArmor
    {
        get => _defaultArmor;
        private set => _defaultArmor = Mathf.Clamp(value, 0, int.MaxValue);
    }

    private int _maxArmor;
    public int MaxArmor
    {
        get => _maxArmor;
        private set => _maxArmor = Mathf.Clamp(value, _defaultArmor, int.MaxValue);
    }

    private int _actualArmor;
    public int ActualArmor
    {
        get => _actualArmor;
        set => _actualArmor = Mathf.Clamp(value, 0, _maxArmor);
    }

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

    public string DescriptionUpdate {get; private set;}

    public Armor(int defaultArmor, float upgradeValue, string description = "", int maxLevel = int.MaxValue, int level = 1)
    {
        DefaultArmor = defaultArmor;
        UpgradeValue = upgradeValue;
        DescriptionUpdate = description;
        MaxLevel = maxLevel;

        SetLevel(level);
    }

    public void Upgrade(int levelUp = 1)
    {
        Level += levelUp;

        MaxArmor = DefaultArmor + (int)UpgradeValue * Level;
        ActualArmor = MaxArmor;
    }

    public void SetLevel(int level)
    {
        Level = level;

        MaxArmor = DefaultArmor + (int)UpgradeValue * Level;
        ActualArmor = MaxArmor;
    }
}
