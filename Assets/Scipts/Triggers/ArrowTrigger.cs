using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    [SerializeField] private GameObject arrow;

    private Rigidbody arrowRb;
    // Start is called before the first frame update
    void Start()
    {
        arrowRb = arrow.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        arrowRb.isKinematic = true;
    }
}
