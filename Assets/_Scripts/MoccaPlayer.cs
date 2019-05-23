using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MoccaPlayer : MonoBehaviour
{
    public CDJointOrientationSetter cdJointSetter;
    public JsonSerializationManager jsonManager;
    public MotionCustomizer motionCustomizer;
    public PopUpMessege popUpManager;

    public SSH ssh;
    public Toggle realTimeModeToggle;
    public InputField inputField;

    private MotionDataFile motionFileData;
    private CDJoint[] cdJoints;

    private string filePath = "Assets/JsonData/";

    private float fps = 5f;
    private float targetFrameTime = 0f;
    private float elapsedTime = 0f;

    void Start()
    {
        targetFrameTime = 1f / fps;
        cdJoints = cdJointSetter.joints;
    }

    void Update()
    {
        if (StateUpdater.isRealTimeMode)
            SendAngleRealTimeToRobot(); //실시간으로 실물로봇으로 보낼때 
    }

    private void SendAngleRealTimeToRobot()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= targetFrameTime)
        {
            SendMotionDataWithSSH();
            elapsedTime = 0f;
        }
    }

    private void SendMotionDataWithSSH()
    {
        jsonManager.UpdateMotionDataForRobot();
        Send("mot:raw(" + jsonManager.GetJsonStringMotionDataForRobot() + ")\n"); //실시간으로 실물에 보낼때 포맷
    }

    private void Send(string rawMotion)
    {
        byte[] data = new byte[1024];
        data = Encoding.UTF8.GetBytes(rawMotion);
        ssh.udpClient.Send(data, data.Length);
    }

    public void PlayMotionFileForSimulator() //시뮬레이터에서 저장된 모션 실행하기 
    {
        if (!StateUpdater.isRealTimeMode)
        {
            if (!StateUpdater.isMotionPlayingSimulator)
            {
                if (inputField.text != string.Empty)
                {
                    LoadMotionFileData();
                    motionCustomizer.CustomizeMotionData(motionCustomizer.speedSlider.value, motionCustomizer.angleSlider.value, motionFileData);
                    StartCoroutine(PalyMotionFile(motionFileData));
                }
                else
                {
                    popUpManager.MessegePopUp("실행할 모션파일을 선택해 주세요");
                }
            }
            else
            {
                popUpManager.MessegePopUp("현재 모션이 실행 중 입니다");
            }
        }
        else
        {
            popUpManager.MessegePopUp("실시간 모드가 진행 중 입니다");
        }
    }

    IEnumerator PalyMotionFile(MotionDataFile motionFileData) //저장된 모션 보간하기.
    {
        StateUpdater.isMotionPlayingSimulator = true;
        WaitForSeconds lerfTime = new WaitForSeconds((float)motionFileData[0][0]);

        for (int i = 0; i < motionFileData.Length; i++)
        {
            float rotDuration = (float)motionFileData[i][0];
            for (int j = 0; j < cdJoints.Length; j++)
            {
                StartCoroutine(cdJoints[j].SetQuatLerp((float)motionFileData[i][j + 1], rotDuration));
            }

            yield return lerfTime;
        }

        StartCoroutine(SetZeroPos());
        StateUpdater.isMotionPlayingSimulator = false;
    }

    IEnumerator SetZeroPos() //보간하며 기본자세 취함.
    {
        float maxDegree = GetMaxDegreeToZeroPos();
        float rotDuration = maxDegree / (360f * 0.8f);
        WaitForSeconds rotDurationSec = new WaitForSeconds(rotDuration);

        for (int i = 0; i < cdJoints.Length; i++)
        {
            StartCoroutine(cdJoints[i].SetQuatLerp(0f, rotDuration));
        }

        yield return rotDurationSec;
    }

    private float GetMaxDegreeToZeroPos() //현재 자세에서 zeropos까지 가장 큰 각을 구함.
    {
        float maxDegree = 0f;
        for (int i = 0; i < cdJoints.Length; i++)
        {
            float degree = MathUtil.ConvertAngle(cdJoints[i].GetCurrentAngle);
            maxDegree = Mathf.Max(maxDegree, degree);
        }
        return maxDegree;
    }

    public void PlayMotionFileForRobot() //로봇에서 저장된 모션 실행하기 
    {
        if (!StateUpdater.isRealTimeMode)
        {
            if (!StateUpdater.isMotionPlayingRobot)
            {
                if (inputField.text != string.Empty)
                {
                    LoadMotionFileData(); //이거 때문에 시뮬레이터에서 play 한것도 원래 데이터로 돌아간다.
                    ChangeAngleForRobot(motionFileData);
                    StartCoroutine(SendMotionFileDataWithSSH());
                }
                else
                {
                    popUpManager.MessegePopUp("실행할 모션파일을 선택해 주세요");
                }
            }
            else
            {
                popUpManager.MessegePopUp("현재 로봇이 모션을 실행 중 입니다");
            }
        }
        else if (StateUpdater.isRealTimeMode)
        {
            popUpManager.MessegePopUp("실시간 모드가 진행 중 입니다");
        }
    }

    private void ChangeAngleForRobot(MotionDataFile motionData) //모션파일 각도값 실물 로봇으로 전송전 로봇에 맞게 매핑
    {
        double tempAngle;
        for (int i = 0; i < motionData.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tempAngle = motionData[i][j + 1]; //시뮬레이터 왼팔 각도들을 변수에 저장.

                if (j == 0)
                    motionData[i][j + 1] = motionData[i][j + 4]; //시뮬레이터 오른팔 각도들을 왼팔에 저장.
                else
                    motionData[i][j + 1] = -motionData[i][j + 4];

                motionData[i][j + 4] = -tempAngle; //변수에 있는 왼팔 각도들을 오른팔에 저장.
            }

            motionData[i][8] = MathUtil.Roundoff((float)(-motionData[i][8] * 1.3)); //tilt 회전 방향이 반대. 30프로 더 회전. //소수점 길게 늘어져서 잘라줌.
        }
    }

    private IEnumerator SendMotionFileDataWithSSH() //모션파일 실물 로봇으로 전송
    {
        StateUpdater.isMotionPlayingRobot = true;
        WaitForSeconds SSHTime = new WaitForSeconds((float)motionFileData[0][0]);

        for (int i = 0; i < motionFileData.Length; i++)
        {
            Send("mot:raw(" + JsonUtility.ToJson(motionFileData[i]) + ")\n");
            yield return SSHTime;
        }
        StateUpdater.isMotionPlayingRobot = false;
    }

    private void LoadMotionFileData()
    {
        string fileName = filePath + inputField.text + ".json";
        string jsonString = File.ReadAllText(fileName);
        motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
    }

    public void RealTimeModeToggle() //실시간 모드 토글
    {
        if (!StateUpdater.isRecording)
        {
            if (realTimeModeToggle.isOn)
                StateUpdater.isRealTimeMode = true;
            else
                StateUpdater.isRealTimeMode = false;
        }
    }
}