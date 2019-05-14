using System.Collections;
using UnityEngine;
using System.IO;

public class TestAll : MonoBehaviour
{
    private IEnumerator Start()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return StartCoroutine(RotateCube());
        }
    }

    private void Update()
    {
        Debug.Log(transform.localRotation);
    }

    IEnumerator RotateCube()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, i * 90, 0));

            yield return new WaitForSeconds(0.5f);
        }
    }
}