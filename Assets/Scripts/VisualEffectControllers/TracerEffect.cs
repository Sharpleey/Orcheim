using System.Collections;
using UnityEngine;

public class TracerEffect : MonoBehaviour
{
    #region Properties

    public TrailRenderer TrailRenderer { get; private set; }

    #endregion Properties

    #region Mono

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

        PoolManager.Instance?.TracerEffectPool.ReturnToContainerPool(this);
    }
    #endregion Private methods

    #region Public methods

    public void StartCountdownToDelete()
    {
        StartCoroutine(DeleteEffectInDelay(1));
    }

    #endregion Public methods
}
