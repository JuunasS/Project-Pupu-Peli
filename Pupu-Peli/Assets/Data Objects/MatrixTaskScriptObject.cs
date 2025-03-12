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

    public bool CompareAgeVal(int val)
    {
        if(val >= minAge || val <= maxAge) return true;

        return false;
    }

    public bool CompareWeightVal(int val)
    {
        if (val >= minWeight || val <= maxWeight) return true;

        return false;
    }

    public bool CompareHeightVal(int val)
    {
        if (val >= minHeight || val <= maxHeight) return true;

        return false;
    }

    public bool CompareProductivityVal(int val)
    {
        if (val >= minProductivity || val <= maxProductivity) return true;

        return false;
    }
}
