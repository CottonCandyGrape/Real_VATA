using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject[] rightArm; //오른팔 오브젝트 배열
    public GameObject[] leftArm; //왼팔 오브젝트 배열

    private bool[] rightArmCollision; //오브젝트들의 충돌여부 boolean 배열    
    private bool[] leftArmCollision;

    public static bool rightArmMove = true; // 오른팔 동작 boolean
    public static bool leftArmMove = true; // 왼팔 동작 boolean

    void Start()
    {
        InitBothArms();
    }

    void Update()
    {
        UpdateArmCollision();
        CheckCollisionArm();
    }

    private void InitBothArms() //양팔 오브젝트에 MOOCAPart.cs 컴포넌트 추가, boolean 배열 초기화.
    {
        rightArmCollision = new bool[rightArm.Length];
        leftArmCollision = new bool[leftArm.Length];

        for (int i = 0; i < rightArm.Length; i++)
        {
            rightArm[i].AddComponent<MOCCAPart>();
            leftArm[i].AddComponent<MOCCAPart>();
        }
    }

    private void UpdateArmCollision() //부품의 충돌 여부를 갱신
    {
        for (int i = 0; i < rightArmCollision.Length; i++)
        {
            rightArmCollision[i] = rightArm[i].GetComponent<MOCCAPart>().collision;
            leftArmCollision[i] = leftArm[i].GetComponent<MOCCAPart>().collision;
        }
    }

    private void CheckCollisionArm() //충돌 여부에 따라 팔 전체를 컨트롤하는 boolean값 toggle
    {
        CheckCollisionRightArm(); //오른팔
        CheckCollisionLeftArm(); //왼팔
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
            else if (!rightArmCollision[i])
            {
                rightArmMove = true;
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
            else if (!leftArmCollision[i])
            {
                leftArmMove = true;
            }
        }
    }
}
