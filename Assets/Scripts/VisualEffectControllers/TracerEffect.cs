using System.Collections;
using UnityEngine;
using Zenject;

public class TracerEffect : MonoBehaviour
{
    #region Properties
    public TrailRenderer TrailRenderer { get; private set; }
    #endregion

    #region Private fields
    private Pool<TracerEffect> _tracerEffectPool;
    #endregion

    #region Mono

    [Inject]
    private void Construct(Pool<TracerEffect> pool)
    {
        _tracerEffectPool = pool;
    }

    private void Awake()
    {
        TrailRenderer = GetComponent<TrailRenderer>();
    }

    #endregion Mono

    #region Private methods

    private IEnumerator DeleteEffectInDelay(int secondsBeforeDeletion)
    {
        yield return new WaitForSeconds(secondsBeforeDeletion);

        TrailRenderer.enabled = false;

        _tracerEffectPool?.ReturnToContainerPool(this);
    }
    #endregion Private methods

    #region Public methods

    public void StartCountdownToDelete()
    {
        StartCoroutine(DeleteEffectInDelay(1));
    }

    #endregion Public methods
}
