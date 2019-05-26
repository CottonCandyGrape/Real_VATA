using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class MotionDropdown : Dropdown
{
    public Text text;
    public PointerEventData pointerEventData;

    protected override void Start()
    {
    }

    private void Update()
    {
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Debug.Log(text.text);

    }
}