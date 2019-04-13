using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAll : MonoBehaviour
{
    A a;

    // Start is called before the first frame update
    void Start()
    {
        a = new A();
        a.num = 1;
        Debug.Log(a.num);

        a = new A();
        Debug.Log(a.num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class A
{
    public int num = 0;
}
