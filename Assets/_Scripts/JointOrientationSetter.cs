using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointOrientationSetter : MonoBehaviour
{
    public Joint[] joints;

    private KinectManager manager;

    private void Awake() //Joint 여러개 한꺼번에 제어.
    {
        manager = KinectManager.Instance;

        foreach (Joint joint in joints)
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
        foreach (Joint joint in joints)
        {
            joint.UpdateRotation();
        }
    }
}
