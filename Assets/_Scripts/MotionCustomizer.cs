using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MotionCustomizer : MonoBehaviour
{
    public MotionDataFile motionFileData;

    public string motionFileName;
    public float speed;
    public float angleRange;

    private string filePath = "Assets/JsonData/";

    void Start()
    {
        motionFileData = LoadMotionDataFile(motionFileName);

        //motionFileData = CustomizeMotionSpeed(speed);
        //CreateFileCustomizedSpeed();
        motionFileData = CustomizeMotionAllAngle(angleRange);
        CreateFileCustomizedAngle();
    }

    private MotionDataFile CustomizeMotionAllAngle(float range) //각도 편집하기.
    {
        for (int i = 0; i < motionFileData.Length; i++)
        {
            for (int j = 1; j < motionFileData[i].Length; j++)
            {
                motionFileData[i][j] = (double)Mathf.Round(((float)motionFileData[i][j] * (float)((100 + range) * 0.01)) * 10) / 10;
            }
        }

        Debug.Log("계산 끝");
        return motionFileData;
    }

    private void CreateFileCustomizedAngle() //편집 후 파일 만들기
    {
        string customizedFileName = filePath + motionFileName + "(angle;" + angleRange + ").json";
        string customizedJsonString = JsonUtility.ToJson(motionFileData, true);
        File.WriteAllText(customizedFileName, customizedJsonString);
    }

    private MotionDataFile LoadMotionDataFile(string motionFileName) //편집할 파일 내용 불러와서 motionFileData로 반환.
    {
        string fileName = filePath + motionFileName + ".json";
        string jsonString = File.ReadAllText(fileName);
        motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);

        return motionFileData;
    }

    private MotionDataFile CustomizeMotionSpeed(float speed) //속도 편집하기.
    {
        for (int i = 0; i < motionFileData.Length; i++)
            motionFileData[i][0] *= (1f / speed);

        return motionFileData;
    }

    private void CreateFileCustomizedSpeed() //편집 후 파일 만들기
    {
        string customizedFileName = filePath + motionFileName + "(speed;" + speed + ").json";
        string customizedJsonString = JsonUtility.ToJson(motionFileData, true);
        File.WriteAllText(customizedFileName, customizedJsonString);
    }
}
