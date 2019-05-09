using UnityEngine;

[System.Serializable]
public class MotionDataFile
{
    [SerializeField]
    private JsonDoubleArray[] jsonDoubleArrays;

    public int Length
    {
        get { return jsonDoubleArrays == null ? -1 : jsonDoubleArrays.Length; }
    }

    public JsonDoubleArray this[int index]
    {
        get { return jsonDoubleArrays == null ? null : jsonDoubleArrays[index]; }
        set { jsonDoubleArrays[index] = value; }
    }

    public void Add(JsonDoubleArray newJsonData)
    {
        if (jsonDoubleArrays == null)
        {
            jsonDoubleArrays = new JsonDoubleArray[1];
            jsonDoubleArrays[0] = newJsonData;

            return;
        }

        JsonDoubleArray[] tempjsonDoubleArrays = new JsonDoubleArray[jsonDoubleArrays.Length];
        for (int ix = 0; ix < tempjsonDoubleArrays.Length; ++ix)
        {
            tempjsonDoubleArrays[ix] = jsonDoubleArrays[ix];
        }

        jsonDoubleArrays = new JsonDoubleArray[jsonDoubleArrays.Length + 1];
        for (int ix = 0; ix < tempjsonDoubleArrays.Length; ++ix)
        {
            jsonDoubleArrays[ix] = tempjsonDoubleArrays[ix];
        }

        jsonDoubleArrays[jsonDoubleArrays.Length - 1] = newJsonData;
    }

    public void RemoveAt(int index)
    {
        if (jsonDoubleArrays == null) return;

        JsonDoubleArray[] tempjsonDoubleArrays = new JsonDoubleArray[jsonDoubleArrays.Length - 1];
        int idx = 0;
        for (int ix = 0; ix < jsonDoubleArrays.Length; ++ix)
        {
            if (ix == index) continue;
            tempjsonDoubleArrays[idx] = jsonDoubleArrays[ix];
            ++idx;
        }

        jsonDoubleArrays = new JsonDoubleArray[tempjsonDoubleArrays.Length];
        for (int ix = 0; ix < jsonDoubleArrays.Length; ++ix)
        {
            jsonDoubleArrays[ix] = tempjsonDoubleArrays[ix];
        }
    }
}