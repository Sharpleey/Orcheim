using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

/// <summary>
/// (P.S. In progress)
/// (TODO) Сделать нормальный алгоритм случайных наград.
/// Отвечает за выдачу наград игроку из сундкуков или иных способов, например за получение уровня игроком
/// </summary>
public class LootManager : MonoBehaviour
{
    #region Properties
    public List<AwardAttackModifier> AwardsAttackModifaers { get; private set; }
    public List<AwardParameterUpgrade> AwardsAttackModifiersUpgrade { get; private set; }
    public List<AwardParameterUpgrade> AwardsPlayerStatsUpgrade { get; private set; }
    public List<Award> Awards { get; private set; }
    #endregion 

    #region Mono
    private void Awake()
    {
        AddListeners();
    }

    private void Start()
    {
        InitAwards();
        CreateAllAwardAttackModifiers();
    }

    #endregion Mono

    #region Private methods
    private void AddListeners()
    {
        GlobalGameEventManager.OnGameOver.AddListener(EventHandler_GameOver);
    }

    private void InitAwards()
    {
        AwardsAttackModifaers = new List<AwardAttackModifier>();
        AwardsAttackModifiersUpgrade = new List<AwardParameterUpgrade>();
        AwardsPlayerStatsUpgrade = new List<AwardParameterUpgrade>();
        Awards = new List<Award>();
    }

    private void CreateAllAwardAttackModifiers()
    {
        AttackModifier attackModifier;

        attackModifier = new CriticalAttack(increaseProcChancePerLevel: 10);
        AwardsAttackModifaers.Add(new AwardAttackModifier(AwardType.AttackModifaer, attackModifier.Name, attackModifier));

        attackModifier = new FlameAttack(increaseProcChancePerLevel: 10, increaseDamageFlamePerLevel: 4, increaseDurationEffectPerLevel: 2);
        AwardsAttackModifaers.Add(new AwardAttackModifier(AwardType.AttackModifaer, attackModifier.Name, attackModifier));

        attackModifier = new SlowAttack(increaseProcChancePerLevel: 10, increaseAttackSpeedPercentageDecrease: 10, increaseMovementSpeedPercentageDecrease: 10, increaseDurationEffectPerLevel: 3, durationEffect: 5);
        AwardsAttackModifaers.Add(new AwardAttackModifier(AwardType.AttackModifaer, attackModifier.Name, attackModifier));

        attackModifier = new PenetrationProjectile(decreasePenetrationDamageDecreasePerLevel: -10, maxLevelPenetrationDamageDecrease: 5);
        AwardsAttackModifaers.Add(new AwardAttackModifier(AwardType.AttackModifaer, attackModifier.Name, attackModifier));
    }

    #endregion Private methods

    #region Public methods

    public void RemoveAwardAttackModifier(AwardAttackModifier award)
    {
        AwardsAttackModifaers.Remove(award);
    }

    public void RemoveAwardAttackModifier<T>() where T: AttackModifier
    {
        foreach(AwardAttackModifier awardAttackModifier in AwardsAttackModifaers)
        {
            if (awardAttackModifier.AttackModifier.GetType() == typeof(T))
            {
                RemoveAwardAttackModifier(awardAttackModifier);
                break;
            }
        }
    }

    public void RemoveAttackModifierFromPoolAwards(AttackModifier attackModifier)
    {
        foreach (AwardAttackModifier awardAttackModifier in AwardsAttackModifaers)
        {
            if (awardAttackModifier.AttackModifier.GetType() == attackModifier.GetType())
            {
                RemoveAwardAttackModifier(awardAttackModifier);
                break;
            }
        }
    }

    public void CreateAwardAttackModifierUpgrade(string name, Upgratable upgratableParameter)
    {
        AwardsAttackModifiersUpgrade.Add(new AwardParameterUpgrade(AwardType.AttackModifierUpgrade, name, upgratableParameter));
    }

    public void CreateAwardPlayerStatUpgrade(string name, Upgratable upgratableParameter)
    {
        AwardsPlayerStatsUpgrade.Add(new AwardParameterUpgrade(AwardType.PlayerStatUpgrade, name, upgratableParameter));
    }

    public AwardAttackModifier GetRandomAwardAttackModifaer()
    {
        if (AwardsAttackModifaers.Count == 0)
            return null;

        int indexAward = Random.Range(0, AwardsAttackModifaers.Count);
        return AwardsAttackModifaers[indexAward];
    }

    public AwardParameterUpgrade GetRandomAwardAttackModifaerUpgrade()
    {
        if (AwardsAttackModifiersUpgrade.Count == 0)
            return null;

        int indexAward = Random.Range(0, AwardsAttackModifiersUpgrade.Count);
        return AwardsAttackModifiersUpgrade[indexAward];
    }

    public AwardParameterUpgrade GetRandomAwardPlayerStatsUpgrade()
    {
        if (AwardsPlayerStatsUpgrade.Count == 0)
            return null;

        int indexAward = Random.Range(0, AwardsPlayerStatsUpgrade.Count);
        return AwardsPlayerStatsUpgrade[indexAward];
    }

    // TODO Переделать это дерьмо
    /// <summary>
    /// Метод возвращает случайную награду
    /// </summary>
    /// <returns></returns>
    public Award GetRandomAward()
    {
        Award award = null;

        while(award == null)
        {
            int indexTypeAward = Random.Range(1, 4);

            switch (indexTypeAward)
            {
                case 1:
                    award = GetRandomAwardAttackModifaer();
                    break;
                case 2:
                    award = GetRandomAwardAttackModifaerUpgrade();
                    break;
                case 3:
                    award = GetRandomAwardPlayerStatsUpgrade();
                    break;

            }
        }

        return award;
    }

    /// <summary>
    /// Получить n-ое кол-во случайных наград
    /// </summary>
    /// <param name="countAward">Кол-во наград</param>
    /// <returns></returns>
    public List<Award> GetListRandomAwards(int countAward)
    {
        List<Award> awards = new List<Award>();

        while(awards.Count != countAward)
        {
            Award award = GetRandomAward();

            if (!awards.Contains(award))
                awards.Add(award);
        }

        return awards;
    }

    /// <summary>
    /// Добваляет параметры игрока в пул наград
    /// </summary>
    /// <param name="playerUnit">Объект юнита игрока</param>
    public void PullingPlayerParametersOnPoolAwards(PlayerUnit playerUnit)
    {
        CreateAwardPlayerStatUpgrade(playerUnit.Health.Name, playerUnit.Health);
        CreateAwardPlayerStatUpgrade(playerUnit.Armor.Name, playerUnit.Armor);
        CreateAwardPlayerStatUpgrade(playerUnit.Damage.Name, playerUnit.Damage);
        CreateAwardPlayerStatUpgrade(playerUnit.MovementSpeed.Name, playerUnit.MovementSpeed);
        CreateAwardPlayerStatUpgrade(playerUnit.AttackSpeed.Name, playerUnit.AttackSpeed);
    }
    
    public void PullingAttackModifierParametersOnPoolAwards(AttackModifier attackModifier)
    {
        if (attackModifier is CriticalAttack criticalAttack)
        {
            CreateAwardAttackModifierUpgrade(criticalAttack.Name, criticalAttack.Chance);
            CreateAwardAttackModifierUpgrade(criticalAttack.Name, criticalAttack.DamageMultiplier);

            return;
        }

        if (attackModifier is FlameAttack flameAttack)
        {
            CreateAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Chance);
            CreateAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.DamagePerSecond);
            CreateAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.Duration);
            CreateAwardAttackModifierUpgrade(flameAttack.Name, flameAttack.Effect.ArmorDecrease);

            return;
        }

        if (attackModifier is SlowAttack slowAttack)
        {
            CreateAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Chance);
            CreateAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.MovementSpeedPercentageDecrease);
            CreateAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.AttackSpeedPercentageDecrease);
            CreateAwardAttackModifierUpgrade(slowAttack.Name, slowAttack.Effect.Duration);

            return;
        }

        if (attackModifier is PenetrationProjectile penetrationProjectile)
        {
            CreateAwardAttackModifierUpgrade(penetrationProjectile.Name, penetrationProjectile.MaxPenetrationCount);
            CreateAwardAttackModifierUpgrade(penetrationProjectile.Name, penetrationProjectile.PenetrationDamageDecrease);

            return;
        }
    }

    #endregion Public methods

    #region Event handlers

    private void EventHandler_GameOver()
    {
        InitAwards();
        CreateAllAwardAttackModifiers();
    }

    #endregion
}
