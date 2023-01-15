using UnityEngine;

public abstract class Upgratable
{
    #region Properties

    /// <summary>
    /// Максимальный уровень улучшения параметра
    /// </summary>
    public int MaxLevel
    {
        get => _maxLevel;
        protected set => _maxLevel = Mathf.Clamp(value, 1, int.MaxValue);
    }

    /// <summary>
    /// Текущий уровень параметра
    /// </summary>
    public int Level
    {
        get => _level;
        private set => _level = Mathf.Clamp(value, 1, _maxLevel);
    }

    /// <summary>
    /// Значение прироста параметра за уровень улучшения
    /// </summary>
    public float IncreaseValuePerLevel
    {
        get => _increaseValuePerLevel;
        protected set => _increaseValuePerLevel = Mathf.Clamp(value, 0, float.MaxValue);
    }

    /// <summary>
    /// Описание улучшения параметра
    /// </summary>
    public string UpgradeDescription { get; set; }

    #endregion Properties

    #region Private fields

    private int _maxLevel;
    private int _level;
    private float _increaseValuePerLevel;

    #endregion Private fields

    public Upgratable(int increaseValuePerLevel = 0, int maxLevel = int.MaxValue, int level = 1)
    {
        IncreaseValuePerLevel = increaseValuePerLevel;
        Level = level;
        MaxLevel = maxLevel;
    }


    /// <summary>
    /// Улучшить параметр на уровень или несколько уровней
    /// </summary>
    /// <param name="levelUp">Значение, на сколько уровней улучшить параметр (По умолчанию на 1)</param>
    public virtual void Upgrade(int levelUp = 1)
    {
        Level += levelUp;
    }

    /// <summary>
    /// Установить уровень параметра
    /// </summary>
    /// <param name="newLevel">Уровень, который хотим установить</param>
    public virtual void SetLevel(int newLevel)
    {
        Level = newLevel;
    }
}
