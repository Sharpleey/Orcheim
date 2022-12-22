
using UnityEngine;

public class Health : IHealth, IUpgratable
{
    private int _defaultHealth;
    public int DefaultHealth
    {
        get => _defaultHealth;
        set => _defaultHealth = Mathf.Clamp(value, 1, int.MaxValue);
    }

    private int _maxHealth;
    public int MaxHealth 
    {
        get => _maxHealth;
        private set => _maxHealth = Mathf.Clamp(value, _defaultHealth, int.MaxValue);
    }

    private int _actualHealth;
    public int ActualHealth 
    {
        get => _actualHealth;
        set => _actualHealth = Mathf.Clamp(value, 0, _maxHealth);
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

    public string DescriptionUpdate { get; private set; }

    public Health(int defaultHealth, float upgradeValue, string description = "", int maxLevel = int.MaxValue, int level = 1)
    {
        DefaultHealth = defaultHealth;
        UpgradeValue = upgradeValue;
        DescriptionUpdate = description;
        MaxLevel = maxLevel;

        SetLevel(level);
    }

    public void Upgrade(int levelUp = 1)
    {
        Level += levelUp;

        MaxHealth = DefaultHealth + (int)UpgradeValue * Level;
        ActualHealth = MaxHealth;
    }

    public void SetLevel(int level)
    {
        Level = level;

        MaxHealth = DefaultHealth + (int)UpgradeValue * Level;
        ActualHealth = MaxHealth;
    }
}
