using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RecordManager : MonoBehaviour
{
    public JsonSerializationManager jsonManager;
    public AngleMessenger angleMessenger;
    public bool recordSign = false;
    public bool signChecker;

    private string filePath = "Assets/JsonData/";

    private float fps = 5f;
    private float recodeTime = 0f;
    private float elapsedTime = 0f;

    private int motionDataCount;

    private MotionDataFile motionFile;

    private void Start()
    {
        recodeTime = 1 / fps;
        signChecker = recordSign;
        MotionDataCountUpdate();
    }

    private void Update()
    {
        if (recordSign && angleMessenger.isRealtimePlayer) //실시간 모드 켜져있고 녹화 사인 들어오면 녹화 시작
            TimerCounter(recodeTime);

        if (!recordSign && signChecker && motionFile != null) //녹화 끝나면 파일 생성
            CreateMotionJsonFile();

        signChecker = recordSign;
    }

    private void CreateMotionJsonFile()
    {
        string fileName = filePath + "motion_" + (motionDataCount + 1) + ".json";
        string jsonString = JsonUtility.ToJson(motionFile, true);
        File.WriteAllText(fileName, jsonString);

        motionFile = null;
        motionDataCount += 1;
    }

    private void CreateOrAddMotionData(DoubleArray motionData)
    {
        if (motionFile == null)
            motionFile = new MotionDataFile();

        motionFile.Add(motionData);
    }

    private void TimerCounter(float recodeTime)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= recodeTime)
        {
            jsonManager.UpdateMotionDataForSimulator();
            CreateOrAddMotionData(jsonManager.GetMotionDataForSimulator);
            elapsedTime = 0f;
        }
    }

    private int GetMotionDataCount() //모션데이터 개수 반환
    {
        DirectoryInfo di = new DirectoryInfo("Assets/JsonData/");
        FileInfo[] fi = di.GetFiles("*.json");

        if (fi.Length == 0) return fi.Length;
        else return fi.Length;
    }

    private void MotionDataCountUpdate() //모션파일 개수 체크 후 motionDataCount 초기화
    {
        motionDataCount = GetMotionDataCount();
    }


    //private void IncMotionDataCount() //녹화 후 motionDataCount + 1
    //{
    //    string fileName = filePath + "motion_" + (motionDataCount + 1) + ".json";

    //    if (File.Exists(fileName) && recordSign == false)
    //        motionDataCount++;
    //}

    //private void CreateMotionDataFile(string jsonString)
    //{
    //    string fileName = filePath + "motion_" + (motionDataCount + 1) + ".json";

    //    FileStream fs = new FileStream(fileName, FileMode.Append);
    //    StreamWriter sw = new StreamWriter(fs);

    //    sw.WriteLine(jsonString);

    //    sw.Close();
    //    fs.Close();
    //}

    //private void ReadMotionData(int motionDataIndex)
    //{
    //    string fileName = filePath + "motion_" + motionDataIndex + ".json";

    //    FileStream fs = new FileStream(fileName, FileMode.Open);
    //    StreamReader sr = new StreamReader(fs);

    //    Debug.Log("Read " + "motion_" + motionDataIndex + ".json");
    //    while ((readString = sr.ReadLine()) != null)
    //    {
    //        Debug.Log(readString);
    //    }
    //    Debug.Log("END");

    //    sr.Close();
    //    fs.Close();
    //}
}
