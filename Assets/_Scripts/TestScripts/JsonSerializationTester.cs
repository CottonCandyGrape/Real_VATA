using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSerializationTester : MonoBehaviour
{
    public JointOrientationSetter jointSetter;

    private readonly string filePath = "Assets/JsonData/";

    private float fps = 1f;
    private float targetFrameTime = 0f;
    private WaitForSeconds frameWait;

    private void Awake()
    {
        targetFrameTime = 1f / fps;
        Debug.Log(targetFrameTime);

        //frameWait = new WaitForSeconds(targetFrameTime);

        //StartCoroutine(DelayedSend());
    }

    void Start()
    {
        string[] topics = new string[]
        {
            Utils.TopicHeader + D2EConstants.TOPIC_TTS,
            Utils.TopicHeader + D2EConstants.TOPIC_MOTION,//모션만 사용 topic = /raw_motion
			Utils.TopicHeader + D2EConstants.TOPIC_MOBILITY,
            Utils.TopicHeader + D2EConstants.TOPIC_FACIAL
        };
        Mqtt.Instance.Connect("52.78.62.151", topics);

        //// 데이터 추가.
        //JsonFloatArray data = new JsonFloatArray();
        //data.Add(0.1f);

        //// joints 배열에 각도 설정.(테스트용).
        //jointSetter.joints[3].angle = 45f;
        //jointSetter.joints[3].UpdateRotation();

        //for (int ix = 0; ix < jointSetter.joints.Length; ++ix)
        //{
        //    float angle = jointSetter.joints[ix].angle;
        //    data.Add(angle);
        //}

        //data.Add(90f);
        //data.Add(90f);
        //data.Add(90f);
        //data.Add(90f);
        //data.Add(90f);
        //data.Add(90f);
        //data.Add(90f);
        //data.Add(90f);

        // 사이즈 설정 - 클래스 안에 배열 값 크기로 size 변수 설정하는 함수 호출.
        //data.SetSize();

        // JSON 문자열 얻기.
        //string jsonString = JsonUtility.ToJson(data);

        // 필요한 형태로 문자열 조합.
        //jsonString = "mot:raw(" + jsonString + ")";

        //Debug.Log(jsonString);

        // 파일로 저장.
        //File.WriteAllText(filePath + "TestData.json", jsonString);
    }

    private void Update()
    {
        Mqtt.Instance.Send("/raw_motion", "123");
    }

    //IEnumerator DelayedSend()
    //{
    //    while (true)
    //    {
    //        yield return frameWait;
    //        Debug.Log("Hello");
    //    }
    //}

    //float elapsedTime = 0f;
    //private void Update()
    //{
    //    elapsedTime += Time.deltaTime;
    //    if (elapsedTime > targetFrameTime)
    //    {
    //        Debug.Log("Hello: " + elapsedTime);
    //        elapsedTime = 0f;
    //    }
    //}
}
