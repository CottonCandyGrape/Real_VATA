using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSerializationManager : MonoBehaviour
{
    public JointOrientationSetter jointSetter;

    private JsonFloatArray motionData;
    private string jsonString;
    private readonly string filePath = "Assets/JsonData/";

    private float fps = 1f;
    private float targetFrameTime = 0f;
    private float elapsedTime = 0f;

    void Start()
    {
        targetFrameTime = 1f / fps;

        string[] topics = new string[] //subscribe할 토픽들
        {
             Utils.TopicHeader + D2EConstants.TOPIC_TTS,
             Utils.TopicHeader + D2EConstants.TOPIC_MOTION, //모션만 사용 topic = /raw_motion (publisher에서 쓴다.)
             Utils.TopicHeader + D2EConstants.TOPIC_MOBILITY,
             Utils.TopicHeader + D2EConstants.TOPIC_FACIAL
        };
        Mqtt.Instance.Connect("52.78.62.151", topics);
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

    private void SendJsonStringWithMqtt()
    {
        UpdateMotionData();
        UpdateJsonString();
        Mqtt.Instance.Send("/raw_motion", jsonString);
    }

    private void UpdateMotionData()
    {
        // 데이터 추가.
        motionData = new JsonFloatArray();

        motionData.Add(0.1f);
        for (int ix = 0; ix < jointSetter.joints.Length; ++ix)
        {
            float angle = jointSetter.joints[ix].angle;
            motionData.Add(angle);
        }

        //사이즈 설정 -클래스 안에 배열 값 크기로 size 변수 설정하는 함수 호출.
        motionData.SetSize();
    }

    private void UpdateJsonString()
    {
        //JSON 문자열 얻기.
        jsonString = JsonUtility.ToJson(motionData);

        //필요한 형태로 문자열 조합.
        jsonString = "mot:raw(" + jsonString + ")";

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