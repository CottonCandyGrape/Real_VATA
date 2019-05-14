using System.Collections;
using UnityEngine;
using System.IO;

public class TestAll : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return StartCoroutine(RotateCube());
    }

    IEnumerator RotateCube()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.localEulerAngles = new Vector3(0, i * 90, 0);

            yield return new WaitForSeconds(0.5f);
        }



    }
}
