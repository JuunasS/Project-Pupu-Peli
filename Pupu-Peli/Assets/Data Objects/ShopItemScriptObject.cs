using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemScriptObject", menuName = "Scriptable Objects/ShopItemScriptObject")]
public class ShopItemScriptObject : ScriptableObject
{
    public string ID; // This is used for identifying this data object from files and lists
    //public string name;
    [TextArea] public string description;

    // Image should be gotten from assets with in code as string so that it works event when saved to file?
    public Sprite image;


}
