using System;
using UnityEngine;

using JointIndex = KinectWrapper.NuiSkeletonPositionIndex;

public enum TargetAxis
{
    X, Y, Z
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
    public JointIndex jointIndex;
    public Transform targetTransform;

    public VectorMaterial childVectorMaterial;
    public VectorMaterial parentVectorMaterial;

    public float direction = 1f;
    public float offset = 0f;

    public KinectManager manager { get; set; }

    public void UpdateRotation()
    {
        Vector3 vector1;
        Vector3 vector2;

        if (parentVectorMaterial.Start == JointIndex.HipCenter)
        {
            vector1 = MathUtil.GetHipCenterCoordinate(manager)[(int)parentVectorMaterial.Axis];
        }
        else
        {
            vector1 = MathUtil.GetVectorBetween(parentVectorMaterial.Start, parentVectorMaterial.End, manager);
        }

        vector2 = MathUtil.GetVectorBetween(childVectorMaterial.Start, childVectorMaterial.End, manager);
        float angle;
        angle = (MathUtil.Dot(vector1, vector2) + offset) * direction;
        Vector3 targetOrientation = Vector3.zero;

        switch (childVectorMaterial.Axis)
        {
            case TargetAxis.X: targetOrientation.x = angle; break;
            case TargetAxis.Y: targetOrientation.y = angle; break;
            case TargetAxis.Z: targetOrientation.z = angle; break;
            default: break;
        }

        targetTransform.localRotation = Quaternion.Euler(targetOrientation);
    }
}