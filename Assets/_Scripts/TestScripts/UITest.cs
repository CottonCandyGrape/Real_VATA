using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    public Button startButton;
    public Button stopButton;

    public Dropdown dropdown;

    void Start()
    {
        stopButton.gameObject.SetActive(false);
        SetDropdownOptions();
    }

    private int GetMotionDataCount() //모션데이터 개수 반환
    {
        DirectoryInfo di = new DirectoryInfo("Assets/JsonData/");
        FileInfo[] fi = di.GetFiles("*.json");

        if (fi.Length == 0) return fi.Length;
        else return fi.Length;
    }

    public void SelectButton()
    {
        //Debug.Log("Dropdown Value: " + dropdown.value + ", List Selected: " + dropdown.options.);
    }

    private void SetDropdownOptions()
    {
        dropdown.ClearOptions();
        for (int i = 0; i < GetMotionDataCount(); i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = i.ToString() + "개수";
            dropdown.options.Add(option);
        }
    }

    public void ToggleRecordButton()
    {
        if (!stopButton.gameObject.activeSelf) //startButton 누를때
        {
            stopButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
        }
        else // stopButton 누를때
        {
            startButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
        }

    }

    public void ClickedStartButton()
    {
        Debug.Log("ClickedStartButton");
    }

    public void ClickedStopButton()
    {
        Debug.Log("ClickedStopButton");
    }
}
