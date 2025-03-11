using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "IcoScriptObject", menuName = "Scriptable Objects/IcoScriptObject")]
public class IcoScriptObject : ScriptableObject
{
    public string icoName;
    public Sprite icoImg;
    [TextArea] public string icoText;


    // Use these values for randomizing 
    public int minAge;
    public int maxAge;

    public float maxWeight;
    public float minWeight;

    public float maxHeight;
    public float minHeight;

    public int maxPoductivity;
    public int minProductivity;

}
