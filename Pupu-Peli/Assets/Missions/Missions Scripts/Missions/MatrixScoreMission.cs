using System;
using UnityEngine;

public class MatrixScoreMission : Mission
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int desiredScore;
    public MatrixScoreManager scoreManager;

    public MatrixScoreMission(string title, Predicate<object> requirement, MissionDataSO data) : base(title, requirement, data)
    {
    }

    void Start()
    {
        scoreManager = FindAnyObjectByType<MatrixScoreManager>();
    }

    private void Awake()
    {
        Debug.Log("Setting mission event call!");
        MatrixGameManager.MatrixGameEnd += CheckMissionState;
    }

    private void OnDestroy()
    {
        MatrixGameManager.MatrixGameEnd += CheckMissionState;
    }

    
    public void CheckMissionState()
    {
        scoreManager = FindAnyObjectByType<MatrixScoreManager>();
        // Add listener for matrix game end screen for checking score
        if (scoreManager.GetScore() >= desiredScore) { Debug.LogError("MissionCompleted!!"); }
    }

    
}
