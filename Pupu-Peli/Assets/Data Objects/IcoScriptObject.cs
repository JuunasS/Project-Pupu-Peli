using NUnit.Framework;
using System.Collections.Generic;
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

    public float minWeight;
    public float maxWeight;

    public float minHeight;
    public float maxHeight;

    public int minProductivity;
    public int maxProductivity;

}
