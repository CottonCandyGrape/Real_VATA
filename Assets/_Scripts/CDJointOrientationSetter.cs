﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Real_VATA
{
    public class CDJointOrientationSetter : MonoBehaviour
    {
        public CDJoint[] joints;

        private KinectManager manager;

        private void Awake() //CDJoint 여러개 한꺼번에 제어.
        {
            manager = KinectManager.Instance;

            foreach (CDJoint joint in joints)
            {
                joint.manager = manager;
            }
        }

        private void Update()
        {
            UpdateJointRotations();
        }

        private void UpdateJointRotations()
        {
            foreach (CDJoint joint in joints)
            {
                joint.UpdateRotation();
            }
        }
    }

}