using System.Collections;
using System.IO;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public AngleMessenger angleMessenger;
    public JointOrientationSetter jointSetter;
    public MotionDataFile motionFileData;

    private string filePath = "Assets/JsonData/";
    //private int motionDataCount = 0;

    private IEnumerator Start()
    {
        if (!angleMessenger.isRealtimePlayer)
        {
            string fileName = filePath + "motion_1.json";
            string jsonString = File.ReadAllText(fileName);
            motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
        }

        yield return StartCoroutine(SetAngles(motionFileData));
    }

    private float waitTime = 0.2f;
    IEnumerator SetAngles(MotionDataFile motionData)
    {
        //Debug.Log("motionData.Length: " + motionData.Length);

        for (int ix = 0; ix < motionData.Length; ++ix)
        {
            //Debug.Log("jointSetter.joints.Length: " + jointSetter.joints.Length);

            for (int jx = 0; jx < jointSetter.joints.Length; ++jx)
            {
                jointSetter.joints[jx].angle = (float)motionData[ix][jx + 1];
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}