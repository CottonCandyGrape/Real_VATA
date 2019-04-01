using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Real_VATA
{
    public class JointOrientationSetter : MonoBehaviour
    {
        public Joint[] joints;

        private void Update()
        {
            UpdateJointRotations();
        }

        private void UpdateJointRotations()
        {
            foreach (Joint joint in joints)
            {
                joint.UpdateRotation();
            }
        }
    }
}