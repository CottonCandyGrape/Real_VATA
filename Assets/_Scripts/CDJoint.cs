﻿using System;
using UnityEngine;

using JointIndex = KinectWrapper.NuiSkeletonPositionIndex;

public enum TargetAxis
{
    X, Y, Z
}

public enum JointName
{
    shoulder_l1, shoulder_l2, elbow_l, shoulder_r1, shoulder_r2, elbow_r, pan, tilt
}

[Serializable]
public class VectorMaterial
{
    public JointIndex Start;
    public JointIndex End;
    public TargetAxis Axis;
}

[Serializable]
public class CDJoint
{
    public JointName jointName; //JointIndex는 shoulder1, 2 구분 못함 => JointName 사용.
    public TargetAxis rotationAxis;
    public Transform targetTransform;

    public VectorMaterial parentVectorMaterial; //기준이 되는 벡터.
    public VectorMaterial childVectorMaterial; //기준과 비교할 벡터.

    public float direction = 1f;
    public float offset = 0f;
    public float angle { get; private set; }//Joint회전 각도.

    public KinectManager kinectManager { get; set; } //CDJointOrientationSetter에서 쓰기 때문에 public
    private FacetrackingManager faceTrackingManager;

    public void UpdateRotation()
    {
        Vector3 parentVector;
        Vector3 childVector;

        if (parentVectorMaterial.Start == JointIndex.HipCenter)
        {
            parentVector = MathUtil.GetJointCoordinate(kinectManager, JointIndex.HipCenter)[(int)parentVectorMaterial.Axis];
        }
        else //팔꿈치에만 쓰임
        {
            parentVector = MathUtil.GetVectorBetween(parentVectorMaterial.Start, parentVectorMaterial.End, kinectManager);
        }

        childVector = MathUtil.GetVectorBetween(childVectorMaterial.Start, childVectorMaterial.End, kinectManager);

        if (jointName == JointName.pan || jointName == JointName.tilt) //목 제어
        {
            angle = MathUtil.GetNeckAngle(faceTrackingManager, jointName);
        }
        else
        {
            angle = (MathUtil.Dot(parentVector, childVector) + offset) * direction; //얻은 두개의 벡터로 angle 계산
        }

        angle = MathUtil.LimitJointAngle(jointName, angle);
        RotateJoint(angle);
    }

    private void RotateJoint(float angle) //angle만큼 Joint 회전
    {
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