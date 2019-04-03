using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleMessenger : MonoBehaviour
{
    private CDJointOrientationSetter cdJointOrientationSetter;
    private JointOrientationSetter jointOrientationSetter;

    private CDJoint[] cdJoints;
    private Joint[] joints;

    private void Awake()
    {
        cdJointOrientationSetter = GameObject.Find("OrientationManager_CDMOCCA").GetComponent<CDJointOrientationSetter>();
        cdJoints = cdJointOrientationSetter.joints;

        jointOrientationSetter = GameObject.Find("OrientationManager_MOCCA").GetComponent<JointOrientationSetter>();
        joints = jointOrientationSetter.joints;
    }

    private void Update()
    {
        SendAngle();
    }

    void SendAngle()
    {
        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].angle = cdJoints[i].angle;
        }
    }
}