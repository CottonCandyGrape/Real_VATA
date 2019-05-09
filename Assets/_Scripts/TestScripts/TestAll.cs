using UnityEngine;
using System.IO;

public class TestAll : MonoBehaviour
{
    private string filePath = "Assets/JsonData/";

    public MotionDataFile motionFile;

    private void Start()
    {
        if (File.Exists(filePath + "motion_1.json"))
        {
            string jsonStr = File.ReadAllText(filePath + "motion_1.json");
            Debug.Log(jsonStr);

            motionFile = JsonUtility.FromJson<MotionDataFile>(jsonStr);

            //JsonDoubleArray jsonDoubleArray = JsonUtility.FromJson<JsonDoubleArray>(jsonStr);
            //Debug.Log(jsonDoubleArray);
            //Debug.Log(jsonDoubleArray[1]);
            //Debug.Log(jsonDoubleArray);
        }
    }

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
