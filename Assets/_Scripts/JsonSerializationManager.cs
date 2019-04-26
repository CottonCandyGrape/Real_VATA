using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class JsonSerializationManager : MonoBehaviour
{
    public JointOrientationSetter jointSetter;

    private JsonDoubleArray motionData;
    private string jsonString;
    private readonly string filePath = "Assets/JsonData/";

    private float fps = 10f;
    private float targetFrameTime = 0f;
    private float elapsedTime = 0f;

    const string IP = "52.78.62.151";
    const int PORT = 5001;
    UdpClient udpClient = new UdpClient(IP, PORT);

    void Start()
    {
        targetFrameTime = 1f / fps;
    }

    private void Update()
    {
        TimeCounter(targetFrameTime);
    }

    private void TimeCounter(float targetFrameTime)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= targetFrameTime)
        {
            SendJsonStringWithMqtt();
            elapsedTime = 0f;
        }
    }

    private void Send(string rawMotion)
    {
        byte[] data = new byte[1024];
        data = Encoding.UTF8.GetBytes(rawMotion);
        udpClient.Send(data, data.Length);
    }

    private void SendJsonStringWithMqtt()
    {
        UpdateMotionData();
        UpdateJsonString();
        //Send(jsonString);
        //Mqtt.Instance.Send("/raw_motion", jsonString);
    }

    private void UpdateMotionData()
    {
        // 데이터 추가.
        motionData = new JsonDoubleArray();

        motionData.Add(0.1);
        for (int ix = 0; ix < jointSetter.joints.Length; ++ix)
        {
            double angle = jointSetter.joints[ix].angle;
            motionData.Add((double)Mathf.Round((float)(angle * 10)) / 10);
        }

        //사이즈 설정 -클래스 안에 배열 값 크기로 size 변수 설정하는 함수 호출.
        motionData.SetSize();
    }

    private void UpdateJsonString()
    {
        //JSON 문자열 얻기.
        jsonString = JsonUtility.ToJson(motionData);

        //필요한 형태로 문자열 조합.
        jsonString = "mot:raw(" + jsonString + ")\n";

        //파일로 저장.
        File.WriteAllText(filePath + "TestData.json", jsonString);
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