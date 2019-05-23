using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TestAll : MonoBehaviour
{
    public Text alertMessage;

    private IEnumerator Start()
    {
        alertMessage.gameObject.SetActive(false);
        StartCoroutine(alert("안녕하세요"));
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(alert("저는 정호입니다."));
    }

    IEnumerator alert(string message)
    {
        yield return new WaitForSeconds(0.5f);
        alertMessage.gameObject.SetActive(true);
        alertMessage.text = message;
        yield return new WaitForSeconds(1.5f);
        alertMessage.CrossFadeAlpha(0, 1.5f, true);
        yield return new WaitForSeconds(1.5f);
        alertMessage.gameObject.SetActive(false);
    }
}


