using System.Collections;
using UnityEngine;
using System.IO;

public class TestAll : MonoBehaviour
{
    public Transform cube;

    float elapsedTime = 0f;
    float targetTime = 3f;

    private void Start()
    {
        //StartCoroutine(MoveCube());
        Debug.Log(cube.localEulerAngles);
        Debug.Log(cube.localRotation);
        Debug.Log(cube.localRotation.eulerAngles);
    }

    //private void Update()
    //{
    //    cube.transform.localPosition = new Vector3(Mathf.Lerp(0f, 10f, elapsedTime / targetTime), 0f, 0f);
    //    elapsedTime += Time.deltaTime;
    //}

    //IEnumerator MoveCube()
    //{
    //    while (elapsedTime <= targetTime)
    //    {
    //        //cube.transform.localPosition = new Vector3(Mathf.Lerp(0f, 10f, elapsedTime / targetTime), 0f, 0f);
    //        cube.transform.localEulerAngles = new Vector3(Mathf.Lerp(0f, 90f, elapsedTime / targetTime), 0f, 0f);
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }
    //}
}