using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_CONST : MonoBehaviour
{
    public string NAME => string.Format(HashAttackModString.CRITICAL_ATTACK_DESCRIPTION, Chance, Multiply);

    public int Chance { get; set; } = 10;
    public int Multiply { get; set; } = 150;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Chance += 10;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Multiply += 25;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(NAME);
        }
    }
}
