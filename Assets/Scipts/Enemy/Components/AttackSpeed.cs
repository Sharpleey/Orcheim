using UnityEngine;

public class AttackSpeed : IAttackSpeed, IUpgratable
{
    private int _defaultAttackSpeed;
    public int DefaultAttackSpeed
    {
        get => _defaultAttackSpeed;
        private set => _defaultAttackSpeed = Mathf.Clamp(value, 25, 1000);
    }

    private int _maxAttackSpeed;
    public int MaxAttackSpeed
    {
        get => _maxAttackSpeed;
        set => _maxAttackSpeed = Mathf.Clamp(value, _defaultAttackSpeed, 2000);
    }

    private int _actualAttackSpeed;
    public int ActualAttackSpeed
    {
        get => _actualAttackSpeed;
        set => _actualAttackSpeed = Mathf.Clamp(value, 25, _maxAttackSpeed);
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

    public AttackSpeed(int defaultAttackSpeed, float upgradeValue = 0, string description = "", int maxLevel = int.MaxValue, int level = 1)
    {
        DefaultAttackSpeed = defaultAttackSpeed;
        UpgradeValue = upgradeValue;
        DescriptionUpdate = description;
        MaxLevel = maxLevel;

        SetLevel(level);
    }

    public void Upgrade(int levelUp = 1)
    {
        Level += levelUp;

        MaxAttackSpeed = (int)(DefaultAttackSpeed + UpgradeValue * Level);
        ActualAttackSpeed = MaxAttackSpeed;
    }

    public void SetLevel(int level)
    {
        Level = level;

        MaxAttackSpeed = (int)(DefaultAttackSpeed + UpgradeValue * Level);
        ActualAttackSpeed = MaxAttackSpeed;
    }
}
