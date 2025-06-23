using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MissionDataSO", menuName = "Scriptable Objects/MissionData")]
public  class MissionDataSO : ScriptableObject
{

    public GameObject[] missionPrefabs;
}
/*
public class MatrixMission : MissionData
{

}

public class AlchemyMission : MissionData
{

}
*/
// Add more minigame base mission data classes as needed
