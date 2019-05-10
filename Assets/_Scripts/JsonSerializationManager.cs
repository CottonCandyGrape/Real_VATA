using System.IO;
using UnityEngine;


public class JsonSerializationManager : MonoBehaviour
{
    public JointOrientationSetter jointSetter;

    public JsonDoubleArray GetMotionDataForSimulator
    {
        get { return motionDataForSimulator; }
    }
    public JsonDoubleArray GetMotionDataForRobot
    {
        get { return motionDataForRobot; }
    }

    private JsonDoubleArray motionDataForSimulator;
    private JsonDoubleArray motionDataForRobot;

    private readonly string filePath = "Assets/JsonData/";
    //private double targetFrameTime = 0.2;

    public void UpdateMotionDataForSimulator()
    {
        motionDataForSimulator = new JsonDoubleArray();
        motionDataForSimulator.Add(0.2);
        foreach (Joint joint in jointSetter.joints)
        {
            //motionDataForSimulator.Add(joint.angle);
            motionDataForSimulator.Add((double)Mathf.Round((joint.angle * 10)) / 10);
        }
    }

    public void UpdateMotionDataForRobot() //실시간으로 실물로봇에 각도값 보내기
    {
        motionDataForRobot = new JsonDoubleArray();
        motionDataForRobot.Add(0.2);

        for (int i = 3; i < 6; i++) // 실물 모카 왼팔 (시뮬레이터 오른팔)
        {
            double angle = jointSetter.joints[i].angle;
            if (i == 3)
                motionDataForRobot.Add((double)Mathf.Round((float)(angle * 10)) / 10);
            else
                motionDataForRobot.Add(-(double)Mathf.Round((float)(angle * 10)) / 10);
        }

        for (int i = 0; i < 3; i++) // 실물 모카 오른팔 (시뮬레이터 왼팔)
        {
            double angle = jointSetter.joints[i].angle;
            motionDataForRobot.Add(-(double)Mathf.Round((float)(angle * 10)) / 10);
        }

        for (int i = 6; i < 8; i++) // 실물 모카 목
        {
            double angle = jointSetter.joints[i].angle;
            if (i == 7) //tilt 회전 방향이 반대. 30프로 더 회전.
                motionDataForRobot.Add((-(double)Mathf.Round((float)(angle * 10)) / 10) * 1.3);
            else
                motionDataForRobot.Add((double)Mathf.Round((float)(angle * 10)) / 10);
        }

        //사이즈 설정 -클래스 안에 배열 값 크기로 size 변수 설정하는 함수 호출.
        motionDataForRobot.SetSize();
    }

    public string GetJsonStringMotionDataForRobot()
    {
        string jsonString;

        //JSON 문자열 얻기.
        jsonString = JsonUtility.ToJson(motionDataForRobot);

        //파일로 저장.
        //File.WriteAllText(filePath + "TestData.json", jsonString);

        return jsonString;
    }

    //public void UpdateMotionData()
    //{
    //    UpdateMotionDataForSimulator();
    //    UpdateMotionDataForRobot();
    //}
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