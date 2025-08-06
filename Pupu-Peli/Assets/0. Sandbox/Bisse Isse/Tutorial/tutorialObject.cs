using UnityEngine;


[CreateAssetMenu(fileName = "Mission Tutorial", menuName = "Scriptable Objects/Tutorial Object")]
public class tutorialObject : ScriptableObject
{
    public string missionName;
    public string tutorialText;
}