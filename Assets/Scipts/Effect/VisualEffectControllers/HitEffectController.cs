using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField][Range(0.1f, 1f)] float _secondsBeforeDeletion = 0.15f;
    #endregion Serialize fields

    #region Mono
    private void Awake()
    {
        
    }
    private void Start()
    {
        StartCoroutine(DeleteEffect(_secondsBeforeDeletion));
    }
    #endregion Mono

    #region Private methods
    private IEnumerator DeleteEffect(float secondsBeforeDeletion)
    {
        yield return new WaitForSeconds(secondsBeforeDeletion);

        Destroy(gameObject);
    }
    #endregion Private methods
}
