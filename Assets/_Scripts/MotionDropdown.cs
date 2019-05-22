using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionDropdown : Dropdown
{
    protected override void Start()
    {
        //captionText.text = "Select Motion data File";
        //RefreshShownValue();
    }

    private void OnWillRenderObject()
    {
        //captionText.text = "Select Motion data File";
    }

    protected override void OnValidate()
    {
        //captionText.text = "Select Motion data File";
    }

    protected override void OnCanvasGroupChanged()
    {
        //captionText.text = "Select Motion data File";
    }

    protected override void OnEnable()
    {
    }

    private void OnPostRender()
    {
        //captionText.text = "Select Motion data File";
        //RefreshShownValue();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Debug.Log("OnRenderImage");
        //captionText.text = "Select Motion data File";
        //RefreshShownValue();
    }

    private void OnRenderObject()
    {
        //captionText.text = "Select Motion data File";
    }
}