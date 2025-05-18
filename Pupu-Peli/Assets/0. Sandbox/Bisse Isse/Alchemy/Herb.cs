using UnityEngine;

[CreateAssetMenu(fileName = "Herb", menuName = "Scriptable Objects/Herb")]
public class Herb : ScriptableObject
{
    public string name;
    public Sprite image;
    public Sprite reverse;
    public bool isReverse = false;
}
