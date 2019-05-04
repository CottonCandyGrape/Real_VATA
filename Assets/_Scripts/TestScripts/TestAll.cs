using UnityEngine;

public class TestAll : MonoBehaviour
{
    private int num;

    private void Start()
    {
        num = 0;
        Debug.Log(num);
        testFunc(3);
        Debug.Log(num);
    }

    private void testFunc(int figure)
    {
        num = figure;
    }
}
