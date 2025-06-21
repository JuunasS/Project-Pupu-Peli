using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public int missionProgression; // Parameter for mission list index

    // List of mission objects (In order of mission appearance)
    [SerializeField]
    private MissionListObject[] missionArray;
    private Dictionary<int, List<GameObject>> missionList = new Dictionary<int, List<GameObject>>(); // List of every mission for current missionProgression state 


    public List<Mission> missions;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        missionList.Clear();
        for (int i = 0; i < missionArray.Length; i++)
        {
            missionList.Add(missionArray[i].index, missionArray[i].missionData);
        }
        // Set current mission active
        GenerateCurrentMissions();
    }

    public void GenerateCurrentMissions()
    {
        for (int i = 0; i < missionList[missionProgression].Count; i++)
        {
            // Initialize current mission prefabs!
            GameObject tempMissionObj = Instantiate(missionList[missionProgression][i], this.transform);
        }
    }

    // Function for checking current mission state

    // Function for updating current mission state

    // Event changed function for calling in other scripts


    // Function for saving mission state to file
    // - Current mission index
    // - Current mission progress
}


[Serializable]
public abstract class Mission : MonoBehaviour
{
    // This is a parent class for missions
    // Every mission must contain parameters for completion and a fucntion to check it's completion.

    public Mission(string title, Predicate<object> requirement, MissionDataSO data)
    {
        missionTitle = title;
        this.requirement = requirement;
        this.data = data;
    }

    public string missionTitle;
    public Predicate<object> requirement;
    MissionDataSO data;

    public bool complete;
    public virtual void CheckMissionProgress()
    {
        if (complete)
        {
            return;
        }

        if (RequirementsMet())
        {
            Debug.Log("Mission " + missionTitle + " complete!");
            complete = true;
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}



[Serializable]
public class MissionListObject
{
    public int index;
    public List<GameObject> missionData;
}