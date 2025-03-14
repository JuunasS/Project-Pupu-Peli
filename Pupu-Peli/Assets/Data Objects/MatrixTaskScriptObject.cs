using UnityEngine;

[CreateAssetMenu(fileName = "MatrixTaskScriptObject", menuName = "Scriptable Objects/MatrixTaskScriptObject")]
public class MatrixTaskScriptObject : ScriptableObject
{
    [TextArea] public string taskDesc;

    // Use these values for randomizing 
    public bool checkAge;
    public int minAge;
    public int maxAge;

    public bool checkWeight;
    public float minWeight;
    public float maxWeight;

    public bool checkHeight;
    public float minHeight;
    public float maxHeight;

    public bool checkProductivity;
    public int minProductivity;
    public int maxProductivity;

    // The functions below compare given parameter value to the task minimum and maximum values
    // Returns "true" if the value is within requirments
    public bool CompareAgeVal(int val)
    {
        Debug.Log("minAge: " + minAge + " maxAge: " + maxAge);
        if (val >= minAge && val <= maxAge) { return true; }

        return false;
    }

    public bool CompareWeightVal(float val)
    {
        if (val >= minWeight && val <= maxWeight) { return true; }

        return false;
    }

    public bool CompareHeightVal(float val)
    {
        if (val >= minHeight && val <= maxHeight) { return true; }

        return false;
    }

    public bool CompareProductivityVal(int val)
    {
        if (val >= minProductivity && val <= maxProductivity) { return true; }

        return false;
    }
}
