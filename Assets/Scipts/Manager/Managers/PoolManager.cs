using System;
using UnityEngine;

public class PoolManager : MonoBehaviour, IGameManager
{
    public static PoolManager Instance { get; private set; }

    #region Serialize fields

    [Header("Popup Damage")]
    [SerializeField] private PopupDamage _prefabPopupDamage;
    [SerializeField] private int _sizePoolPopupDamage;

    [Header("ProjectileArrow")]
    [SerializeField] private ProjectileArrow _prefabProjectileArrow;
    [SerializeField] private int _sizePoolProjectileArrow;

    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    public Pool<PopupDamage> PopupDamagePool { get; set; }
    public Pool<ProjectileArrow> ProjectileArrowPool { get; set; }

    #endregion Properties

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

    private void Start()
    {
        PopupDamagePool = new Pool<PopupDamage>(_prefabPopupDamage, _sizePoolPopupDamage, CreateAndGetContainer(_prefabPopupDamage.GetType()));
        //ProjectileArrowPool = new Pool<ProjectileArrow>(_prefabProjectileArrow, _sizePoolProjectileArrow, CreateAndGetContainer(_prefabProjectileArrow.GetType()));
    }

    #endregion Mono

    #region Private methods

    private void AddListeners()
    {
        GlobalGameEventManager.OnNewGame.AddListener(EventHandler_OnNewGame);
    }

    private Transform CreateAndGetContainer(Type typeNameContainer)
    {
        GameObject container = new GameObject(name: $"{typeNameContainer}Pool");
        container.transform.SetParent(transform);

        return container.transform;
    }

    #endregion Private methods

    #region Public methods

    public void Startup()
    {
        Debug.Log("Pool Managaer manager starting...");

        Status = ManagerStatus.Started;
    }

    private void EventHandler_OnNewGame(GameMode gameMode)
    {
        PopupDamagePool.RefillPool(_sizePoolPopupDamage);
    }

    #endregion Public methods
}
