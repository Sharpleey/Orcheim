using System.Collections;
using UnityEngine;
using Zenject;

public class HitEffect : MonoBehaviour
{
    #region Serialize fields

    [Header("Время показа")]
    [SerializeField][Range(0.1f, 1f)] float _secondsBeforeDeletion = 0.15f;

    #endregion Serialize fields

    #region Private fields
    private Pool<HitEffect> _hitEffectPool;
    #endregion

    #region Mono

    [Inject]
    private void Construct(Pool<HitEffect> pool)
    {
        _hitEffectPool = pool;
    }

    private void OnEnable()
    {
        StartCoroutine(StartCountdownToDelete(_secondsBeforeDeletion));
    }

    #endregion Mono

    #region Private methods
    private IEnumerator StartCountdownToDelete(float secondsBeforeDeletion)
    {
        yield return new WaitForSeconds(secondsBeforeDeletion);

        _hitEffectPool?.ReturnToContainerPool(this);
    }
    #endregion Private methods
}
