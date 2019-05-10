using UnityEngine;
using System.IO;

public class TestAll : MonoBehaviour
{
    private string filePath = "Assets/JsonData/";

    public MotionDataFile motionFile;

    private void Start()
    {
        string customizedFileName = filePath + "motion_(speed;2).json";
        //string customizedJsonString = JsonUtility.ToJson(motionFile);
        File.WriteAllText(customizedFileName, "asdfkjhasd;k");
    }
}
