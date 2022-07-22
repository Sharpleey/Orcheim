using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerEffectController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private IEnumerator DeleteEffect(int secondsBeforeDeletion)
    {
        yield return new WaitForSeconds(secondsBeforeDeletion);

        Destroy(gameObject);
    }

    public void DeleteTracer()
    {
        StartCoroutine(DeleteEffect(1));
    }
}
