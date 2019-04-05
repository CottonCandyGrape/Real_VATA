using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject[] rightArm;
    public GameObject[] leftArm;

    private bool[] rightArmCollision;
    private bool[] leftArmCollision;

    public static bool RightArmMove = false;
    public static bool LeftArmMove = false;

    void Start()
    {
        InitArm();
    }

    void Update()
    {
        //CheckCollisionRightArm();
    }

    private void InitArm()
    {
        rightArmCollision = new bool[rightArm.Length];
        leftArmCollision = new bool[leftArm.Length];

        for (int i = 0; i < rightArm.Length; i++)
        {
            rightArm[i].AddComponent<MOCCAPart>();
            leftArm[i].AddComponent<MOCCAPart>();
            rightArmCollision[i] = rightArm[i].GetComponent<MOCCAPart>().collision;
            leftArmCollision[i] = leftArm[i].GetComponent<MOCCAPart>().collision;
        }
    }

    //private void CheckCollisionRightArm()
    //{
    //    if (!rightArmCollision[0])
    //        RightArmMove = false;
    //    else if (rightArmCollision[3])
    //        RightArmMove = true;
    //}
}