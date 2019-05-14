using System.Collections;
using System.IO;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public AngleMessenger angleMessenger;
    public JointOrientationSetter jointSetter;
    public MotionDataFile motionFileData;

    public string motionFileName;

    private string filePath = "Assets/JsonData/";
    private float waitTime;

    private IEnumerator Start()
    {
        if (!angleMessenger.isRealtimePlayer)
        {
            string fileName = filePath + motionFileName + ".json";
            string jsonString = File.ReadAllText(fileName);
            motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
        }
        if (motionFileData != null)
        {
            //for (int i = 0; i < 5; i++)
            yield return StartCoroutine(SetAnglesMOCCA(motionFileData));
        }
    }

    IEnumerator SetAnglesMOCCA(MotionDataFile motionData)
    {
        for (int ix = 0; ix < motionData.Length; ++ix)
        {
            for (int jx = 0; jx < jointSetter.joints.Length; ++jx)
            {
                jointSetter.joints[jx].angle = (float)motionData[ix][jx + 1];
            }

            waitTime = (float)motionData[ix][0];
            yield return new WaitForSeconds(waitTime);
        }
    }
}