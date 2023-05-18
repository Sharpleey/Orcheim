using System;
using UnityEngine;

public class PoolManager : MonoBehaviour, IGameManager
{
    public static PoolManager Instance { get; private set; }

    #region Serialize fields

    [Header("Popup Damage")]
    [SerializeField] private PopupDamage _prefabPopupDamage;
    [SerializeField] private int _startSizePoolPopupDamage;

    [Header("Projectile Arrow")]
    [SerializeField] public ProjectileArrow _prefabProjectileArrow;
    [SerializeField] private int _startSizePoolProjectileArrow;

    [Header("TracerEffect")]
    [SerializeField] private TracerEffect _prefabTracerEffect;
    [SerializeField] private int _startSizePoolTracerEffect;

    [Header("HitEffect")]
    [SerializeField] private HitEffect _prefabHitEffect;
    [SerializeField] private int _startSizePoolHitEffect;

    #endregion Serialize fields

    #region Properties

    public ManagerStatus Status { get; private set; }

    public Pool<PopupDamage> PopupDamagePool { get; private set; }
    public Pool<ProjectileArrow> ProjectileArrowPool { get; private set; }
    public Pool<TracerEffect> TracerEffectPool { get; private set; }
    public Pool<HitEffect> HitEffectPool { get; private set; }

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
        PopupDamagePool = new Pool<PopupDamage>(_prefabPopupDamage, _startSizePoolPopupDamage, CreateAndGetContainer(_prefabPopupDamage.GetType()));
        ProjectileArrowPool = new Pool<ProjectileArrow>(_prefabProjectileArrow, _startSizePoolProjectileArrow, CreateAndGetContainer(_prefabProjectileArrow.GetType()));
        TracerEffectPool = new Pool<TracerEffect>(_prefabTracerEffect, _startSizePoolTracerEffect, CreateAndGetContainer(_prefabTracerEffect.GetType()));
        HitEffectPool = new Pool<HitEffect>(_prefabHitEffect, _startSizePoolHitEffect, CreateAndGetContainer(_prefabHitEffect.GetType()));

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
        PopupDamagePool.RefillPool(_startSizePoolPopupDamage);
        ProjectileArrowPool.RefillPool(_startSizePoolProjectileArrow);
        TracerEffectPool.RefillPool(_startSizePoolTracerEffect);
        HitEffectPool.RefillPool(_startSizePoolHitEffect);
    }

    #endregion Public methods
}
