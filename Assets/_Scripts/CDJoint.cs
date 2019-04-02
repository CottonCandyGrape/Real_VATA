using System;
using UnityEngine;

using JointIndex = KinectWrapper.NuiSkeletonPositionIndex;

namespace Real_VATA
{
    public enum TargetAxis
    {
        X, Y, Z
    }

    public enum JointName
    {
        shoulder_l1, shoulder_l2, elbow_l, shoulder_r1, shoulder_r2, elbow_r, Length
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

        public KinectManager manager { get; set; }

        public void UpdateRotation()
        {
            Vector3 parentVector;
            Vector3 childVector;
            float angle; //Joint회전 각도.

            if (parentVectorMaterial.Start == JointIndex.HipCenter)
            {
                parentVector = MathUtil.GetJointCoordinate(manager, JointIndex.HipCenter)[(int)parentVectorMaterial.Axis];
            }
            else
            {
                parentVector = MathUtil.GetVectorBetween(parentVectorMaterial.Start, parentVectorMaterial.End, manager);
            }

            if (childVectorMaterial.Start == JointIndex.Head)
            {
                childVector = MathUtil.GetJointCoordinate(manager, JointIndex.Head)[(int)parentVectorMaterial.Axis];
            }
            else
            {
                childVector = MathUtil.GetVectorBetween(childVectorMaterial.Start, childVectorMaterial.End, manager);
            }

            angle = (MathUtil.Dot(parentVector, childVector) + offset) * direction; //얻은 두개의 벡터로 angle 계산
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
}