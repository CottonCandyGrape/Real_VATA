using System.Collections;
using System.IO;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public AngleMessenger angleMessenger;
    public JointOrientationSetter jointSetter;
    public MotionDataFile motionFileData;
    public SSH ssh;

    public string motionFileName;

    private string filePath = "Assets/JsonData/";
    private float waitTime;

    //private IEnumerator Start()
    void Start()
    {
        if (!angleMessenger.isRealtimePlayer)
        {
            string fileName = filePath + motionFileName + ".json";
            string jsonString = File.ReadAllText(fileName);
            motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
        }

        //if (motionFileData != null)
        //{
        //    yield return StartCoroutine(SetAnglesMOCCA(motionFileData));
        //}

        //ChangeAngleForRobot(motionFileData);
        //yield return StartCoroutine(SendMotionFileDataWithSSH());

    }

    IEnumerator SetAnglesMOCCA(MotionDataFile motionData)
    {
        for (int i = 0; i < motionData.Length; i++)
        {
            for (int j = 0; j < jointSetter.joints.Length; j++)
            {
                jointSetter.joints[j].angle = (float)motionData[i][j + 1];
            }

            waitTime = (float)motionData[i][0];
            yield return new WaitForSeconds(waitTime);
        }
    }

    //double[] tempArray = new double[3];
    double tempAngle;
    private void ChangeAngleForRobot(MotionDataFile motionData) //모션파일 각도값 실물 로봇으로 전송전 로봇에 맞게 매핑
    {
        for (int i = 0; i < motionData.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //tempArray[j] = motionData[i][j + 1]; //시뮬레이터 왼팔 각도들을 임시배열에 저장.
                tempAngle = motionData[i][j + 1]; //시뮬레이터 왼팔 각도들을 변수에 저장.

                if (j == 0)
                    motionData[i][j + 1] = motionData[i][j + 4]; //시뮬레이터 오른팔 각도들을 왼팔에 저장.
                else
                    motionData[i][j + 1] = -motionData[i][j + 4];

                //motionData[i][j + 4] = -tempArray[j]; //임시배열에 있는 왼팔 각도들을 오른팔에 저장.
                motionData[i][j + 4] = -tempAngle; //변수에 있는 왼팔 각도들을 오른팔에 저장.
            }

            motionData[i][8] = MathUtil.Roundoff((float)(-motionData[i][8] * 1.3)); //tilt 회전 방향이 반대. 30프로 더 회전. //소수점 길게 늘어져서 잘라줌.
        }
    }

    private IEnumerator SendMotionFileDataWithSSH() //모션파일 실물 로봇으로 전송
    {
        for (int i = 0; i < motionFileData.Length; i++)
        {
            ssh.Send("mot:raw(" + JsonUtility.ToJson(motionFileData[i]) + ")\n");
            Debug.Log("mot:raw(" + JsonUtility.ToJson(motionFileData[i]) + ")\n");

            yield return new WaitForSeconds((float)motionFileData[i][0]);
        }
    }
}