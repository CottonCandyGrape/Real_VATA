using UnityEngine;

[System.Serializable]
public class TestAll
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
        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = array[i];
        }

        array = new double[array.Length + 1];
        for (int i = 0; i < tempArray.Length; i++)
        {
            array[i] = tempArray[i];
        }

        array[array.Length - 1] = angle;
    }

    public void RemoveAt(int index)
    {
        if (array == null) return;

        double[] tempArray = new double[array.Length - 1];
        int idx = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (i == index) continue;
            tempArray[idx] = array[i];
            idx++;
        }

        array = new double[tempArray.Length];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = tempArray[i];
        }
    }
}