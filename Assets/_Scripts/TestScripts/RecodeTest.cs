using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RecodeTest : MonoBehaviour
{
    private string filePath = "Assets/JsonData/";
    //private string readString;

    private float elapsedTime = 0f;
    private float recodeTime = 1f;

    private int motionDataCount;
    private int motionDataIndex; //인덱스 보다는 motionDataCount를 조절??

    private bool recodeSign = false;

    private void Start()
    {
        MotionDataCountUpdate();
    }

    private void Update()
    {
        if (recodeSign) TimerCounter(recodeTime);
    }

    private void MotionDataCountUpdate() //모션파일 개수 체크 후 motionDataCount 초기화
    {
        motionDataCount = GetMotionDataCount();
    }

    private int GetMotionDataCount() //모션데이터 개수 반환
    {
        DirectoryInfo di = new DirectoryInfo("Assets/JsonData/");
        FileInfo[] fi = di.GetFiles("*.json");

        if (fi.Length == 0)
        {
            Debug.Log("모션 데이터 없음");
            return fi.Length;
        }
        else
        {
            //for (int i = 0; i < fi.Length; i++)
            //{
            //    Debug.Log(fi[i].Name);
            //}
            //Debug.Log(fi.Length);
            return fi.Length;
        }
    }

    private void TimerCounter(float recodeTime) //모션파일 만들고 recodeTime 마다 내용추가. 레코드 사인 들어오면 녹화시작
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= recodeTime)
        {
            CreateMotionDataFile("hi, hi, hi");
            elapsedTime = 0f;
            //ReadMotionData("Hello");
        }
    }

    private void CreateMotionDataFile(string jsonString)
    {
        motionDataCount += 1;

        FileStream fs = new FileStream(filePath + "motion_" + motionDataCount + ".json", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(jsonString);

        sw.Close();
        fs.Close();
    }

    //private void UpdateMotinoData(int motionDataIndex, string jsonString) //읽은 후 바꿔야 할 듯
    //{
    //    FileStream fs = new FileStream(filePath + "motion_" + motionDataIndex + ".json", FileMode.Append);
    //    StreamWriter sw = new StreamWriter(fs);

    //    sw.WriteLine(jsonString);

    //    sw.Close();
    //    fs.Close();
    //}

    //private void ReadMotionData(int motionDataIndex)
    //{
    //    FileStream fs = new FileStream(filePath + "motion_" + motionDataIndex + ".json", FileMode.Open);
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
