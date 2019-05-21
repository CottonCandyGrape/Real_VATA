using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RecordPlayer : MonoBehaviour
{
    public CDJointOrientationSetter cdJointSetter;
    public SSH ssh;
    public Toggle realTimeModeToggle;
    public InputField inputField;

    private MotionDataFile motionFileData;
    private CDJoint[] cdJoints;

    private string filePath = "Assets/JsonData/";

    void Start()
    {
        cdJoints = cdJointSetter.joints;
    }

    public void PlayMotionFileForSimulator()
    {
        if (!StateUpdater.isRealTimeMode)
        {
            string fileName = filePath + inputField.text + ".json";
            string jsonString = File.ReadAllText(fileName);
            motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
            StartCoroutine(PalyMotionFile(motionFileData));
        }
        else
            Debug.Log("실시간 모드가 진행 중 입니다.");
    }

    IEnumerator PalyMotionFile(MotionDataFile motionData)
    {
        StateUpdater.isMotionDataPlaying = true;
        for (int i = 0; i < motionData.Length; i++)
        {
            float rotDuration = (float)motionData[i][0];
            for (int j = 0; j < cdJoints.Length; j++)
            {
                StartCoroutine(cdJoints[j].SetQuatLerp((float)motionData[i][j + 1], rotDuration));
            }

            yield return new WaitForSeconds((float)motionData[i][0]);
        }
        StateUpdater.isMotionDataPlaying = false;
    }

    public void PlayMotionFileForRobot()
    {
        if (!StateUpdater.isRealTimeMode)
        {
            if (inputField.text == string.Empty)
                Debug.Log("모션파일을 선택해 주세요");
            else
            {
                string fileName = filePath + inputField.text + ".json";
                string jsonString = File.ReadAllText(fileName);
                motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
                ChangeAngleForRobot(motionFileData);
                StartCoroutine(SendMotionFileDataWithSSH());
            }
        }
        else if (StateUpdater.isRealTimeMode)
            Debug.Log("실시간 모드가 진행 중 입니다.");
    }

    public void RealTimeModeToggle()
    {
        if (realTimeModeToggle.isOn)
            StateUpdater.isRealTimeMode = true;
        else
            StateUpdater.isRealTimeMode = false;
    }

    private double ConvertAngle(double WrongAngle) //큰각을 음각으로 반환
    {
        if (WrongAngle > 180.0)
            return WrongAngle - 360.0;
        else
            return WrongAngle;
    }

    double tempAngle;
    private void ChangeAngleForRobot(MotionDataFile motionData) //모션파일 각도값 실물 로봇으로 전송전 로봇에 맞게 매핑
    {
        for (int i = 0; i < motionData.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tempAngle = ConvertAngle(motionData[i][j + 1]); //시뮬레이터 왼팔 각도들을 변수에 저장.

                if (j == 0)
                    motionData[i][j + 1] = ConvertAngle(motionData[i][j + 4]); //시뮬레이터 오른팔 각도들을 왼팔에 저장.
                else
                    motionData[i][j + 1] = -ConvertAngle(motionData[i][j + 4]);

                motionData[i][j + 4] = -tempAngle; //변수에 있는 왼팔 각도들을 오른팔에 저장.
            }

            motionData[i][8] = MathUtil.Roundoff((float)(-ConvertAngle(motionData[i][8]) * 1.3)); //tilt 회전 방향이 반대. 30프로 더 회전. //소수점 길게 늘어져서 잘라줌.
        }
    }

    private IEnumerator SendMotionFileDataWithSSH() //모션파일 실물 로봇으로 전송
    {
        for (int i = 0; i < motionFileData.Length; i++)
        {
            ssh.Send("mot:raw(" + JsonUtility.ToJson(motionFileData[i]) + ")\n");

            yield return new WaitForSeconds((float)motionFileData[i][0]);
        }
    }
}