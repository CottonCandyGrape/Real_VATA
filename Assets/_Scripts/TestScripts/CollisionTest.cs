using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("<color=red>OnTriggerEnter: " + other.gameObject + "</color>");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("<color=green>OnTriggerStay: " + other.gameObject + "</color>");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("<color=blue>OnTriggerExit: " + other.gameObject + "</color>");
    }
}
