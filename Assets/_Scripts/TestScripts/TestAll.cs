using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TestAll : MonoBehaviour
{
    //public Transform cube;
    public Image image;

    float elapsedTime = 0f;
    float targetTime = 3f;

    private void Start()
    {
        //StartCoroutine(MoveCube());
        //Debug.Log(image.color.);
        StartCoroutine(BlinkImage());
    }

    IEnumerator BlinkImage()
    {
        while (true)
        {
            image.CrossFadeAlpha(0, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
            image.CrossFadeAlpha(1f, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void Update()
    {
        //cube.transform.localPosition = new Vector3(Mathf.Lerp(0f, 10f, elapsedTime / targetTime), 0f, 0f);
        //elapsedTime += Time.deltaTime;
    }

    //IEnumerator MoveCube()
    //{
    //    while (elapsedTime <= targetTime)
    //    {
    //        //cube.transform.localPosition = new Vector3(Mathf.Lerp(0f, 10f, elapsedTime / targetTime), 0f, 0f);
    //        cube.transform.localEulerAngles = new Vector3(Mathf.Lerp(0f, 90f, elapsedTime / targetTime), 0f, 0f);
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }
}

