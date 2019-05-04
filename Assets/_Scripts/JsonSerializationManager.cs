using System.IO;
using UnityEngine;


public class JsonSerializationManager : MonoBehaviour
{
    public JointOrientationSetter jointSetter;

    private JsonDoubleArray motionData;
    //private double targetFrameTime = 0.2;

    private readonly string filePath = "Assets/JsonData/";

    public void UpdateMotionData()  
    {
        // 데이터 추가.
        motionData = new JsonDoubleArray();

        motionData.Add(0.2);        

        for (int i = 3; i < 6; i++) // 실물 모카 왼팔
        {
            double angle = jointSetter.joints[i].angle;
            if (i == 3)
                motionData.Add((double)Mathf.Round((float)(angle * 10)) / 10);
            else
                motionData.Add(-(double)Mathf.Round((float)(angle * 10)) / 10);
        }

        for (int i = 0; i < 3; i++) // 실물 모카 오른팔
        {
            double angle = jointSetter.joints[i].angle;
            motionData.Add(-(double)Mathf.Round((float)(angle * 10)) / 10);
        }

        for (int i = 6; i < 8; i++) // 실물 모카 목
        {
            double angle = jointSetter.joints[i].angle;
            if (i == 7)
                motionData.Add((-(double)Mathf.Round((float)(angle * 10)) / 10) * 1.3);
            else
                motionData.Add((double)Mathf.Round((float)(angle * 10)) / 10);
        }

        //사이즈 설정 -클래스 안에 배열 값 크기로 size 변수 설정하는 함수 호출.
        motionData.SetSize();
    }

    public string UpdateJsonString()
    {
        string jsonString;

        //JSON 문자열 얻기.
        jsonString = JsonUtility.ToJson(motionData);

        //필요한 형태로 문자열 조합.
        jsonString = "mot:raw(" + jsonString + ")\n";

        //파일로 저장.
        File.WriteAllText(filePath + "TestData.json", jsonString);

        return jsonString;
    }
}


//private WaitForSeconds frameWait;

//private void Awake()
//{
//    frameWait = new WaitForSeconds(targetFrameTime);
//    StartCoroutine(DelayedSend());
//}

//IEnumerator DelayedSend()
//{
//    while (true)
//    {
//        yield return frameWait;
//        Debug.Log("Hello");
//    }
//}

//for (int ix = 0; ix < jointSetter.joints.Length; ++ix)
//{
//    double angle = jointSetter.joints[ix].angle;
//    if (ix == 3 || ix == 7)
//    {
//        motionData.Add(-(double)Mathf.Round((float)(angle * 10)) / 10);
//    }
//    else
//    {
//        motionData.Add((double)Mathf.Round((float)(angle * 10)) / 10);
//    }
//}