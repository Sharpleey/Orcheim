using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerEffectController : MonoBehaviour
{
    #region Mono
    private void Start()
    {
        gameObject.SetActive(false);
    }
    #endregion Mono

    #region Private methods
    private IEnumerator DeleteEffect(int secondsBeforeDeletion)
    {
        yield return new WaitForSeconds(secondsBeforeDeletion);

        Destroy(gameObject);
    }
    #endregion Private methods

    #region Public methods
    public void DeleteTracer()
    {
        StartCoroutine(DeleteEffect(1));
    }
    #endregion Public methods
}
