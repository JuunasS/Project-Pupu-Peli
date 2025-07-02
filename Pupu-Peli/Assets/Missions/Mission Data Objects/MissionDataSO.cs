using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MissionDataSO", menuName = "Scriptable Objects/MissionData")]
public abstract class MissionDataSO : ScriptableObject
{
    public GameObject missionPrefab;

    public abstract void SetMissionDataSO(GameObject instansiatedObject);
}
