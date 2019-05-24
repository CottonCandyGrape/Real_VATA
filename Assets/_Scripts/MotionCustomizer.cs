using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MotionCustomizer : MonoBehaviour
{
    public CDJointOrientationSetter cdJointSetter;
    //public MoccaPlayer moccaPlayer;
    //public RecordManager recordManager;
    //public PopUpMessege popUpManager;

    public InputField inputField;
    public Slider speedSlider;
    public Slider angleSlider;
    public Text speedText;
    public Text angleText;

    private CDJoint[] cdJoints;

    private string filePath = "Assets/JsonData/";

    private void Start()
    {
        cdJoints = cdJointSetter.joints;
        speedText.text = "Speed X" + speedSlider.value * 0.1;
        angleText.text = "Angle X" + angleSlider.value * 0.1;
    }

    public void SpeedSliderChange()
    {
        speedText.text = "Speed X" + speedSlider.value * 0.1;
    }

    public void AngleSliderChange()
    {
        angleText.text = "Angle X" + angleSlider.value * 0.1;
    }

    public void InitSlider()
    {
        speedSlider.value = 10;
        angleSlider.value = 10;
        SpeedSliderChange();
        AngleSliderChange();
    }

    //private void CustomizedMotionFileAdd() //저장된 모션 데이터(O), 방금 녹화된 모션 데이터(X)
    //{
    //    string fileName = filePath + inputField.text + ".json";
    //    if (!StateUpdater.isRealTimeMode)
    //    {
    //        if (motionFileData != null)
    //        {
    //            if (!File.Exists(fileName))
    //            {
    //                CreateMotionJsonFile(fileName);
    //            }
    //            else
    //            {
    //                popUpManager.MessegePopUp("이미 저장된 이름입니다");
    //            }
    //        }
    //        else
    //        {
    //            popUpManager.MessegePopUp("녹화된 파일이 없습니다");
    //        }
    //        SetDropdownOptions();
    //        inputField.text = string.Empty;
    //    }
    //    else
    //    {
    //        popUpManager.MessegePopUp("실시간 모드가 진행 중 입니다");
    //    }
    //}

    public void CustomizeMotionData(float speed, float range, MotionDataFile motionFileData)
    {
        CustomizeMotionSpeed(speed, motionFileData);
        CustomizeMotionAllAngle(range, motionFileData);
        LimitCustomizedAngle(motionFileData);
    }

    private void LimitCustomizedAngle(MotionDataFile motionFileData) //각도 제한하여 motionFileData에 저장.
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

    private void CustomizeMotionAllAngle(float range, MotionDataFile motionFileData) //각도 편집하기.
    {
        for (int i = 0; i < motionFileData.Length; i++)
        {
            for (int j = 1; j < motionFileData[i].Length; j++)
            {
                motionFileData[i][j] = MathUtil.Roundoff(((float)motionFileData[i][j] * (range * 0.1f)));
            }
        }
    }

    private void CustomizeMotionSpeed(float speed, MotionDataFile motionFileData) //속도 편집하기.
    {
        for (int i = 0; i < motionFileData.Length; i++)
            motionFileData[i][0] *= MathUtil.Roundoff((1f / (speed * 0.1f)));
    }
}
