using UnityEngine;

public class CDJointOrientationSetter : MonoBehaviour
{
    public AngleMessenger angleMessenger;
    public CDJoint[] joints;

    private KinectManager manager;

    private void Awake() //CDJoint 여러개 한꺼번에 제어.
    {
        manager = KinectManager.Instance;

        foreach (CDJoint joint in joints)
        {
            joint.kinectManager = manager;
        }
    }

    private void Update()
    {
        if (angleMessenger)
        {
            UpdateJointRotations();
        }
        else
        {
            UpdateFileJointRotations();
        }
    }

    private void UpdateFileJointRotations()
    {
        foreach (CDJoint joint in joints)
        {
            joint.UpdateFileRotation();
        }
    }

    private void UpdateJointRotations()
    {
        foreach (CDJoint joint in joints)
        {
            joint.UpdateRotation();
        }
    }
}