using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject[] rightArm; //오른팔 오브젝트 배열
    public GameObject[] leftArm; //왼팔 오브젝트 배열

    private bool[] rightArmCollision; //오브젝트들의 충돌 boolean      
    private bool[] leftArmCollision;

    public static bool rightArmMove = true; // 오른팔 동작 boolean
    public static bool leftArmMove = true; // 왼팔 동작 boolean

    void Start()
    {
        InitArm();
    }

    void Update()
    {
        CheckCollisionArm();
    }

    private void InitArm() //양팔 오브젝트에 MOOCAPart.cs 추가, boolean 추가.
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

    private void CheckCollisionArm()
    {
        CheckCollisionRightArm();
        CheckCollisionLeftArm();
    }

    private void CheckCollisionRightArm()
    {
        for (int i = 0; i < rightArmCollision.Length; i++)
        {
            if (rightArmCollision[i])
            {
                rightArmMove = false;
                break;
            }
        }
    }

    private void CheckCollisionLeftArm()
    {
        for (int i = 0; i < leftArmCollision.Length; i++)
        {
            if (leftArmCollision[i])
            {
                leftArmMove = false;
                break;
            }
        }
    }
}
