using UnityEngine;

public class MovementSpeed : IMovementSpeed, IUpgratable
{
    private float _defaultSpeed;
    public float DefaultSpeed 
    {
        get => _defaultSpeed;
        private set => _defaultSpeed = Mathf.Clamp(value, 0, 10f);
    }

    private float _maxSpeed;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = Mathf.Clamp(value, _defaultSpeed, 10f);
    }

    private float _actualSpeed;
    public float ActualSpeed 
    { 
        get => _actualSpeed; 
        set => _actualSpeed = Mathf.Clamp(value, 0, _maxSpeed);
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

    public MovementSpeed(float defaultSpeed, float upgradeValue = 0, string description = "", int maxLevel = int.MaxValue, int level = 1)
    {
        DefaultSpeed = defaultSpeed;
        UpgradeValue = upgradeValue;
        DescriptionUpdate = description;
        MaxLevel = maxLevel;

        SetLevel(level);
    }
  
    public void Upgrade(int levelUp = 1)
    {
        Level += levelUp;

        MaxSpeed = DefaultSpeed + UpgradeValue * Level;
        ActualSpeed = MaxSpeed;
    }

    public void SetLevel(int level)
    {
        Level = level;

        MaxSpeed = DefaultSpeed + UpgradeValue * Level;
        ActualSpeed = MaxSpeed;
    }
}
