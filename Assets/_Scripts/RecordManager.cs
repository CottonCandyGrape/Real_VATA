using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

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

    private float fps = 5f;
    private float recordTime = 0f;
    private float elapsedTime = 0f;

    private string filePath = "Assets/JsonData/";

    private void Start()
    {
        recordTime = 1 / fps;
        directoryInfo = new DirectoryInfo("Assets/JsonData/");

        recStopButton.gameObject.SetActive(false);
        recordImage.gameObject.SetActive(false);

        SetDropdownOptions();
        Debug.Log(StateUpdater.isConnectingKinect);
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
        //dropdown.captionText.text = "Select Motion data File";
    }

    public void ChangedDropdownOption() //드롭다운 옵션 바뀌었을 때 
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
        Debug.Log("ClickedStartButton");
        if (StateUpdater.isConnectingKinect)
        {
            if (StateUpdater.isRealTimeMode && !StateUpdater.isMotionDataPlaying)
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
                Debug.Log("실시간 모드가 아니거나 모션을 실행중입니다.");
        }
        else
            Debug.Log("Kinect가 연결되어 있지 않습니다.");
    }

    public void ClickedStopButton() //녹화 끝 버튼
    {
        Debug.Log("ClickedStopButton");
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

    public void ToggleRecordButton()
    {
        if (!recStopButton.gameObject.activeSelf) //RecStartButton 누를때
        {
            recStopButton.gameObject.SetActive(true);
            recStartButton.gameObject.SetActive(false);
        }
        else // RecEndButton 누를때
        {
            recStartButton.gameObject.SetActive(true);
            recStopButton.gameObject.SetActive(false);
        }
    }

    private void CreateMotionJsonFile(string fileName)
    {
        string jsonString = JsonUtility.ToJson(motionFile, true);
        File.WriteAllText(fileName, jsonString);
        motionFile = null;
    }

    private void CreateOrAddMotionData(DoubleArray motionData)
    {
        if (motionFile == null)
            motionFile = new MotionDataFile();

        motionFile.Add(motionData);
    }

    IEnumerator Flicker()
    {
        while (StateUpdater.isRecording)
        {
            recordImage.CrossFadeAlpha(0, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
            recordImage.CrossFadeAlpha(1f, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator Recording()
    {
        while (StateUpdater.isRecording)
        {
            yield return new WaitForSeconds(recordTime);
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

    //private bool RedundancyCheck(string fileName) //파일이름 중복체크
    //{
    //    for (int i = 0; i < fileInfo.Length; i++)
    //    {
    //        if (fileInfo[i].Name.Equals(fileName))
    //        {
    //            Debug.Log("이미 존재하는 이름입니다.");
    //            return false;
    //        }
    //    }
    //    Debug.Log("사용가능한 이름입니다.");
    //    return true;
    //}
}
