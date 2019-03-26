using System;
using JointIndex = KinectWrapper.NuiSkeletonPositionIndex;

public class JointComponent
{
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
}
