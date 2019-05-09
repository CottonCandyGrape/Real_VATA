﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RecodeManager : MonoBehaviour
{
    public JsonSerializationManager jsonManager;
    public bool recodeSign = false;

    private string filePath = "Assets/JsonData/";
    private string readString;

    private float fps = 5f;
    private float recodeTime = 0f;
    private float elapsedTime = 0f;

    private int motionDataCount;

    private void Start()
    {
        recodeTime = 1 / fps;
        MotionDataCountUpdate();
        ReadMotionData(1);
    }

    private void Update()
    {
        if (recodeSign) TimerCounter(recodeTime);
        IncMotionDataCount();
    }

    private void MotionDataCountUpdate() //모션파일 개수 체크 후 motionDataCount 초기화
    {
        motionDataCount = GetMotionDataCount();
    }

    private void IncMotionDataCount() //녹화 후 motionDataCount + 1
    {
        string fileName = filePath + "motion_" + (motionDataCount + 1) + ".json";

        if (File.Exists(fileName) && recodeSign == false)
            motionDataCount++;
    }

    private int GetMotionDataCount() //모션데이터 개수 반환
    {
        DirectoryInfo di = new DirectoryInfo("Assets/JsonData/");
        FileInfo[] fi = di.GetFiles("*.json");

        if (fi.Length == 0) return fi.Length;
        else return fi.Length;
    }

    private void TimerCounter(float recodeTime) //모션파일 만들고 recodeTime 마다 내용추가. 레코드 사인 들어오면 녹화시작
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= recodeTime)
        {
            CreateMotionDataFile(jsonManager.UpdateJsonString());
            elapsedTime = 0f;
        }
    }

    private void CreateMotionDataFile(string jsonString)
    {
        string fileName = filePath + "motion_" + (motionDataCount + 1) + ".json";

        FileStream fs = new FileStream(fileName, FileMode.Append);
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(jsonString);

        sw.Close();
        fs.Close();
    }

    private void ReadMotionData(int motionDataIndex)
    {
        string fileName = filePath + "motion_" + motionDataIndex + ".json";

        FileStream fs = new FileStream(fileName, FileMode.Open);
        StreamReader sr = new StreamReader(fs);

        Debug.Log("Read " + "motion_" + motionDataIndex + ".json");
        while ((readString = sr.ReadLine()) != null)
        {
            Debug.Log(readString);
        }
        Debug.Log("END");

        sr.Close();
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


}