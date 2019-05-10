using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MotionCustomizer : MonoBehaviour
{
    public MotionDataFile motionFileData;

    public string motionFileName;
    public float speed;

    private string filePath = "Assets/JsonData/";

    void Start()
    {
        motionFileData = LoadMotionDataFile(motionFileName);

        motionFileData = CustomizeMotionSpeed(speed);
        CreateFileCustomizedSpeed();
    }

    private MotionDataFile LoadMotionDataFile(string motionFileName)//편집할 파일 내용 불러와서 motionFileData로 반환.
    {
        string fileName = filePath + motionFileName + ".json";
        string jsonString = File.ReadAllText(fileName);
        motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);

        return motionFileData;
    }

    private MotionDataFile CustomizeMotionSpeed(float speed)//속도 편집하기 편집하기.
    {
        for (int i = 0; i < motionFileData.Length; i++)
            motionFileData[i][0] *= (1f / speed);

        return motionFileData;
    }

    private void CreateFileCustomizedSpeed()//편집 후 파일 만들기
    {
        string customizedFileName = filePath + motionFileName + "(speed;" + speed + ").json";
        string customizedJsonString = JsonUtility.ToJson(motionFileData);
        File.WriteAllText(customizedFileName, customizedJsonString);
    }
}
