using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class MotionDropdown : Dropdown
{
    public Dropdown dropdown;

    DropdownItem[] dropdownItems;

    protected override void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdownItems = new DropdownItem[dropdown.options.Count];
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            dropdownItems[i].text.text = dropdown.options[i].text;
            Debug.Log(dropdownItems[i].text.text);
        }
    }
}