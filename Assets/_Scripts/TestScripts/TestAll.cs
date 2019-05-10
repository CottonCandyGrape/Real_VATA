using UnityEngine;
using System.IO;

public class TestAll : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            switch (i)
            {
                case 0:
                case 2:
                case 4:
                case 6:
                case 8: Debug.Log("짝수" + i); break;

                case 1:
                case 3:
                case 5:
                case 7:
                case 9: Debug.Log("홀수" + i); break;

            }
        }
    }
}
