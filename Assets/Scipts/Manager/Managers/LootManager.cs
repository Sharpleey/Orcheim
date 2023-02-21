using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (P.S. In progress)
/// (TODO) Сделать нормальный алгоритм случайных наград.
/// Менеджер отвечает за зоны спавна сундуков с наградами, непосредственно за спавн этих сундуков в случаных местах. 
/// Отвечает за выдачу наград игроку из сундкуков или иных способов, например за получение уровня игроком
/// </summary>
public class LootManager : MonoBehaviour, IGameManager
{
    public static LootManager Instance { get; private set; }

    #region Serialize fields

    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    public List<AwardAttackModifaer> AwardsAttackModifaers { get; private set; } = new List<AwardAttackModifaer>();
    public List<AwardAttackModifierUpgrade> AwardsAttackModifiersUpgrade { get; private set; } = new List<AwardAttackModifierUpgrade>();
    public List<AwardPlayerStatsUpgrade> AwardsPlayerStatsUpgrade { get; private set; } = new List<AwardPlayerStatsUpgrade>();


    #endregion Properties

    #region Private fields

    private GameObject[] _chestSpawnZones;

    #endregion Private fields

    #region Mono

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        AddListeners();
    }

    #endregion Mono

    #region Private methods

    private void AddListeners()
    {
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_NewGame);
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        GlobalGameEventManager.OnGameOver.AddListener(EventHandler_GameOver);
        WaveEventManager.OnWaveIsOver.AddListener(EventHandler_WaveIsOver);
    }

    private void FindChestSpawnZones()
    {
        Debug.Log("Find chest spawn zones...");

        _chestSpawnZones = GameObject.FindGameObjectsWithTag("ChestSpawnZone");

        Debug.Log("Found: " + _chestSpawnZones.Length.ToString() + " zones");
    }

    private void ChestRespawn()
    {
        Debug.Log("Spawn chest");
    }
    #endregion Private methods

    #region Public methods

    public void Startup()
    {
        Debug.Log("Loot manager starting...");

        Status = ManagerStatus.Started;
    }

    public void AddAwardAttackModifier(AttackModifier attackModifaer)
    {
        AwardsAttackModifaers.Add(new AwardAttackModifaer(attackModifaer.Name, attackModifaer));
    }

    public void RemoveAwardAttackModifier(AwardAttackModifaer award)
    {
        AwardsAttackModifaers.Remove(award);
    }

    public void AddAwardAttackModifierUpgrade(string name, Upgratable upgratableParameter)
    {
        AwardsAttackModifiersUpgrade.Add(new AwardAttackModifierUpgrade(name, upgratableParameter));
    }

    public void AddAwardPlayerStatUpgrade(string name, Upgratable upgratableParameter)
    {
        AwardsPlayerStatsUpgrade.Add(new AwardPlayerStatsUpgrade(name, upgratableParameter));
    }

    public AwardAttackModifaer GetRandomAwardAttackModifaer()
    {
        if (AwardsAttackModifaers.Count == 0)
            return null;

        int indexAward = Random.Range(0, AwardsAttackModifaers.Count);
        return AwardsAttackModifaers[indexAward];
    }

    public AwardAttackModifierUpgrade GetRandomAwardAttackModifaerUpgrade()
    {
        if (AwardsAttackModifiersUpgrade.Count == 0)
            return null;

        int indexAward = Random.Range(0, AwardsAttackModifiersUpgrade.Count);
        return AwardsAttackModifiersUpgrade[indexAward];
    }

    public AwardPlayerStatsUpgrade GetRandomAwardPlayerStatsUpgrade()
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

    #endregion Public methods

    #region Event handlers
    private void EventHandler_NewGame(GameMode gameMode)
    {

    }

    private void EventHandler_GameMapStarted()
    {
        FindChestSpawnZones();
        ChestRespawn();
    }

    private void EventHandler_GameOver()
    {
        AwardsAttackModifaers = new List<AwardAttackModifaer>();
        AwardsAttackModifiersUpgrade = new List<AwardAttackModifierUpgrade>();
        AwardsPlayerStatsUpgrade = new List<AwardPlayerStatsUpgrade>();
    }

    private void EventHandler_WaveIsOver(int wave)
    {
        ChestRespawn();
    }

    #endregion
}
