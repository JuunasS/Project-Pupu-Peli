using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemScriptObject", menuName = "Scriptable Objects/ShopItemScriptObject")]
public class ShopItemScriptObject : ScriptableObject
{
    public string name;
    [TextArea] public string description;

    // Image should be gotten from assets with in code as string so that it works event when saved to file?
    public Sprite image;

}
