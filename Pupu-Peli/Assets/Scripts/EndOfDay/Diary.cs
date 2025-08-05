using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    [SerializeField]
    public List<DayData> daysCompleted;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddDayData(int dayNum, int coins, string missions, string brews, string herbs)
    {
        daysCompleted.Add(new DayData(dayNum, coins, missions, brews, herbs));
    }
}

[Serializable]
public class DayData
{
    public int dayNumber;
    public int coinsCollected;
    public string missionsCompleted;
    public string brewsCompleted;
    public string herbsCompleted;

    public DayData(int dayNum, int coins, string missions, string brews, string herbs)
    {
        dayNumber = dayNum;
        coinsCollected = coins;
        missionsCompleted = missions;
        brewsCompleted = brews;
        herbsCompleted = herbs;
    }
}
