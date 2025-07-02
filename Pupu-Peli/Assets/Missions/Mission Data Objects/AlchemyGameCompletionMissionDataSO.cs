using UnityEngine;

[CreateAssetMenu(fileName = "AlchemyGameCompletionMissionDataSO", menuName = "Scriptable Objects/MissionData/AlchemyGameCompletionMissionDataSO")]
public class AlchemyGameCompletionMissionDataSO : MissionDataSO
{
    public string title;

    public override void SetMissionDataSO(GameObject instansiatedObj)
    {
        instansiatedObj.GetComponent<AlchemyCompletionMission>().missionTitle = this.title;
    }
}
