using UnityEngine;
using Zenject;

public abstract class PlayerUnit : Unit, IPlayerUnitParameters
{
    #region Properties

    public int Gold { get; protected set; }
    
    private int _experience;
    public int Experience 
    { 
        get => _experience; 
        protected set => _experience = Mathf.Clamp(value, 0, int.MaxValue);
    }
    public int ExperienceForNextLevel => ((Level + 1) - 1) * (((Level + 1) - 2) * GeneralParameter.EXP_COEFF + 200);

    #endregion Properties

    #region Private fields
    private LootManager _lootManager;
    #endregion

    #region Mono

    protected override void Awake()
    {
        base.Awake();

        AddListeners();
    }

    protected override void Start()
    {
        base.Start();

        _lootManager?.PullingPlayerParametersOnPoolAwards(this);
    }

    [Inject]
    private void Construct(LootManager lootManager)
    {
        _lootManager = lootManager;
    }

    #endregion Mono

    #region Private methods
    protected virtual void AddListeners()
    {
        GlobalGameEventManager.OnEnemyKilled.AddListener(EventHandler_EnemyKilled);
        WaveEventManager.OnWaveIsOver.AddListener(EventHandler_WaveIsOver);
    }

    #endregion Private methods

    #region Public methods

    public override void InitParameters()
    {
        base.InitParameters();

        PlayerUnitConfig? playerUnitConfig = _unitConfig as PlayerUnitConfig;

        if (playerUnitConfig != null)
        {
            Gold = playerUnitConfig.Gold;
        }
    }

    public override void TakeDamage(float damage, DamageType damageType, bool isArmorIgnore = false, bool isCriticalHit = false, Collider hitBox = null)
    {
        if (Health.Actual > 0)
        {
            if (!isArmorIgnore)
            {
                // Значение уменьшения урона
                float increaseDamage = 1.0f - (Armor.Actual / (100.0f + Armor.Actual));

                // Уменьшенный урон за счет брони
                damage *= increaseDamage;
            }

            Health.Actual -= damage;

            PlayerEventManager.PlayerDamaged(damage);
            PlayerEventManager.PlayerHealthChanged();
        }

        if (Health.Actual <= 0)
        {
            PlayerEventManager.PlayerDead();
        }
    }

    public override void LevelUp(int levelUp = 1)
    {
        base.LevelUp(levelUp);

        PlayerEventManager.PlayerLevelUp();
    }

    public override void SetAttackModifier(AttackModifier newAttackModifier)
    {
        base.SetAttackModifier(newAttackModifier);

        _lootManager?.PullingAttackModifierParametersOnPoolAwards(newAttackModifier);
        _lootManager?.RemoveAttackModifierFromPoolAwards(newAttackModifier);
    }

    public void AddExperience(int newExp)
    {
        int delta = newExp - (ExperienceForNextLevel - Experience);

        if(delta >= 0)
        {
            Experience = ExperienceForNextLevel;

            LevelUp();

            AddExperience(delta);

        }

        Experience += newExp;

        PlayerEventManager.PlayerExperienceChanged();
    }

    public void AddGold(int gold)
    {
        Gold += gold;

        PlayerEventManager.PlayerGoldChanged();
    }

    #endregion Public methods

    #region Event Handlers

    private void EventHandler_EnemyKilled(EnemyUnit enemyUnit)
    {
        AddExperience((int)enemyUnit.CostInExp.Value);
        AddGold((int)enemyUnit.CostInGold.Value);
    }

    private void EventHandler_WaveIsOver(int wave)
    {
        Health.Actual = Health.Max;

        PlayerEventManager.PlayerHealthChanged();
    }

    #endregion Event Handlers
}
