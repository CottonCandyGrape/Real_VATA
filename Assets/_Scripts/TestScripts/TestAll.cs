using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestAll : MonoBehaviour
{
    public GraphicRaycaster raycaster;

    EventSystem eventSystem;
    PointerEventData data;
    List<RaycastResult> results;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        data = new PointerEventData(eventSystem);
    }

    private void Update()
    {
        GetOptionText();
    }

    private void GetOptionText()
    {
        data = new PointerEventData(eventSystem);
        data.position = Input.mousePosition;

        results = new List<RaycastResult>();
        raycaster.Raycast(data, results);

        //if (results.Count > 0)
        //{
        //    GameObject result = results[0].gameObject;
        //    string optionText = result.GetComponent<Text>().text;
        //    Debug.Log(optionText);
        //}
        Debug.Log(results[0].gameObject.name);

    }

}


