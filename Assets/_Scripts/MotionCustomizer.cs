using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MotionCustomizer : MonoBehaviour
{
    public MotionDataFile motionFileData;
    public CDJointOrientationSetter cdJointSetter;

    public string motionFileName;
    public float speed;
    public float angleRange;

    private string filePath = "Assets/JsonData/";
    private CDJoint[] cdJoints;

    private void Start()
    {
        cdJoints = cdJointSetter.joints;

        LoadMotionDataFile(motionFileName);

        //CustomizeMotionSpeed(speed);
        //CreateFileCustomizedSpeed();

        //CustomizeMotionAllAngle(angleRange);
        //LimitCustomizedAngle();
        //CreateFileCustomizedAngle();

        //PlayForCollisionDetect(); //AngleMessenger isRealTimePlayer 켜져있을때 작동 X.
    }

    //void SendAngleToNeck()
    //{
    //    if (CollisionManager.neckMove)
    //    {
    //        for (int i = 7; i < 9; i++)

    //    }
    //}

    //void SendAngleToRightArm()
    //{
    //    if (CollisionManager.rightArmMove) //시뮬레이터 오른팔
    //    {
    //        for (int i = 4; i < 7; i++)

    //    }
    //}

    //void SendAngleToLeftArm()
    //{
    //    if (CollisionManager.leftArmMove)//시뮬레이터 왼팔
    //    {
    //        for (int i = 1; i < 4; i++)

    //    }
    //}

    //private IEnumerator PlayForCollisionDetect()
    //{
    //    yield return StartCoroutine(SetAnglesCDMOCCA(motionFileData));
    //}

    private void PlayForCollisionDetect()
    {
        StartCoroutine(SetAnglesCDMOCCA(motionFileData));
    }

    IEnumerator SetAnglesCDMOCCA(MotionDataFile motionData)
    {
        for (int i = 0; i < motionData.Length; i++)
        {
            for (int j = 0; j < cdJointSetter.joints.Length; j++)
            {
                cdJointSetter.joints[j].angle = (float)motionData[i][j + 1];
            }

            yield return new WaitForSeconds((float)motionData[i][0]);
        }
    }

    private void LoadMotionDataFile(string motionFileName) //편집할 파일 내용 불러와서 motionFileData로 반환.
    {
        string fileName = filePath + motionFileName + ".json";
        string jsonString = File.ReadAllText(fileName);
        motionFileData = JsonUtility.FromJson<MotionDataFile>(jsonString);
    }

    private void LimitCustomizedAngle() //각도 제한하기
    {
        for (int i = 0; i < motionFileData.Length; i++)
        {
            for (int j = 1; j < motionFileData[i].Length; j++)
            {
                switch (j)
                {
                    case 1:
                    case 4:
                    case 7: motionFileData[i][j] = MathUtil.Roundoff(Mathf.Clamp((float)motionFileData[i][j], -90.0f, 90.0f)); break;
                    case 8: motionFileData[i][j] = MathUtil.Roundoff(Mathf.Clamp((float)motionFileData[i][j], -30.0f, 15.0f)); break;

                    case 5:
                    case 6: motionFileData[i][j] = MathUtil.Roundoff(Mathf.Clamp((float)motionFileData[i][j], -90.0f, 0.0f)); break;

                    case 2:
                    case 3: motionFileData[i][j] = MathUtil.Roundoff(Mathf.Clamp((float)motionFileData[i][j], 0.0f, 90.0f)); break;
                }
            }
        }
    }

    private void CustomizeMotionAllAngle(float range) //각도 편집하기.
    {
        for (int i = 0; i < motionFileData.Length; i++)
        {
            for (int j = 1; j < motionFileData[i].Length; j++)
            {
                motionFileData[i][j] = MathUtil.Roundoff(((float)motionFileData[i][j] * (float)((100 + range) * 0.01)));
            }
        }
    }

    private void CustomizeMotionSpeed(float speed) //속도 편집하기.
    {
        for (int i = 0; i < motionFileData.Length; i++)
            motionFileData[i][0] *= (1f / speed);
    }

    private void CreateFileCustomizedAngle() //편집 후 파일 만들기(각도)
    {
        string customizedFileName = filePath + motionFileName + "(angle;" + angleRange + ").json";
        string customizedJsonString = JsonUtility.ToJson(motionFileData, true);
        File.WriteAllText(customizedFileName, customizedJsonString);
    }

    private void CreateFileCustomizedSpeed() //편집 후 파일 만들기(속도)
    {
        string customizedFileName = filePath + motionFileName + "(speed;" + speed + ").json";
        string customizedJsonString = JsonUtility.ToJson(motionFileData, true);
        File.WriteAllText(customizedFileName, customizedJsonString);
    }
}
