using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    #region Serialize fields

    [SerializeField][Range(0.1f, 1f)] float _secondsBeforeDeletion = 0.15f;

    #endregion Serialize fields

    #region Mono

    private void OnEnable()
    {
        StartCoroutine(DeleteEffect(_secondsBeforeDeletion));
    }

    #endregion Mono

    #region Private methods
    private IEnumerator DeleteEffect(float secondsBeforeDeletion)
    {
        yield return new WaitForSeconds(secondsBeforeDeletion);

        PoolManager.Instance?.HitEffectPool.ReturnToContainerPool(this);
    }
    #endregion Private methods
}
