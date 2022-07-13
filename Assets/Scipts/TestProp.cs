using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProp : MonoBehaviour
{
    [SerializeField] int _damage = -45;

    public int Damage
    {
        get { return _damage; }
        set 
        { 
            if (value < 0)
            {
                _damage = 0;
                return;
            }
            _damage = value; 
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Damage = _damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Damage);
        }
    }
}

//public class A_Class: MonoBehaviour
//{
//    private TestProp test = new TestProp();

//    private void Awake()
//    {
//        test.Damage = 10;
//    }
//}
