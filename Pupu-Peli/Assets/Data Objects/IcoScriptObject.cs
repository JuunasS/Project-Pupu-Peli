using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "IcoScriptObject", menuName = "Scriptable Objects/IcoScriptObject")]
public class IcoScriptObject : ScriptableObject
{
    public string icoName;
    public Sprite icoImg;
    [TextArea] public string icoText;
}
