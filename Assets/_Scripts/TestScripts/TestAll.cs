using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TestAll : MonoBehaviour
{
    public Slider speedSlider;
    public Slider angleSlider;

    private void Update()
    {
        Debug.Log("<color=red>speedSlider.value: " + MathUtil.Roundoff(speedSlider.value) + "</color>");
        Debug.Log("angleSlider.value: " + MathUtil.Roundoff(angleSlider.value));
    }
}

