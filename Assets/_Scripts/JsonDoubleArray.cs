using UnityEngine;

//[System.Serializable]
//public class MotionDataFile
//{
//    [SerializeField]
//    private JsonDoubleArray[] jsonDoubleArrays;

//    public int Length
//    {
//        get { return jsonDoubleArrays == null ? -1 : jsonDoubleArrays.Length; }
//    }

//    public JsonDoubleArray this[int index]
//    {
//        get { return jsonDoubleArrays == null ? null : jsonDoubleArrays[index]; }
//        set { jsonDoubleArrays[index] = value; }
//    }

//    public void Add(JsonDoubleArray newJsonData)
//    {
//        if (jsonDoubleArrays == null)
//        {
//            jsonDoubleArrays = new JsonDoubleArray[1];
//            jsonDoubleArrays[0] = newJsonData;

//            return;
//        }

//        JsonDoubleArray[] tempjsonDoubleArrays = new JsonDoubleArray[jsonDoubleArrays.Length];
//        for (int ix = 0; ix < tempjsonDoubleArrays.Length; ++ix)
//        {
//            tempjsonDoubleArrays[ix] = jsonDoubleArrays[ix];
//        }

//        jsonDoubleArrays = new JsonDoubleArray[jsonDoubleArrays.Length + 1];
//        for (int ix = 0; ix < tempjsonDoubleArrays.Length; ++ix)
//        {
//            jsonDoubleArrays[ix] = tempjsonDoubleArrays[ix];
//        }

//        jsonDoubleArrays[jsonDoubleArrays.Length - 1] = newJsonData;
//    }

//    public void RemoveAt(int index)
//    {
//        if (jsonDoubleArrays == null) return;

//        JsonDoubleArray[] tempjsonDoubleArrays = new JsonDoubleArray[jsonDoubleArrays.Length - 1];
//        int idx = 0;
//        for (int ix = 0; ix < jsonDoubleArrays.Length; ++ix)
//        {
//            if (ix == index) continue;
//            tempjsonDoubleArrays[idx] = jsonDoubleArrays[ix];
//            ++idx;
//        }

//        jsonDoubleArrays = new JsonDoubleArray[tempjsonDoubleArrays.Length];
//        for (int ix = 0; ix < jsonDoubleArrays.Length; ++ix)
//        {
//            jsonDoubleArrays[ix] = tempjsonDoubleArrays[ix];
//        }
//    }
//}

[System.Serializable]
public class JsonDoubleArray
{
    [SerializeField]
    private int size;

    [SerializeField]
    private double[] array;

    public void SetSize()
    {
        size = array.Length;
    }

    public int Length
    {
        get { return array == null ? -1 : array.Length; }
    }

    public double this[int index]
    {
        get { return array == null ? -1 : array[index]; }
        set { array[index] = value; }
    }

    public void Add(double angle)
    {
        if (array == null)
        {
            array = new double[1];
            array[0] = angle;

            return;
        }

        double[] tempArray = new double[array.Length];
        for (int ix = 0; ix < tempArray.Length; ++ix)
        {
            tempArray[ix] = array[ix];
        }

        array = new double[array.Length + 1];
        for (int ix = 0; ix < tempArray.Length; ++ix)
        {
            array[ix] = tempArray[ix];
        }

        array[array.Length - 1] = angle;
    }

    public void RemoveAt(int index)
    {
        if (array == null) return;

        double[] tempArray = new double[array.Length - 1];
        int idx = 0;
        for (int ix = 0; ix < array.Length; ++ix)
        {
            if (ix == index) continue;
            tempArray[idx] = array[ix];
            ++idx;
        }

        array = new double[tempArray.Length];
        for (int ix = 0; ix < array.Length; ++ix)
        {
            array[ix] = tempArray[ix];
        }
    }
}