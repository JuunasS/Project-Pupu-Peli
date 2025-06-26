using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MatrixMissionDataSO", menuName = "Scriptable Objects/MissionData/MatrixMissionData")]
public class MatrixScoreMissionSO : MissionDataSO
{
    public string title;
    public int desiredScore;


    public override void SetMissionDataSO(GameObject instansiatedObj)
    {
        instansiatedObj.GetComponent<MatrixScoreMission>().missionTitle = this.title;
        instansiatedObj.GetComponent<MatrixScoreMission>().desiredScore = this.desiredScore;
    }
}
