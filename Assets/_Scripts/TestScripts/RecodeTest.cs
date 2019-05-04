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

    void Start()
    {
        //CreateMotionData("Hello", "hi, hi, hi");
        //ReadMotionData("Hello");
        //Debug.Log(GetMotionDataCount());
        motionDataCount = GetMotionDataCount();
    }

    private void Update()
    {
        TimerCounter(recodeTime);
    }

    private int GetMotionDataCount()
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
            Debug.Log(fi.Length);
            return fi.Length;
        }
    }

    //private void recodeStart() //녹화를 시작시키고 모션을 하나 먼저 만든다.
    //{
    //    CreateMotionData();
    //}

    //private void recodeEnd()
    //{

    //}

    private void TimerCounter(float recodeTime) //recodeStart에서 만들어진 모션파일에 recodeTime 마다 내용추가, 녹화 시작 후 호출.
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= recodeTime)
        {
            //CreateMotionData("hi, hi, hi"); //UpdateMotinoData() 호출
            elapsedTime = 0f;
            //ReadMotionData("Hello");
        }
    }

    private void CreateMotionData(string jsonString)
    {
        motionDataCount += 1;

        FileStream fs = new FileStream(filePath + "motion_" + motionDataCount + ".json", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(jsonString);

        sw.Close();
        fs.Close();
    }

    //private void UpdateMotinoData(string motionName, string jsonString)
    //{
    //    FileStream fs = new FileStream(filePath + motionName + ".json", FileMode.Append);
    //    StreamWriter sw = new StreamWriter(fs);

    //    sw.WriteLine(jsonString);

    //    sw.Close();
    //    fs.Close();
    //}

    //private void ReadMotionData(string motionName)
    //{
    //    FileStream fs = new FileStream(filePath + motionName + ".json", FileMode.Open);
    //    StreamReader sr = new StreamReader(fs);

    //    Debug.Log("Read " + motionName + ".json");
    //    while ((readString = sr.ReadLine()) != null)
    //    {
    //        Debug.Log(readString);
    //    }
    //    Debug.Log("END");

    //    sr.Close();
    //    fs.Close();
    //}
}
