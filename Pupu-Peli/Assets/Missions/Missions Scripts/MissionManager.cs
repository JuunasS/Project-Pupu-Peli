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
    public GameObject missionHighlightPrefab;
    public tutorialManager tutorialManager;


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
        Debug.Log("Generating missions! 1 " + missionArray[missionProgression].missionDataList.Count);

        // Generate mission objects
        for (int i = 0; i < missionArray[missionProgression].missionDataList.Count; i++)
        {
            Debug.Log("Generating missions! 2");
            // Initialize current mission prefabs!
            GameObject tempMissionObj = Instantiate(missionArray[missionProgression].missionDataList[i].missionPrefab, this.transform);
            missionArray[missionProgression].missionDataList[i].SetMissionDataSO(tempMissionObj);

            tutorialManager.advanceTutorial(missionArray[missionProgression].missionDataList[i].name);

            activeMissions.Add(tempMissionObj);
        }

        // Generate highlight based on current missions
        if (missionArray[missionProgression].missionLocations.Length != 0)
        {
            // Generate highlights
            for (int i = 0; i < activeMissions.Count; i++)
            {
                GameObject missionHighlight = Instantiate(missionHighlightPrefab, missionArray[missionProgression].missionLocations[i]);
                missionHighlight.transform.SetParent(activeMissions[i].transform, false);
                missionHighlight.transform.position = missionArray[missionProgression].missionLocations[i].position;
                activeMissions[i].GetComponent<Mission>().missionHighlight = missionHighlight;
            }
        }
    }

    // Function for checking current mission state
    public void CheckMissionState()
    {
        for (int i = 0; activeMissions.Count > i; i++)
        {
            Debug.Log("activeMissions.Count " + activeMissions.Count);
            Debug.Log("!activeMissions[i].GetComponent<Mission>().complete " + !activeMissions[i].GetComponent<Mission>().isComplete);
            if (!activeMissions[i].GetComponent<Mission>().isComplete)
            {
                return;
            }
        }
        Debug.Log("ALL MISSIONS COMPLETE!");

        for (int i = activeMissions.Count - 1; 0 <= i; i--)
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

    /* Event changed function for calling in other scripts
    public void MissionProgressStateChanged()
    {

    }*/


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
    public string completionText;
    public bool isComplete;
    public GameObject missionHighlight;
    public float missionDayTimeDuration;

    public virtual void CheckMissionState()
    {
        this.isComplete = true;
        EndOfDayScreen.Instance.MissionCompleted(completionText);
        DayNightCycle.Instance.AddTime(missionDayTimeDuration);
        Destroy(this.missionHighlight);
        MissionManager.Instance.CheckMissionState();
    }
}



[Serializable]
public class MissionListObject
{
    //public int index;
    public List<MissionDataSO> missionDataList;
    public Transform[] missionLocations;
}