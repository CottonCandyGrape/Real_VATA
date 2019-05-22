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
        speedText.text = MathUtil.Roundoff(angleSlider.value / 5f).ToString();
        angleText.text = MathUtil.Roundoff(angleSlider.value / 16f).ToString();
    }

    public void AngleSliderChange()
    {
        angleText.text = MathUtil.Roundoff(angleSlider.value / 5f).ToString();
        //angleText.text = angleSlider.value.ToString();

    }
}


