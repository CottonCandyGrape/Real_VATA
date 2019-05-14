using System.Collections;
using System.IO;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public AngleMessenger angleMessenger;
    public JointOrientationSetter jointSetter;
    public CDJointOrientationSetter cdJointSetter;
    public MotionDataFile motionFileData;
    //public bool isPlaying = false;

    public string motionFileName;

    private string filePath = "Assets/JsonData/";
    private float waitTime;
    //private int motionDataCount = 0;

    private IEnumerator Start()
    {
        if (!angleMessenger.isRealtimePlayer)
        {
            string fileName = filePath + motionFileName + ".json";
            string jsonString = File.ReadAllText(fileName);
            motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
        }
        if (motionFileName != null)
        {
            //for (int i = 0; i < 5; i++)
            //yield return StartCoroutine(SetAnglesMOCCA(motionFileData));
            yield return StartCoroutine(SetAnglesCDMOCCA(motionFileData));
        }
    }

    //private void Update()
    //{
    //    if (isPlaying)
    //    {
    //        PlayMotion(motionFileData);
    //    }
    //}

    //private IEnumerator PlayMotion(MotionDataFile motionFileData)
    //{
    //    if (motionFileName != null)
    //    {
    //        yield return StartCoroutine(SetAngles(motionFileData));
    //    }
    //}

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

    IEnumerator SetAnglesCDMOCCA(MotionDataFile motionData)
    {
        for (int ix = 0; ix < motionData.Length; ++ix)
        {
            for (int jx = 0; jx < cdJointSetter.joints.Length; ++jx)
            {
                cdJointSetter.joints[jx].angle = (float)motionData[ix][jx + 1];
            }

            waitTime = (float)motionData[ix][0];
            yield return new WaitForSeconds(waitTime);
        }
    }
}