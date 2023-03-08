using System.Collections;
using UnityEngine;

public class TracerEffect : MonoBehaviour
{
    #region Private methods

    private IEnumerator DeleteEffectInDelay(int secondsBeforeDeletion)
    {
        yield return new WaitForSeconds(secondsBeforeDeletion);

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
