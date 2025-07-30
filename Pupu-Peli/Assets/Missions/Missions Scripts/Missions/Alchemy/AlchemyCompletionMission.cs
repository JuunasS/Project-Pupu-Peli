using System;
using UnityEngine;

public class AlchemyCompletionMission : Mission
{

    private void Awake()
    {
        Debug.Log("Setting mission event call!");
        AlchemyMaster.AlchemyGameSuccess += CheckMissionState;
    }

    private void OnDestroy()
    {
        AlchemyMaster.AlchemyGameSuccess -= CheckMissionState;
    }

    public override void CheckMissionState()
    {
        base.CheckMissionState();
    }

}
