using UnityEngine;
using JointIndex = KinectWrapper.NuiSkeletonPositionIndex;

public class OrientationTester : MonoBehaviour
{
    public JointIndex jointIndex;

    public Transform elbow;
    public Transform shoulder1;
    public Transform shoulder2;

    private KinectManager manager;
    
    private void Awake()
    {
        manager = KinectManager.Instance;
    }

    private void Update()
    {
        float angle = GetXVector();
        Vector3 ori = elbow.rotation.eulerAngles;
        ori.z = angle;
        elbow.rotation = Quaternion.Euler(ori);

        Matrix4x4 hipCenter = manager.GetJointOrientationMatrix((int)JointIndex.HipCenter);//모든 조인트의 기준점
        
        Vector3 shoulderLeftX = GetPositionBetween(JointIndex.ElbowLeft, JointIndex.ShoulderLeft);//순서 중요
        Vector3 hipCenterX = new Vector3(hipCenter.GetColumn(0).x, hipCenter.GetColumn(0).y, hipCenter.GetColumn(0).z);
        Vector3 hipCenterY = new Vector3(hipCenter.GetColumn(1).x, hipCenter.GetColumn(1).y, hipCenter.GetColumn(1).z);

        angle = -Dot(hipCenterX, shoulderLeftX);
        Vector3 shoulderOri = shoulder2.rotation.eulerAngles;
        shoulderOri.y = angle;
        shoulder2.rotation = Quaternion.Euler(shoulderOri);

        angle = Dot(hipCenterY, shoulderLeftX);
        Vector3 shoulderOriX = shoulder1.rotation.eulerAngles;
        shoulderOriX.x = angle;
        shoulder1.rotation = Quaternion.Euler(shoulderOriX);
    }

    Vector3 GetPositionBetween(JointIndex joint1, JointIndex joint2)
    {
        uint playerID = manager != null ? manager.GetPlayer1ID() : 0;
        Vector3 pos1 = manager.GetJointPosition(playerID, (int)joint1);
        Vector3 pos2 = manager.GetJointPosition(playerID, (int)joint2);

        return pos2 - pos1;
    }

    private float GetXVector()
    {
        uint playerID = manager != null ? manager.GetPlayer1ID() : 0;

        Vector3 vx1 = GetPositionBetween(JointIndex.ShoulderLeft, JointIndex.ElbowLeft);
        Vector3 vx2 = GetPositionBetween(JointIndex.ElbowLeft, JointIndex.WristLeft);
        
        return Dot(vx1, vx2);
        //return 180f - Dot(vx1, vx2);
    }

    //private float GetDegElbowRight(JointIndex index1, JointIndex index2)
    //{
    //    return GetDegElbowRight((int)index1, (int)index2);
    //}

    //private float GetDegElbowRight(int elbow_right, int wrist_right)
    //{
    //    uint playerID = manager != null ? manager.GetPlayer1ID() : 0;

    //    Matrix4x4 elbowOrientation = manager.GetJointOrientationMatrix(elbow_right);
    //    Matrix4x4 wristOrientation = manager.GetJointOrientationMatrix(wrist_right);

    //    float theta = GetDegBetweenTwoVector(elbowOrientation.GetColumn(0), wristOrientation.GetColumn(0));

    //    Vector3 pos = elbowOrientation.GetColumn(0);
    //    sphere1.transform.position = pos + manager.GetJointPosition(playerID, elbow_right) * 2f;
    //    pos = wristOrientation.GetColumn(0);
    //    sphere2.transform.position = pos + manager.GetJointPosition(playerID, wrist_right) * 2f;

    //    return 180 - theta;
    //}

    private float Dot(Vector3 start, Vector3 end)
    {
        start.Normalize();
        end.Normalize();
        float theta = Mathf.Acos(Vector3.Dot(start, end));

        return theta * Mathf.Rad2Deg;
    }

    private float GetDegBetweenTwoVector(Vector4 start, Vector4 end)
    {
        start.Normalize();
        end.Normalize();
        //float theta = Mathf.Acos(Vector4.Dot(start, end) / (start.magnitude * end.magnitude));
        float theta = Mathf.Acos(Vector4.Dot(start, end));

        return Mathf.Abs(theta * Mathf.Rad2Deg);
    }

    //private float GetCosBetweenTwoVector(Vector4 start, Vector4 end)
    //{
    //    float theta = Vector4.Dot(start, end);
    //    return theta * Mathf.Rad2Deg;
    //}
}