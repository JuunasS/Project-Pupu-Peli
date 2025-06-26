using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MissionDataSO", menuName = "Scriptable Objects/MissionData")]
public abstract class MissionDataSO : ScriptableObject
{
    public GameObject missionPrefab;

    public abstract void SetMissionDataSO(GameObject instansiatedObject);
}

/*
[CreateAssetMenu(fileName = "MatrixMissionDataSO", menuName = "Scriptable Objects/MissionData/MatrixMissionData")]
public class MatrixScoreMissionSO : MissionDataSO
{
    public string title;
    public int desiredScore;


    public void SetMissionDataSO(GameObject instansiatedObj)
    {
        instansiatedObj.GetComponent<MatrixScoreMission>().desiredScore = this.desiredScore;
    }
}
*/
/*
public class AlchemyMission : MissionDataSO
{

}
*/
// Add more minigame base mission data classes as needed
