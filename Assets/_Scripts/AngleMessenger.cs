using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Real_VATA
{
    public class AngleMessenger : MonoBehaviour
    {
        //public Dictionary<JointName, CDJoint> cdJointDictionary = new Dictionary<JointName, CDJoint>();

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

            foreach (CDJoint joint in cdJoints)
            {
                Debug.Log("CDJoint");
            }

            foreach (Joint joint in joints)
            {
               Debug.Log("Joint");
            }
        }

        private void Update()
        {

        }

        void UpdateDictionary()
        {

        }

        //void InitDictionary(JointName jointName, CDJoint cdJoint)
        //{
        //    for (int i = 0; i < (int)JointName.Length; i++)
        //    {
        //        cdJointDictionary.Add(jointName, cdJoint);
        //    }
        //}
    }
}