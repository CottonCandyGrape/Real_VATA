using System;
using UnityEngine;

using JointIndex = KinectWrapper.NuiSkeletonPositionIndex;

public enum TargetAxis
{
    X, Y, Z
}

public enum JointName
{
    shoulder_l1, shoulder_l2, elbow_l, shoulder_r1, shoulder_r2, elbow_r
}

[Serializable]
public class VectorMaterial
{
    public JointIndex Start;
    public JointIndex End;
    public TargetAxis Axis;
}

[Serializable]
public class Joint
{
    //public JointIndex jointIndex; //코드에서 사용x. Inspector에서 쉽게 구분하기위해 사용.
    public JointName jointName;
    public TargetAxis rotationAxis;
    public Transform targetTransform;

    public VectorMaterial parentVectorMaterial;
    public VectorMaterial childVectorMaterial;

    public float direction = 1f;
    public float offset = 0f;

    public KinectManager manager { get; set; }

    public void UpdateRotation()
    {
        Vector3 vector1; //기준이 되는 벡터.
        Vector3 vector2; //기준과 비교할 벡터.

        if (parentVectorMaterial.Start == JointIndex.HipCenter) //조인트 고유의 좌표가 필요할 때
        {
            vector1 = MathUtil.GetHipCenterCoordinate(manager)[(int)parentVectorMaterial.Axis];
            //vector1 = MathUtil.GetJointCoordinate(manager, JointIndex.HipCenter)[(int)parentVectorMaterial.Axis];
        }
        else //조인트간의 사이 벡터가 필요할 때
        {
            vector1 = MathUtil.GetVectorBetween(parentVectorMaterial.Start, parentVectorMaterial.End, manager);
        }

        if (childVectorMaterial.Start == JointIndex.Head)
        {
            vector2 = MathUtil.GetHeadCoordinate(manager)[(int)childVectorMaterial.Axis];
            //vector2 = MathUtil.GetJointCoordinate(manager, JointIndex.Head)[(int)parentVectorMaterial.Axis];
        }
        else
        {
            vector2 = MathUtil.GetVectorBetween(childVectorMaterial.Start, childVectorMaterial.End, manager);
        }

        //vector2 = MathUtil.GetVectorBetween(childVectorMaterial.Start, childVectorMaterial.End, manager);


        float angle;
        angle = (MathUtil.Dot(vector1, vector2) + offset) * direction;
        Vector3 targetOrientation = Vector3.zero;

        switch (rotationAxis)
        {
            case TargetAxis.X: targetOrientation.x = angle; break;
            case TargetAxis.Y: targetOrientation.y = angle; break;
            case TargetAxis.Z: targetOrientation.z = angle; break;
            default: break;
        }

        targetTransform.localRotation = Quaternion.Euler(targetOrientation);
    }
}