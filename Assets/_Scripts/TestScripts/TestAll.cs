using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestAll : MonoBehaviour
{
    //public Button btn;
    //GameObject parent;
    private void Start()
    {
        Transform transform = gameObject.GetComponent<Transform>();
    }


    public void GetParentObject()
    {
        string text = transform.parent.gameObject.GetComponent<Text>().text;

        Debug.Log(text);
    }
}


