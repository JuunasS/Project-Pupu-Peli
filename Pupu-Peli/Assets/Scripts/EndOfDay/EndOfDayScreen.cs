using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayScreen : MonoBehaviour
{
    public GameObject canvas;

    public TMP_Text dayCompletedText;

    public TMP_Text coinsText;
    public int coinsCollected;

    public TMP_Text missionText;
    public List<string> missionsCompleted;

    public TMP_Text herbText;
    public List<string> herbsCompleted;

    public TMP_Text brewText;
    public List<string> brewsCompleted;

    public float endOfDayScreenDuration;

    public Diary diary;

    public static EndOfDayScreen Instance { get; private set; }

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


    public void ActivateEndOfDayScreen()
    {
       canvas.SetActive(true);
       SetStats();
    }

    public void SetStats()
    {
        dayCompletedText.text = "";
        coinsText.text = "";
        missionText.text = "";
        herbText.text = "";
        brewText.text = "";

        dayCompletedText.text = "Day " + DayNightCycle.Instance.daysCompleted + " Completed!";

        coinsText.text = coinsCollected.ToString();

        string tempMissionText = "";
        for (int i = 0; i < missionsCompleted.Count; i++)
        {
            tempMissionText += missionsCompleted[i] + "\n";
        }
        missionText.text = tempMissionText;

        string tempBrewText = "";
        for (int i = 0; i < herbsCompleted.Count; i++)
        {
            tempBrewText += herbsCompleted[i] + "\n";
        }
        herbText.text = tempBrewText;

        string tempHerbText = "";
        for (int i = 0; i < brewsCompleted.Count; i++)
        {
            tempHerbText += brewsCompleted[i] + "\n";
        }
        brewText.text = tempHerbText;

        diary.AddDayData(DayNightCycle.Instance.daysCompleted, coinsCollected, tempMissionText, tempBrewText, tempHerbText);
    }

    public void DisableEndOfDayScreen()
    {
        canvas.SetActive(false);
    }

    public void CoinsCollected(int coins) { this.coinsCollected += coins; }
    public void MissionCompleted(string text) { missionsCompleted.Add(text); }
    public void HerbCompleted(string text) { herbsCompleted.Add(text); }
    public void BrewCompleted(string text) { brewsCompleted.Add(text); }

}
