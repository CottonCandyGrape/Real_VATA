using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
//using static UnityEngine.UI.Dropdown;

public class RecordManager : MonoBehaviour
{
    public Button recStartButton;
    public Button recStopButton;
    public InputField inputField;
    public Dropdown dropdown;
    public Image recordImage;

    public JsonSerializationManager jsonManager;

    private MotionDataFile motionFile;
    private DirectoryInfo directoryInfo;
    private FileInfo[] fileInfo;
    private WaitForSeconds delayRecordTime;

    private float fps = 5f;
    private float recordTime = 0f;

    private string filePath = "Assets/JsonData/";

    private void Start()
    {
        recordTime = 1 / fps;
        directoryInfo = new DirectoryInfo(filePath);
        delayRecordTime = new WaitForSeconds(recordTime);

        recStopButton.gameObject.SetActive(false);
        recordImage.gameObject.SetActive(false);

        SetDropdownOptions();

        //dropdown.onValueChanged.AddListener(OnDropdownChanged);
        //yield return null;
    }

    private void SetDropdownOptions() //드롭다운 목록 초기화
    {
        dropdown.ClearOptions();
        fileInfo = directoryInfo.GetFiles("*.json");
        for (int i = 0; i < fileInfo.Length; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = fileInfo[i].Name.Substring(0, fileInfo[i].Name.Length - 5);
            dropdown.options.Add(option);
        }
        dropdown.captionText.text = "Select Motion data File";
    }

    //public void OnDropdownChanged(int value)
    //{
    //    Debug.Log("OnDropdownChanged: " + value);
    //    //dropdown.value = -1;
    //}

    //IEnumerator SetValueToMinusOne()
    //{
    //    yield return null;
    //    //dropdown.value = -1;
    //}

    public void ChangedDropdownOption() //드롭다운 선택 옵션 바뀌었을 때 
    {
        inputField.text = dropdown.options[dropdown.value].text;
        //dropdown.captionText.text = "Select Motion data File";
    }

    public void ClickedAddButton() //파일이름 중복 체크하여 모션 데이터 추가
    {
        string fileName = filePath + inputField.text + ".json";
        if (!File.Exists(fileName) && motionFile != null)
        {
            Debug.Log("사용가능한 이름입니다.");
            CreateMotionJsonFile(fileName);
            Debug.Log("모션이 저장되었습니다.");
        }
        else
        {
            Debug.Log("이미 존재하는 이름이거나 녹화된 파일이 없습니다.");
        }
        SetDropdownOptions();
        inputField.text = string.Empty;
    }

    public void ClickedDeleteButton() //존재하는 파일인지 체크 후 모션 데이터 삭제
    {
        string fileName = filePath + inputField.text + ".json";
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
            Debug.Log("파일을 삭제합니다.");
        }
        else
        {
            Debug.Log("존재하지 않는 파일입니다.");
        }
        SetDropdownOptions();
        inputField.text = string.Empty;
    }

    public void ClickedStartButton() //녹화시작 버튼
    {
        //Debug.Log("ClickedStartButton");
        if (StateUpdater.isConnectingKinect)
        {
            if (StateUpdater.isRealTimeMode)
            {
                if (inputField.text != string.Empty)
                {
                    ToggleRecordButton();
                    StateUpdater.isRecording = true;
                    StartCoroutine("Recording");

                    recordImage.gameObject.SetActive(true);
                    StartCoroutine("Flicker");

                    Debug.Log("녹화를 시작합니다.");
                }
                else
                    Debug.Log("모션의 이름을 정해주세요");
            }
            else
                Debug.Log("실시간 모드를 실행 해주세요.");
        }
        else
            Debug.Log("Kinect가 연결되어 있지 않습니다.");
    }

    public void ClickedStopButton() //녹화 끝 버튼
    {
        //Debug.Log("ClickedStopButton");
        if (StateUpdater.isRecording)
        {
            ToggleRecordButton();
            StateUpdater.isRecording = false;
            StopCoroutine("Recording");

            recordImage.gameObject.SetActive(false);
            StopCoroutine("Flicker");

            Debug.Log("녹화를 종료합니다.");
        }
    }

    public void ToggleRecordButton() //녹화버튼 토글
    {
        if (!recStopButton.gameObject.activeSelf)
        {
            recStopButton.gameObject.SetActive(true);
            recStartButton.gameObject.SetActive(false);
        }
        else
        {
            recStartButton.gameObject.SetActive(true);
            recStopButton.gameObject.SetActive(false);
        }
    }

    private void CreateMotionJsonFile(string fileName) //모션 파일 생성
    {
        string jsonString = JsonUtility.ToJson(motionFile, true);
        File.WriteAllText(fileName, jsonString);
        motionFile = null;
    }

    private void CreateOrAddMotionData(DoubleArray motionData) //모션 파일에 들어갈 데이터 생성
    {
        if (motionFile == null)
            motionFile = new MotionDataFile();

        motionFile.Add(motionData);
    }

    IEnumerator Flicker() //rec이미지 깜박이기
    {
        while (StateUpdater.isRecording)
        {
            recordImage.CrossFadeAlpha(0, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
            recordImage.CrossFadeAlpha(1f, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
        }
    }
    
    IEnumerator Recording() //녹화하기
    {
        if (motionFile != null)
            motionFile = null;

        while (StateUpdater.isRecording)
        {
            yield return delayRecordTime;
            jsonManager.UpdateMotionDataForSimulator(recordTime);
            CreateOrAddMotionData(jsonManager.GetMotionDataForSimulator);
        }
    }

    //private void TimerCounter(float recodeTime)
    //{
    //    elapsedTime += Time.deltaTime;
    //    if (elapsedTime >= recodeTime)
    //    {
    //        jsonManager.UpdateMotionDataForSimulator();
    //        CreateOrAddMotionData(jsonManager.GetMotionDataForSimulator);
    //        elapsedTime = 0f;
    //    }
    //}
}
