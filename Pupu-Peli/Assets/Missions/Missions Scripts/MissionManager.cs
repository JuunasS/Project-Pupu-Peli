using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public int missionProgression; // Parameter for mission list index

    // List of mission objects (In order of mission appearance)
    [SerializeField]
    private MissionListObject[] missionArray;
    public List<GameObject> activeMissions = new List<GameObject>();


    public static event Action<int> OnMissionProgressionChanged;

    public static MissionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        activeMissions.Clear();

        // Set current mission active
        GenerateCurrentMissions();
    }

    public void GenerateCurrentMissions()
    {
        for (int i = 0; i < missionArray[missionProgression].missionDataList.Count; i++)
        {
            // Initialize current mission prefabs!
            GameObject tempMissionObj = Instantiate(missionArray[missionProgression].missionDataList[i].missionPrefab, this.transform);
            missionArray[missionProgression].missionDataList[i].SetMissionDataSO(tempMissionObj);

            activeMissions.Add(tempMissionObj);

        }
    }

    // Function for checking current mission state
    public void CheckMissionState()
    {
        for (int i = 0; activeMissions.Count > i; i++)
        {
            Debug.Log("activeMissions.Count " + activeMissions.Count);
            Debug.Log("!activeMissions[i].GetComponent<Mission>().complete " + !activeMissions[i].GetComponent<Mission>().complete);
            if (!activeMissions[i].GetComponent<Mission>().complete)
            {
                return;
            }
        }
        Debug.Log("ALL MISSIONS COMPLETE!");

        for (int i = activeMissions.Count-1; 0 <= i; i--)
        {
            GameObject tempRef = activeMissions[i];
            activeMissions.Remove(tempRef);
            Destroy(tempRef);
        }
        // Move to next mission progress state
        missionProgression++;
        OnMissionProgressionChanged.Invoke(missionProgression);

        // Instantiate new missions
        GenerateCurrentMissions();
    }

    // Function for updating current mission state

    // Event changed function for calling in other scripts
    public void MissionProgressStateChanged()
    {

    }


    // Function for saving mission state to file
    // - Current mission index
    // - Current mission progress
}


[Serializable]
public abstract class Mission : MonoBehaviour
{
    // This is a parent class for missions
    // Every mission must contain parameters for completion and a fucntion to check it's completion.
    public string missionTitle;
    MissionDataSO data;

    public bool complete;

}



[Serializable]
public class MissionListObject
{
    //public int index;
    public List<MissionDataSO> missionDataList;
}