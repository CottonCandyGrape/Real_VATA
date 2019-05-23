using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TestAll : MonoBehaviour
{
    public Slider speedSlider;
    public Slider angleSlider;
    public Text speedText;
    public Text angleText;

    private void Start()
    {
        //angleText.text = "0%";
        angleText.text = "x1";
        speedText.text = "x1";
        //Debug.Log("speedSlider.value: " + speedSlider.value + " angleSlider.value: " + angleSlider.value);
    }

    public void AngleSliderChange()
    {
        //angleText.text = angleSlider.value * 10 + "%";
        angleText.text = "x" + angleSlider.value * 0.1;
    }

    public void SpeedSliderChange()
    {
        speedText.text = "x" + speedSlider.value * 0.1;
    }
}


