using UnityEngine;

public class AngleMessenger : MonoBehaviour
{
    [SerializeField]
    private CDJointOrientationSetter cdJointOrientationSetter;

    [SerializeField]
    private JointOrientationSetter jointOrientationSetter;

    private CDJoint[] cdJoints;
    private Joint[] joints;

    private void Awake()
    {
        cdJoints = cdJointOrientationSetter.joints;
        joints = jointOrientationSetter.joints;
    }

    private void Update()
    {
        SendAngle();
    }

    void SendAngle()
    {
        SendAngleToRightArm();
        SendAngleToLeftArm();
    }

    void SendAngleToRightArm()
    {
        if (CollisionManager.rightArmMove)
        {
            for (int i = 0; i < 3; i++)
                joints[i].angle = cdJoints[i].angle;
        }
    }

    void SendAngleToLeftArm()
    {
        if (CollisionManager.leftArmMove)
        {
            for (int i = 3; i < 6; i++)
                joints[i].angle = cdJoints[i].angle;
        }
    }

    //void SendAngleTo()
    //{

    //}
}