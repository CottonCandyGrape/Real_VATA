using UnityEngine;

public class OrientationTester : MonoBehaviour
{
    public KinectWrapper.NuiSkeletonPositionIndex jointIndex;
    //public Transform jointTarget;

    private KinectManager manager;
    //Vector3 a = new Vector3(Mathf.Sqrt(2), Mathf.Sqrt(2), 2);
    //Vector3 b = new Vector3(1, 1, 0);

    private void Awake()
    {
        manager = KinectManager.Instance;
        //jointTarget.GetComponent<Transform>();
        //Debug.Log(GetDegBetweenTwoVector(a, b));
    }

    private void Update()
    {

        //Matrix4x4 orientation = manager.GetJointOrientationMatrix(jointIndex);

        //Vector3 xOrientation = new Vector3(orientation.GetColumn(0).x, orientation.GetColumn(0).y, orientation.GetColumn(0).z);

        //jointTarget.localRotation = 
        //Debug.Log(GetDegBetweenTwoVector(orientation.GetColumn(0), orientation.GetColumn(2)));
        //Debug.Log("x: " + orientation.GetColumn(0));
        //Debug.Log("y: " + orientation.GetColumn(1));
        //Debug.Log("z: " + orientation.GetColumn(2));
        Debug.Log(GetDegElbowRight(5, 6));
    }

    private float GetDegElbowRight(int elbow_right, int wrist_right) 
    {
        Matrix4x4 elbowOrientation = manager.GetJointOrientationMatrix(elbow_right);
        Matrix4x4 wristOrientation = manager.GetJointOrientationMatrix(wrist_right);
        
        float theta = GetDegBetweenTwoVector(elbowOrientation.GetColumn(0), wristOrientation.GetColumn(0));

        return 180 - theta;
    }

    private float GetDegBetweenTwoVector(Vector4 start, Vector4 end)
    {
        float theta = Mathf.Acos(Vector4.Dot(start, end) / (start.magnitude * end.magnitude));

        return theta * Mathf.Rad2Deg;
    }

    //private float GetCosBetweenTwoVector(Vector4 start, Vector4 end)
    //{
    //    float theta = Vector4.Dot(start, end);
    //    return theta * Mathf.Rad2Deg;
    //}
}